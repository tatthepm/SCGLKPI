using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
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
                var ddlYear = ddl.GetDropDownListDeliveryMonth("Year");
                var ddlMonth = ddl.GetDropDownListDeliveryMonth("Month");
                var ddlMatName = ddl.GetDropDownListDeliveryMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                var ddlReason = (from m in objBs.reasonAcceptedBs.GetAll()
                                 select new
                                 {
                                     Id = m.Id,
                                     Name = m.Name,
                                 }).Distinct();

                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Delivery failed " + ex.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.acceptedAdjustedBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.acceptedAdjustedBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
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
            var q = from d in objBs.acceptedAdjustedBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
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
                model.ShippingPoint = item.SHPPOINT;
                model.TruckType = item.TRUCK_TYPE;
                model.LastTender = item.LTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.PlanAccept = item.PLNACPDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.LastAccept = item.LACPDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.Approve = Convert.ToBoolean(item.ACPD_ADJUST);
                model.AdjustBy = item.ACPD_ADJUST_BY;
                model.Remark = item.ACPD_REMARK;
                model.Reason = item.ACPD_REASON;
                model.thisReasonId = Convert.ToString(item.ACPD_REASON_ID);
                try
                {
                    model.FilePath = objBs.acceptedFilesBs.GetByShipment(item.SHPMNTNO).FirstOrDefault().FILEPATH;
                }
                catch(Exception)
                {
                    model.FilePath = "#";
                }
                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAcceptApprove(List<string> thisReasonId, List<string> txtSM, List<string> txtApprove, List<string> txtRemark, string yearId, string monthId)
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    // List<string> listSM = new List<string>();
                    int countSM = 0;
                    List<string> SMs = new List<string>(txtApprove.Distinct());
                    foreach (string sm in SMs)
                    {
                        var reasonId = objBs.acceptedAdjustedBs.GetByID(sm).ACPD_REASON_ID;
                        bool isadjust = objBs.reasonAcceptedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                        DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                        //Change adjustable here
                        ontimeShipment.ACPD_ADJUST = isadjust ? 1 : 0;
                        objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                        //delete AcceptedDelays
                        objBs.acceptedAdjustedBs.Delete(sm);

                        //update sum of adjust daily
                        DateTime LACPDDate = Convert.ToDateTime(ontimeShipment.LACPDDATE_D);
                        string matNameId = Convert.ToString(ontimeShipment.MATFRIGRP);
                        string sectionId = Convert.ToString(ontimeShipment.SECTION_ID);
                        string departmentId = Convert.ToString(ontimeShipment.DEPARTMENT_ID);

                        if (isadjust)
                        {
                            int id = objBs.ontimeAcceptBs.GetAll()
                                .Where(x => x.ActualGiDate == LACPDDate
                                       && x.DepartmentId == departmentId
                                       && x.SectionId == sectionId
                                       && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                            OntimeAccept ontimeAccept = objBs.ontimeAcceptBs.GetByID(id);
                            int adjACPD = ontimeAccept.AdjustAccept + 1;
                            ontimeAccept.AdjustAccept = adjACPD;
                            ontimeAccept.SumOfAdjustAccept = ontimeAccept.OnTime + adjACPD;
                            objBs.ontimeAcceptBs.Update(ontimeAccept);

                            // update sum of adjust monthly
                            int idM = objBs.ontimeAcceptMonthBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.Month == monthId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                            OntimeAcceptMonth ontimeAcceptMonth = objBs.ontimeAcceptMonthBs.GetByID(idM);
                            int adjACPDMonth = ontimeAcceptMonth.AdjustAccept + 1;
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
                            int adjACPDYear = ontimeAcceptYear.AdjustAccept + 1;
                            ontimeAcceptYear.AdjustAccept = adjACPDYear;
                            ontimeAcceptYear.SumOfAdjustAccept = ontimeAcceptYear.OnTime + adjACPDYear;
                            objBs.ontimeAcceptYearBs.Update(ontimeAcceptYear);
                        }
                        countSM++;
                    }

                    Trans.Complete();
                    return Content(countSM + " - Shipment is adjusted Successfully!");
                }
                catch (Exception ex)
                {
                    return Content("Operation update reason ontimed failed !" + ex.ToString());
                }
                //  return View();
            }
        }

    }
}
