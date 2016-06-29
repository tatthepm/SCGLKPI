using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Inbounded;
using System.Transactions;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers {
    public class AdjustInboundedController : BaseController {
        // GET: AdjustInbounded
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {

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
                var ddlMatName = (from m in objBs.inboundDelayBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation inbund adjust failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.inboundDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.inboundDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDelayInboundedData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                ViewBag.DepartmentId = DepartmentId;
                ViewBag.SectionId = SectionId;
                ViewBag.MatNameId = MatNameId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustInbounedViewModels> viewModel = new List<AdjustInbounedViewModels>();

                //filter department
                var q = from d in objBs.inboundDelayBs.GetAll()
                        where d.DEPARTMENT_ID == DepartmentId
                        && d.SECTION_ID == SectionId
                        && d.ACTGIDATE_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.ACTGIDATE_D.Value.Year == Convert.ToInt32(YearId)
                        select d;

                //filter matname
                if (!String.IsNullOrEmpty(MatNameId)) {
                    q = q.Where(x => x.MATFRIGRP == MatNameId);
                }

                int c = q.Count();

                foreach (var item in q) {
                    AdjustInbounedViewModels model = new AdjustInbounedViewModels();
                    model.DeliveryNote = item.DELVNO;
                    model.CarrierId = item.CARRIER_ID;
                    model.RegionId = item.REGION_ID;
                    model.RegionName = item.REGION_NAME_TH;
                    model.Soldto = item.SOLDTO;
                    model.SoldtoName = item.SOLDTO_NAME;
                    model.Shipto = item.SHIPTO;
                    model.ShiptoName = item.TO_SHPG_LOC_NAME;
                    model.PlanInbound = Convert.ToDateTime(item.PLNINBDATE);
                    model.ActualInbound = Convert.ToDateTime(item.ACTGIDATE);
                    viewModel.Add(model);
                }

                var ddlReason = (from r in objBs.reasonInboundBs.GetAll()
                                 select new {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return PartialView("pv_AdjustInbounded", viewModel);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation getDelayTenderedData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult UpdateInboundedReason(List<String> ReasonId, List<string> txtDN, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId) {
            using (TransactionScope Trans = new TransactionScope()) {

                try {

                    int countDN = 0;
                    for (int i = 0; i < ReasonId.Count; i++) {
                        if (!String.IsNullOrEmpty(ReasonId[i])) {
                            string dn = txtDN[i];
                            string reasonId = ReasonId[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonInboundBs.GetByID(Convert.ToInt32(reasonId)).Name;

                            DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                            ontimeDn.INB_ADJUST = 1;
                            ontimeDn.INB_ADJUST_BY = User.Identity.Name;
                            ontimeDn.INB_ADJUST_DATE = DateTime.Now;
                            ontimeDn.INB_REASON = reasonName;
                            ontimeDn.INB_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeDn.INB_REMARK = remark;

                            objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                            //delete AcceptedDelays
                            objBs.inboundDelayBs.Delete(dn);

                            //update sum of adjust daily
                            DateTime ACTGIDate = Convert.ToDateTime(objBs.dWH_ONTIME_DNBs.GetByID(dn).ACTGIDATE_D);

                            int id = objBs.ontimeInboundBs.GetAll()
                                .Where(x => x.ActualGiDate == ACTGIDate
                                       && x.DepartmentId == DepartmentId
                                       && x.SectionId == SectionId
                                       && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                            OntimeInbound ontimeInbound = objBs.ontimeInboundBs.GetByID(id);

                            int adjINB = ontimeInbound.AdjustInbound + 1;
                            ontimeInbound.AdjustInbound = adjINB;
                            ontimeInbound.SumOfAdjustInbound = ontimeInbound.OnTime + adjINB;
                            objBs.ontimeInboundBs.Update(ontimeInbound);
                            countDN++;
                        }
                    }

                    // update sum of adjust monthly
                    int idM = objBs.ontimeInboundMonthBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.Month == MonthId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeInboundMonth ontimeInboundMonth = objBs.ontimeInboundMonthBs.GetByID(idM);

                    int adjINBMonth = ontimeInboundMonth.AdjustInbound + countDN;
                    ontimeInboundMonth.AdjustInbound = adjINBMonth;
                    ontimeInboundMonth.SumOfAdjustInbound = ontimeInboundMonth.OnTime + adjINBMonth;
                    objBs.ontimeInboundMonthBs.Update(ontimeInboundMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeInboundYearBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeInboundYear ontimeInboundYear = objBs.ontimeInboundYearBs.GetByID(idY);

                    int adjTNRDYear = ontimeInboundYear.AdjustInbound + countDN;
                    ontimeInboundYear.AdjustInbound = adjTNRDYear;
                    ontimeInboundYear.SumOfAdjustInbound = ontimeInboundYear.OnTime + adjTNRDYear;
                    objBs.ontimeInboundYearBs.Update(ontimeInboundYear);

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