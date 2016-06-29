using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using System.Transactions;

namespace SCGLKPIUI.Controllers {
    public class AdjustAcceptedController : BaseController {
        // GET: AdjustAccepted
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {

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
                var ddlMatName = (from m in objBs.acceptedDelayBs.GetAll()
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
            var result = (from m in objBs.acceptedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.acceptedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDelayAcceptedData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                ViewBag.DepartmentId = DepartmentId;
                ViewBag.SectionId = SectionId;
                ViewBag.MatNameId = MatNameId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustAcceptedViewModels> viewModel = new List<AdjustAcceptedViewModels>();

                //filter department
                var q = from d in objBs.acceptedDelayBs.GetAll()
                        where d.DEPARTMENT_ID == DepartmentId
                        && d.SECTION_ID == SectionId
                        && d.LACPDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.LACPDDATE_D.Value.Year == Convert.ToInt32(YearId)
                        select d;

                //filter matname
                if (!String.IsNullOrEmpty(MatNameId)) {
                    q = q.Where(x => x.MATFRIGRP == MatNameId);
                }

                int c = q.Count();

                foreach (var item in q) {
                    AdjustAcceptedViewModels model = new AdjustAcceptedViewModels();
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
                    viewModel.Add(model);
                }

                var ddlReason = (from r in objBs.reasonAcceptedBs.GetAll()
                                 select new {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return PartialView("pv_AdjustAccepted", viewModel);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation getDelayAccepteData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult UpdateAcceptReason(List<String> ReasonId, List<string> txtSM, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId) {
            using (TransactionScope Trans = new TransactionScope()) {

                try {
                   // List<string> listSM = new List<string>();
                    int countSM = 0;
                    for (int i = 0; i < ReasonId.Count; i++) {
                        if (!String.IsNullOrEmpty(ReasonId[i])) {
                            string sm = txtSM[i];
                            string reasonId = ReasonId[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonAcceptedBs.GetByID(Convert.ToInt32(reasonId)).Name;

                            DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                            ontimeShipment.ACPD_ADJUST = 1;
                            ontimeShipment.ACPD_ADJUST_BY = User.Identity.Name;
                            ontimeShipment.ACPD_ADJUST_DATE = DateTime.Now;
                            ontimeShipment.ACPD_REASON = reasonName;
                            ontimeShipment.ACPD_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeShipment.ACPD_REMARK = remark;
                            objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                            //delete AcceptedDelays
                            objBs.acceptedDelayBs.Delete(sm);

                            //update sum of adjust daily
                            DateTime LACPDDate = Convert.ToDateTime(objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm).LACPDDATE_D);

                            int id = objBs.ontimeAcceptBs.GetAll()
                                .Where(x => x.AcceptDate == LACPDDate
                                       && x.DepartmentId == DepartmentId
                                       && x.SectionId == SectionId
                                       && x.MatFriGrp == MatNameId).FirstOrDefault().Id;
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
                              .Where(x => x.Year == YearId
                              && x.Month == MonthId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;
                    OntimeAcceptMonth ontimeAcceptMonth = objBs.ontimeAcceptMonthBs.GetByID(idM);
                    int adjACPDMonth = ontimeAcceptMonth.AdjustAccept + countSM;
                    ontimeAcceptMonth.AdjustAccept = adjACPDMonth;
                    ontimeAcceptMonth.SumOfAdjustAccept = ontimeAcceptMonth.OnTime + adjACPDMonth;
                    objBs.ontimeAcceptMonthBs.Update(ontimeAcceptMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeAcceptYearBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;
                    OntimeAcceptYear ontimeAcceptYear = objBs.ontimeAcceptYearBs.GetByID(idY);
                    int adjACPDYear = ontimeAcceptYear.AdjustAccept + countSM;
                    ontimeAcceptYear.AdjustAccept = adjACPDYear;
                    ontimeAcceptYear.SumOfAdjustAccept = ontimeAcceptYear.OnTime + adjACPDYear;
                    objBs.ontimeAcceptYearBs.Update(ontimeAcceptYear);

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countSM + "-Shipment is adjusted Successfully!" });

                }
                catch (Exception ex) {
                    return RedirectToAction("Index", new { sms = "Operation update reason accepted failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
                //  return View();
            }
        }

        [HttpPost]
        public JsonResult tableJsonData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {

            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustAcceptedViewModels> viewModel = new List<AdjustAcceptedViewModels>();

            //filter department
            var q = from d in objBs.acceptedDelayBs.GetAll()
                    where d.DEPARTMENT_ID == DepartmentId
                    && d.SECTION_ID == SectionId
                    && d.LACPDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                    && d.LACPDDATE_D.Value.Year == Convert.ToInt32(YearId)
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId)) {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            int c = q.Count();

            foreach (var item in q) {
                AdjustAcceptedViewModels model = new AdjustAcceptedViewModels();
                model.Shipment = item.SHPMNTNO;
                model.CarrierId = item.CARRIER_ID;
                //model.RegionId = item.REGION_ID;
                //model.RegionName = item.REGION_NAME_TH;
                // model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                // model.Shipto = item.SHIPTO;
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.PlanAccept = Convert.ToDateTime(item.PLNACPDDATE);
                model.LastAccept = Convert.ToDateTime(item.LACPDDATE);
                viewModel.Add(model);
            }

            var ddlReason = (from r in objBs.reasonAcceptedBs.GetAll()
                             select new {
                                 Id = r.Id,
                                 Name = r.Name
                             }).Distinct().OrderBy(x => x.Name);
            ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

            return Json(viewModel, JsonRequestBehavior.AllowGet);
            //return PartialView("pv_AdjustAccepted", viewModel);

        }
    }
}