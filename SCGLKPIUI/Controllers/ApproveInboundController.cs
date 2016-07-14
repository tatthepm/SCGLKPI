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
    public class ApproveInboundedController : BaseController
    {
        // GET: AdjustInbounded
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            try
            {
                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListInboundMonth("Year");
                var ddlMonth = ddl.GetDropDownListInboundMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.inboundAdjustedBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new
                                  {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

                var ddlReason = (from m in objBs.reasonInboundBs.GetAll()
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
                return RedirectToAction("Index", new { sms = "Operation Inbound failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            var result = (from m in objBs.inboundAdjustedBs.GetAll()
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
            var result = (from m in objBs.inboundAdjustedBs.GetAll()
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
            var result = (from r in objBs.reasonInboundBs.GetAll()
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonApproveOntimeTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AdjustInboundedViewModels>
            List<ApproveInboundedViewModels> viewModel = new List<ApproveInboundedViewModels>();

            //filter department
            var q = from d in objBs.inboundAdjustedBs.GetAll()
                    where d.DEPARTMENT_ID == DepartmentId
                    && d.SECTION_ID == SectionId
                    && d.ACTGIDATE_D.Value.Month == Convert.ToInt32(MonthId)
                    && d.ACTGIDATE_D.Value.Year == Convert.ToInt32(YearId)
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            foreach (var item in q)
            {
                ApproveInboundedViewModels model = new ApproveInboundedViewModels();
                model.Dn = item.DELVNO;
                model.CarrierId = item.CARRIER_ID;
                model.RegionId = item.REGION_ID;
                model.RegionName = item.REGION_NAME_TH;
                model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                model.Shipto = item.SHIPTO;
                model.ShiptoName = item.TO_SHPG_LOC_NAME;
                model.PlanInbound = Convert.ToDateTime(item.PLNINBDATE);
                model.ActualInbound = Convert.ToDateTime(item.ACTGIDATE);
                model.Approve = Convert.ToBoolean(item.INB_ADJUST);
                model.AdjustBy = item.INB_ADJUST_BY;
                model.Remark = item.INB_REMARK;
                model.Reason = item.INB_REASON;
                model.thisReasonId = Convert.ToString(item.INB_REASON_ID);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateInboundApprove(List<string> thisReasonId, List<string> txtDN, List<string> txtApprove, List<string> txtRemark, string departmentId, string sectionId, string matNameId, string yearId, string monthId)
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    // List<string> listSM = new List<string>();
                    int countDN = 0;
                    foreach (string index in txtApprove)
                    {
                        int i = Convert.ToInt16(index);

                        string dn = txtDN[i];
                        string reasonId = thisReasonId[i];
                        string remark = txtRemark[i];
                        string reasonName = objBs.reasonInboundBs.GetByID(Convert.ToInt32(reasonId)).Name;
                        bool isadjust = objBs.reasonInboundBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                        DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                        ontimeDn.INB_ADJUST = isadjust ? 1 : 0;
                        ontimeDn.INB_ADJUST_BY = User.Identity.Name;
                        ontimeDn.INB_ADJUST_DATE = DateTime.Now;
                        ontimeDn.INB_REASON = reasonName;
                        ontimeDn.INB_REASON_ID = Convert.ToInt32(reasonId);
                        ontimeDn.INB_REMARK = remark;

                        objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                        //delete InboundedDelays
                        objBs.inboundAdjustedBs.Delete(dn);

                        //update sum of adjust daily
                        DateTime ONTIMEDate = Convert.ToDateTime(objBs.dWH_ONTIME_DNBs.GetByID(dn).ACDLVDATE_D);

                        if (isadjust)
                        {

                            int id = objBs.ontimeInboundBs.GetAll()
                                .Where(x => x.ActualGiDate == ONTIMEDate
                                       && x.DepartmentId == departmentId
                                       && x.SectionId == sectionId
                                       && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeInbound ontimeInbound = objBs.ontimeInboundBs.GetByID(id);

                            int adjOntime = ontimeInbound.AdjustInbound + 1;
                            ontimeInbound.AdjustInbound = adjOntime;
                            ontimeInbound.SumOfAdjustInbound = ontimeInbound.OnTime + adjOntime;
                            objBs.ontimeInboundBs.Update(ontimeInbound);

                            // update sum of adjust monthly
                            int idM = objBs.ontimeInboundMonthBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.Month == monthId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeInboundMonth ontimeInboundMonth = objBs.ontimeInboundMonthBs.GetByID(idM);

                            int adjInboundMonth = ontimeInboundMonth.AdjustInbound + countDN;
                            ontimeInboundMonth.AdjustInbound = adjInboundMonth;
                            ontimeInboundMonth.SumOfAdjustInbound = ontimeInboundMonth.OnTime + adjInboundMonth;
                            objBs.ontimeInboundMonthBs.Update(ontimeInboundMonth);

                            // update sum of adjust yearly
                            int idY = objBs.ontimeInboundYearBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeInboundYear ontimeInboundYear = objBs.ontimeInboundYearBs.GetByID(idY);

                            int adjInboundYear = ontimeInboundYear.AdjustInbound + countDN;
                            ontimeInboundYear.AdjustInbound = adjInboundYear;
                            ontimeInboundYear.SumOfAdjustInbound = ontimeInboundYear.OnTime + adjInboundYear;
                            objBs.ontimeInboundYearBs.Update(ontimeInboundYear);
                        }
                    }
                    countDN++;


                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countDN + "-Shipment is adjusted Successfully!" });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { sms = "Operation update reason Inbounded failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
                //  return View();

            }
        }

    }
}
