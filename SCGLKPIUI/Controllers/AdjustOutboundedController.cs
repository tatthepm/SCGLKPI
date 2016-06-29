using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Outbounded;
using System.Transactions;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers {
    public class AdjustOutboundedController : BaseController {
        // GET: AdjustOutbounded
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {

                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListOutboundMonth("Year");
                var ddlMonth = ddl.GetDropDownListOutboundMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.outboundDelayBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation outbound adjust failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.outboundDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.outboundDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDelayOutboundedData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                ViewBag.DepartmentId = DepartmentId;
                ViewBag.SectionId = SectionId;
                ViewBag.MatNameId = MatNameId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustOutbounedViewModels> viewModel = new List<AdjustOutbounedViewModels>();

                //filter department
                var q = from d in objBs.outboundDelayBs.GetAll()
                        where d.DEPARTMENT_ID == DepartmentId
                        && d.SECTION_ID == SectionId
                        && d.ACDLVDATE_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.ACDLVDATE_D.Value.Year == Convert.ToInt32(YearId)
                        select d;

                //filter matname
                if (!String.IsNullOrEmpty(MatNameId)) {
                    q = q.Where(x => x.MATFRIGRP == MatNameId);
                }

                int c = q.Count();

                foreach (var item in q) {
                    AdjustOutbounedViewModels model = new AdjustOutbounedViewModels();
                    model.DeliveryNote = item.DELVNO;
                    model.CarrierId = item.CARRIER_ID;
                    model.RegionId = item.REGION_ID;
                    model.RegionName = item.REGION_NAME_TH;
                    model.Soldto = item.SOLDTO;
                    model.SoldtoName = item.SOLDTO_NAME;
                    model.Shipto = item.SHIPTO;
                    model.ShiptoName = item.TO_SHPG_LOC_NAME;
                    model.PlanOutbound = Convert.ToDateTime(item.PLNOUTBDATE);
                    model.ActualOutbound = Convert.ToDateTime(item.ACDLVDATE);
                    viewModel.Add(model);
                }

                var ddlReason = (from r in objBs.reasonOutboundBs.GetAll()
                                 select new {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return PartialView("pv_AdjustOutbounded", viewModel);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation getDelayTenderedData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult UpdateOutboundedReason(List<String> ReasonId, List<string> txtDN, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId) {
            using (TransactionScope Trans = new TransactionScope()) {

                try {

                    int countDN = 0;
                    for (int i = 0; i < ReasonId.Count; i++) {
                        if (!String.IsNullOrEmpty(ReasonId[i])) {
                            string dn = txtDN[i];
                            string reasonId = ReasonId[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonOutboundBs.GetByID(Convert.ToInt32(reasonId)).Name;

                            DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                            ontimeDn.OUTB_ADJUST = 1;
                            ontimeDn.OUTB_ADJUST_BY = User.Identity.Name;
                            ontimeDn.OUTB_ADJUST_DATE = DateTime.Now;
                            ontimeDn.OUTB_REASON = reasonName;
                            ontimeDn.OUTB_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeDn.OUTB_REMARK = remark;

                            objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                            //delete OutboundDelays
                            objBs.outboundDelayBs.Delete(dn);

                            //update sum of adjust daily
                            DateTime ACTGIDate = Convert.ToDateTime(objBs.dWH_ONTIME_DNBs.GetByID(dn).ACTGIDATE_D);

                            int id = objBs.ontimeOutboundBs.GetAll()
                                .Where(x => x.ActualGiDate == ACTGIDate
                                       && x.DepartmentId == DepartmentId
                                       && x.SectionId == SectionId
                                       && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                            OntimeOutbound ontimeOutbound = objBs.ontimeOutboundBs.GetByID(id);

                            int adjOUTB = ontimeOutbound.AdjustOutbound + 1;
                            ontimeOutbound.AdjustOutbound = adjOUTB;
                            ontimeOutbound.SumOfAdjustOutbound = ontimeOutbound.OnTime + adjOUTB;
                            objBs.ontimeOutboundBs.Update(ontimeOutbound);
                            countDN++;
                        }
                    }

                    // update sum of adjust monthly
                    int idM = objBs.ontimeOutboundMonthBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.Month == MonthId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeOutboundMonth ontimeOutboundMonth = objBs.ontimeOutboundMonthBs.GetByID(idM);

                    int adjOUTBMonth = ontimeOutboundMonth.AdjustOutbound + countDN;
                    ontimeOutboundMonth.AdjustOutbound = adjOUTBMonth;
                    ontimeOutboundMonth.SumOfAdjustOutbound = ontimeOutboundMonth.OnTime + adjOUTBMonth;
                    objBs.ontimeOutboundMonthBs.Update(ontimeOutboundMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeOutboundYearBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeOutboundYear ontimeOutboundYear = objBs.ontimeOutboundYearBs.GetByID(idY);

                    int adjOUTBYear = ontimeOutboundYear.AdjustOutbound + countDN;
                    ontimeOutboundYear.AdjustOutbound = adjOUTBYear;
                    ontimeOutboundYear.SumOfAdjustOutbound = ontimeOutboundYear.OnTime + adjOUTBYear;
                    objBs.ontimeOutboundYearBs.Update(ontimeOutboundYear);

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countDN + "-DN is adjusted Successfully!" });

                }
                catch (Exception ex) {
                    return RedirectToAction("Index", new { sms = "Operation update reason accepted failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
            }
        }

    }
}