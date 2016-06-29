using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Tendered;
using System.Transactions;

namespace SCGLKPIUI.Controllers {
    public class AdjustTenderedController : BaseController {
        // GET: AdjustTendered
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {

                TempData["Msg"] = sms;

                //var ddlDept = (from d in objBs.tenderedDelayBs.GetAll()
                //               where !String.IsNullOrEmpty(d.MATNAME)
                //               && !String.IsNullOrEmpty(d.DEPARTMENT_Name)
                //               && !String.IsNullOrEmpty(d.SECTION_NAME)
                //               select new {
                //                   Id = d.DEPARTMENT_ID,
                //                   Name = d.DEPARTMENT_Name,
                //               }).Distinct().OrderBy(x=>x.Name);

                //var ddlSec = (from s in objBs.tenderedDelayBs.GetAll()
                //              where !String.IsNullOrEmpty(s.MATNAME)
                //              && !String.IsNullOrEmpty(s.DEPARTMENT_Name)
                //              && !String.IsNullOrEmpty(s.SECTION_NAME)
                //              select new {
                //                  Id = s.SECTION_ID,
                //                  Name = s.SECTION_NAME,
                //              }).Distinct().OrderBy(x=>x.Name);

                //var ddlYear = (from y in objBs.tenderedDelayBs.GetAll()
                //               where !String.IsNullOrEmpty(y.MATNAME)
                //                && !String.IsNullOrEmpty(y.DEPARTMENT_Name)
                //                && !String.IsNullOrEmpty(y.SECTION_NAME)
                //               select new {
                //                   Id = y.FTNRDDATE_D.Value.Year,
                //                   Name = y.FTNRDDATE_D.Value.Year,
                //               }).Distinct();

                //var ddlMonth = (from y in objBs.tenderedDelayBs.GetAll()
                //                where !String.IsNullOrEmpty(y.MATNAME)
                //                 && !String.IsNullOrEmpty(y.DEPARTMENT_Name)
                //                 && !String.IsNullOrEmpty(y.SECTION_NAME)
                //                select new {
                //                    Id = y.FTNRDDATE_D.Value.Month,
                //                    Name = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(y.FTNRDDATE_D.Value.Month)
                //                }).Distinct().OrderBy(x => x.Id);

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListTenderedMonth("Year");
                var ddlMonth = ddl.GetDropDownListTenderedMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.tenderedDelayBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.tenderedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.tenderedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDelayTenderedData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                ViewBag.DepartmentId = DepartmentId;
                ViewBag.SectionId = SectionId;
                ViewBag.MatNameId = MatNameId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustTenderedViewModels> viewModel = new List<AdjustTenderedViewModels>();

                //filter department
                var q = from d in objBs.tenderedDelayBs.GetAll()
                        where d.DEPARTMENT_ID == DepartmentId
                        && d.SECTION_ID == SectionId
                        && d.FTNRDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.FTNRDDATE_D.Value.Year == Convert.ToInt32(YearId)
                        select d;

                //filter matname
                if (!String.IsNullOrEmpty(MatNameId)) {
                    q = q.Where(x => x.MATFRIGRP == MatNameId);
                }

                int c = q.Count();

                foreach (var item in q) {
                    AdjustTenderedViewModels model = new AdjustTenderedViewModels();
                    model.Shipment = item.SHPMNTNO;
                    model.CarrierId = item.CARRIER_ID;
                    model.RegionId = item.REGION_ID;
                    model.RegionName = item.REGION_NAME_TH;
                    model.Soldto = item.SOLDTO;
                    model.SoldtoName = item.SOLDTO_NAME;
                    model.Shipto = item.SHIPTO;
                    model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                    model.PlanTender = Convert.ToDateTime(item.PLNTNRDDATE);
                    model.FirstTender = Convert.ToDateTime(item.FTNRDDATE);
                    viewModel.Add(model);
                }

                var ddlReason = (from r in objBs.reasonTenderedBs.GetAll()
                                 select new {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return PartialView("pv_AdjustTendered", viewModel);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation getDelayTenderedData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult UpdateTenderedReason(List<String> ReasonId, List<string> txtSM, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId) {
            using (TransactionScope Trans = new TransactionScope()) {

                try {
                    // List<string> listSM = new List<string>();
                    int countSM = 0;
                    for (int i = 0; i < ReasonId.Count; i++) {
                        if (!String.IsNullOrEmpty(ReasonId[i])) {
                            string sm = txtSM[i];
                            string reasonId = ReasonId[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).Name;

                            DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                            ontimeShipment.TNRD_ADJUST = 1;
                            ontimeShipment.TNRD_ADJUST_BY = User.Identity.Name;
                            ontimeShipment.TNRD_ADJUST_DATE = DateTime.Now;
                            ontimeShipment.TNRD_ONTIME_REASON = reasonName;
                            ontimeShipment.TNRD_ONTIME_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeShipment.TNRD_ONTIME_REMARK = remark;
                            objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                            //delete AcceptedDelays
                            objBs.tenderedDelayBs.Delete(sm);

                            //update sum of adjust daily
                            DateTime FTNRDDate = Convert.ToDateTime(objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm).FTNRDDATE_D);

                            int id = objBs.ontimeTenderBs.GetAll()
                                .Where(x => x.FirstTenderDate == FTNRDDate
                                       && x.DepartmentId == DepartmentId
                                       && x.SectionId == SectionId
                                       && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                            OntimeTender ontimeTender = objBs.ontimeTenderBs.GetByID(id);

                            int adjTNRD = ontimeTender.AdjustTender + 1;
                            ontimeTender.AdjustTender = adjTNRD;
                            ontimeTender.SumOfAdjustTender = ontimeTender.OnTime + adjTNRD;
                            objBs.ontimeTenderBs.Update(ontimeTender);
                            countSM++;
                        }
                    }

                    // update sum of adjust monthly
                    int idM = objBs.ontimeTenderMonthBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.Month == MonthId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeTenderMonth ontimeTenderMonth = objBs.ontimeTenderMonthBs.GetByID(idM);

                    int adjTNRDMonth = ontimeTenderMonth.AdjustTender + countSM;
                    ontimeTenderMonth.AdjustTender = adjTNRDMonth;
                    ontimeTenderMonth.SumOfAdjustTender = ontimeTenderMonth.OnTime + adjTNRDMonth;
                    objBs.ontimeTenderMonthBs.Update(ontimeTenderMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeTenderYearBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeTenderYear ontimeTenderYear = objBs.ontimeTenderYearBs.GetByID(idY);

                    int adjTNRDYear = ontimeTenderYear.AdjustTender + countSM;
                    ontimeTenderYear.AdjustTender = adjTNRDYear;
                    ontimeTenderYear.SumOfAdjustTender = ontimeTenderYear.OnTime + adjTNRDYear;
                    objBs.ontimeTenderYearBs.Update(ontimeTenderYear);

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countSM + "-Shipment is adjusted Successfully!" });

                }
                catch (Exception ex) {
                    return RedirectToAction("Index", new { sms = "Operation update reason accepted failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
                //  return View();
            }
        }
    }
}