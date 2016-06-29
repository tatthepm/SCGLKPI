using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.DocReturned;
using System.Transactions;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers {
    public class AdjustDocReturnedController : BaseController {
        // GET: AdjustDocReturned
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {

                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListDocReturnMonth("Year");
                var ddlMonth = ddl.GetDropDownListDocReturnMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.docReturnDelayBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation doc return adjust failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.docReturnDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.docReturnDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDelayDocReturnedData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                ViewBag.DepartmentId = DepartmentId;
                ViewBag.SectionId = SectionId;
                ViewBag.MatNameId = MatNameId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustDocReturnedViewModels> viewModel = new List<AdjustDocReturnedViewModels>();

                //filter department
                var q = from d in objBs.docReturnDelayBs.GetAll()
                        where d.DEPARTMENT_ID == DepartmentId
                        && d.SECTION_ID == SectionId
                        && d.DOCRETDATE_SCGL_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.DOCRETDATE_SCGL_D.Value.Year == Convert.ToInt32(YearId)
                        select d;

                //filter matname
                if (!String.IsNullOrEmpty(MatNameId)) {
                    q = q.Where(x => x.MATFRIGRP == MatNameId);
                }

                int c = q.Count();

                foreach (var item in q) {
                    AdjustDocReturnedViewModels model = new AdjustDocReturnedViewModels();
                    model.DeliveryNote = item.DELVNO;
                    model.CarrierId = item.CARRIER_ID;
                    model.RegionId = item.REGION_ID;
                    model.RegionName = item.REGION_NAME_TH;
                    model.Soldto = item.SOLDTO;
                    model.SoldtoName = item.SOLDTO_NAME;
                    model.Shipto = item.SHIPTO;
                    model.ShiptoName = item.TO_SHPG_LOC_NAME;
                    model.PlanDocReturn = Convert.ToDateTime(item.PLNDOCRETDATE_SCGL);
                    model.ActualDocReturn = Convert.ToDateTime(item.DOCRETDATE_SCGL);
                    viewModel.Add(model);
                }

                var ddlReason = (from r in objBs.reasonDocReturnBs.GetAll()
                                 select new {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return PartialView("pv_AdjustDocReturned", viewModel);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation getDelayTenderedData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult UpdateDocReturnedReason(List<String> ReasonId, List<string> txtDN, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId) {
            using (TransactionScope Trans = new TransactionScope()) {

                try {

                    int countDN = 0;
                    for (int i = 0; i < ReasonId.Count; i++) {
                        if (!String.IsNullOrEmpty(ReasonId[i])) {
                            string dn = txtDN[i];
                            string reasonId = ReasonId[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonDocReturnBs.GetByID(Convert.ToInt32(reasonId)).Name;

                            DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                            ontimeDn.SCGL_DOCRET_ADJUST = 1;
                            ontimeDn.SCGL_DOCRET_ADJUST_BY = User.Identity.Name;
                            ontimeDn.SCGL_DOCRET_ADJUST_DATE = DateTime.Now;
                            ontimeDn.SCGL_DOCRET_REASON = reasonName;
                            ontimeDn.SCGL_DOCRET_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeDn.SCGL_DOCRET_REMARK = remark;

                            objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                            //delete AcceptedDelays
                            objBs.docReturnDelayBs.Delete(dn);

                            //update sum of adjust daily
                            DateTime DOCRETDate = Convert.ToDateTime(objBs.dWH_ONTIME_DNBs.GetByID(dn).DOCRETDATE_SCGL_D);

                            int id = objBs.ontimeDocReturnBs.GetAll()
                                .Where(x => x.ActualGiDate == DOCRETDate
                                       && x.DepartmentId == DepartmentId
                                       && x.SectionId == SectionId
                                       && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                            OntimeDocReturn ontimeDocReturn = objBs.ontimeDocReturnBs.GetByID(id);

                            int adjDOCRET = ontimeDocReturn.AdjustDocReturn + 1;
                            ontimeDocReturn.AdjustDocReturn = adjDOCRET;
                            ontimeDocReturn.SumOfAdjustDocReturn= ontimeDocReturn.OnTime + adjDOCRET;
                            objBs.ontimeDocReturnBs.Update(ontimeDocReturn);
                            countDN++;
                        }
                    }

                    // update sum of adjust monthly
                    int idM = objBs.ontimeDocReturnMonthBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.Month == MonthId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeDocReturnMonth ontimeDocReturnMonth = objBs.ontimeDocReturnMonthBs.GetByID(idM);

                    int adjDOCRETMonth = ontimeDocReturnMonth.AdjustDocReturn + countDN;
                    ontimeDocReturnMonth.AdjustDocReturn = adjDOCRETMonth;
                    ontimeDocReturnMonth.SumOfAdjustDocReturn = ontimeDocReturnMonth.OnTime + adjDOCRETMonth;
                    objBs.ontimeDocReturnMonthBs.Update(ontimeDocReturnMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeDocReturnYearBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeDocReturnYear ontimeDocReturnYear = objBs.ontimeDocReturnYearBs.GetByID(idY);

                    int adjDOCRETYear = ontimeDocReturnYear.AdjustDocReturn + countDN;
                    ontimeDocReturnYear.AdjustDocReturn = adjDOCRETYear;
                    ontimeDocReturnYear.SumOfAdjustDocReturn = ontimeDocReturnYear.OnTime + adjDOCRETYear;
                    objBs.ontimeDocReturnYearBs.Update(ontimeDocReturnYear);

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countDN + "-DN is adjusted Successfully!" });

                }
                catch (Exception ex) {
                    return RedirectToAction("Index", new { sms = "Operation update reason doc return failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
            }
        }

    }
}