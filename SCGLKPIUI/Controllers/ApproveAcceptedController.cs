using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using System.Transactions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace SCGLKPIUI.Controllers
{
    public class ApproveAcceptedController : BaseController
    {
        // GET: AdjustAccepted
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            try
            {
                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListAcceptedMonth("Year");
                var ddlMonth = ddl.GetDropDownListAcceptedMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.acceptedAdjustedBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new
                                  {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

                var ddlReason = (from m in objBs.reasonAcceptedBs.GetAll()
                                  select new
                                  {
                                      Id = m.Id,
                                      Name = m.Name,
                                  }).Distinct();

                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            var result = (from m in objBs.acceptedAdjustedBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new
                          {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid)
        {
            var result = (from m in objBs.acceptedAdjustedBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new
                          {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonAcceptedBs.GetAll()
                            select new
                            {
                                Id = r.Id,
                                Name = r.Name
                            }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonApproveAcceptTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AdjustAcceptedViewModels>
            List<ApproveAcceptedViewModels> viewModel = new List<ApproveAcceptedViewModels>();

            //filter department
            var q = from d in objBs.acceptedAdjustedBs.GetAll()
                    where d.DEPARTMENT_ID == DepartmentId
                    && d.SECTION_ID == SectionId
                    && d.LACPDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                    && d.LACPDDATE_D.Value.Year == Convert.ToInt32(YearId)
                    && d.ACPD_ADJUST == 0
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            foreach (var item in q)
            {
                ApproveAcceptedViewModels model = new ApproveAcceptedViewModels();
                model.Shipment = item.SHPMNTNO;
                model.CarrierId = item.CARRIER_ID;
                model.RegionId = item.REGION_ID;
                model.RegionName = item.REGION_NAME_TH;
                model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                model.Shipto = item.SHIPTO;
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.PlanAccept = Convert.ToDateTime(item.PLNACPDDATE);
                model.LastAccept = Convert.ToDateTime(item.LACPDDATE);
                model.Approve = Convert.ToBoolean(item.ACPD_ADJUST);
                model.AdjustBy = item.ACPD_ADJUST_BY;
                model.Remark = item.ACPD_REMARK;
                model.Reason = item.ACPD_REASON;
                model.thisReasonId = Convert.ToString(item.ACPD_REASON_ID);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAcceptApprove(List<string> thisReasonId, List<string> txtSM, List<string> txtApprove, List<string> txtRemark, string departmentId, string sectionId, string matNameId, string yearId, string monthId)
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    // List<string> listSM = new List<string>();
                    int countSM = 0;
                    foreach(string index in txtApprove)
                    {
                        int i = Convert.ToInt16(index);
                        if (!String.IsNullOrEmpty(txtApprove[i]))
                        {
                            string sm = txtSM[i];
                            string reasonId = thisReasonId[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonAcceptedBs.GetByID(Convert.ToInt32(reasonId)).Name;
                            bool isadjust = objBs.reasonAcceptedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                            DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                            //Change adjustable here
                            ontimeShipment.ACPD_ADJUST = isadjust ? 1 : 0;
                            ontimeShipment.ACPD_ADJUST_BY = User.Identity.Name;
                            ontimeShipment.ACPD_ADJUST_DATE = DateTime.Now;
                            ontimeShipment.ACPD_REASON = reasonName;
                            ontimeShipment.ACPD_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeShipment.ACPD_REMARK = remark;
                            objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                            //delete AcceptedDelays
                            objBs.acceptedAdjustedBs.Delete(sm);

                            //update sum of adjust daily
                            DateTime LACPDDate = Convert.ToDateTime(objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm).LACPDDATE_D);

                            int id = objBs.ontimeAcceptBs.GetAll()
                                .Where(x => x.AcceptDate == LACPDDate
                                       && x.DepartmentId == departmentId
                                       && x.SectionId == sectionId
                                       && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                            OntimeAccept ontimeAccept = objBs.ontimeAcceptBs.GetByID(id);
                            int adjACPD = ontimeAccept.AdjustAccept + 1;
                            ontimeAccept.AdjustAccept = adjACPD;
                            ontimeAccept.SumOfAdjustAccept = ontimeAccept.OnTime + adjACPD;
                            objBs.ontimeAcceptBs.Update(ontimeAccept);
                            countSM++;
                        }
                    }

                    // update sum of adjust monthly
                    int idM = objBs.ontimeAcceptMonthBs.GetAll()
                              .Where(x => x.Year == yearId
                              && x.Month == monthId
                              && x.DepartmentId == departmentId
                              && x.SectionId == sectionId
                              && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                    OntimeAcceptMonth ontimeAcceptMonth = objBs.ontimeAcceptMonthBs.GetByID(idM);
                    int adjACPDMonth = ontimeAcceptMonth.AdjustAccept + countSM;
                    ontimeAcceptMonth.AdjustAccept = adjACPDMonth;
                    ontimeAcceptMonth.SumOfAdjustAccept = ontimeAcceptMonth.OnTime + adjACPDMonth;
                    objBs.ontimeAcceptMonthBs.Update(ontimeAcceptMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeAcceptYearBs.GetAll()
                              .Where(x => x.Year == yearId
                              && x.DepartmentId == departmentId
                              && x.SectionId == sectionId
                              && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                    OntimeAcceptYear ontimeAcceptYear = objBs.ontimeAcceptYearBs.GetByID(idY);
                    int adjACPDYear = ontimeAcceptYear.AdjustAccept + countSM;
                    ontimeAcceptYear.AdjustAccept = adjACPDYear;
                    ontimeAcceptYear.SumOfAdjustAccept = ontimeAcceptYear.OnTime + adjACPDYear;
                    objBs.ontimeAcceptYearBs.Update(ontimeAcceptYear);

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countSM + "-Shipment is adjusted Successfully!" });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { sms = "Operation update reason accepted failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
                //  return View();
            }
        }

    }
}
