using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Delivered;
using System.Transactions;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers {
    public class AdjustDeliveredController : BaseController {
        // GET: AdjustDelivered
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {

                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListDeliveryMonth("Year");
                var ddlMonth = ddl.GetDropDownListDeliveryMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.ontimeDelayBs.GetAll()
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
            var result = (from m in objBs.ontimeDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDelayDeliveredData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                ViewBag.DepartmentId = DepartmentId;
                ViewBag.SectionId = SectionId;
                ViewBag.MatNameId = MatNameId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustDeliveredViewModels> viewModel = new List<AdjustDeliveredViewModels>();

                //filter department
                var q = from d in objBs.ontimeDelayBs.GetAll()
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
                    AdjustDeliveredViewModels model = new AdjustDeliveredViewModels();
                    model.DeliveryNote = item.DELVNO;
                    model.CarrierId = item.CARRIER_ID;
                    model.RegionId = item.REGION_ID;
                    model.RegionName = item.REGION_NAME_TH;
                    model.Soldto = item.SOLDTO;
                    model.SoldtoName = item.SOLDTO_NAME;
                    model.Shipto = item.SHIPTO;
                    model.ShiptoName = item.TO_SHPG_LOC_NAME;
                    model.PlanDelivery = Convert.ToDateTime(item.PLNONTIMEDATE);
                    model.ActualDelivery = Convert.ToDateTime(item.ACDLVDATE);
                    viewModel.Add(model);
                }

                var ddlReason = (from r in objBs.reasonOntimeBs.GetAll()
                                 select new {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return PartialView("pv_AdjustDelivered", viewModel);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation getDelayDeliveredData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult UpdateDeliveredReason(List<String> ReasonId, List<string> txtDN, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId) {
            using (TransactionScope Trans = new TransactionScope()) {

                try {

                    int countDN = 0;
                    for (int i = 0; i < ReasonId.Count; i++) {
                        if (!String.IsNullOrEmpty(ReasonId[i])) {
                            string dn = txtDN[i];
                            string reasonId = ReasonId[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonOntimeBs.GetByID(Convert.ToInt32(reasonId)).Name;

                            DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                            ontimeDn.ON_TIME_ADJUST = 1;
                            ontimeDn.ON_TIME_ADJUST_BY = User.Identity.Name;
                            ontimeDn.ON_TIME_ADJUST_DATE = DateTime.Now;
                            ontimeDn.ON_TIME_REASON = reasonName;
                            ontimeDn.ON_TIME_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeDn.ON_TIME_REMARK = remark;

                            objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                            //delete AcceptedDelays
                            objBs.ontimeDelayBs.Delete(dn);

                            //update sum of adjust daily
                            DateTime ONTIMEDate = Convert.ToDateTime(objBs.dWH_ONTIME_DNBs.GetByID(dn).ACDLVDATE_D);

                            int id = objBs.ontimeDeliveryBs.GetAll()
                                .Where(x => x.ActualGiDate == ONTIMEDate
                                       && x.DepartmentId == DepartmentId
                                       && x.SectionId == SectionId
                                       && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                            OntimeDelivery ontimeDelivery = objBs.ontimeDeliveryBs.GetByID(id);

                            int adjONTIME = ontimeDelivery.AdjustDelivery + 1;
                            ontimeDelivery.AdjustDelivery = adjONTIME;
                            ontimeDelivery.SumOfAdjustDelivery = ontimeDelivery.OnTime + adjONTIME;
                            objBs.ontimeDeliveryBs.Update(ontimeDelivery);
                            countDN++;
                        }
                    }

                    // update sum of adjust monthly
                    int idM = objBs.ontimeDeliveryMonthBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.Month == MonthId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeDeliveryMonth ontimeDeliveryMonth = objBs.ontimeDeliveryMonthBs.GetByID(idM);

                    int adjONTIMEMonth = ontimeDeliveryMonth.AdjustDelivery + countDN;
                    ontimeDeliveryMonth.AdjustDelivery = adjONTIMEMonth;
                    ontimeDeliveryMonth.SumOfAdjustDelivery = ontimeDeliveryMonth.OnTime + adjONTIMEMonth;
                    objBs.ontimeDeliveryMonthBs.Update(ontimeDeliveryMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeDeliveryYearBs.GetAll()
                              .Where(x => x.Year == YearId
                              && x.DepartmentId == DepartmentId
                              && x.SectionId == SectionId
                              && x.MatFriGrp == MatNameId).FirstOrDefault().Id;

                    OntimeDeliveryYear ontimeDeliveryYear = objBs.ontimeDeliveryYearBs.GetByID(idY);

                    int adjONTIMEYear = ontimeDeliveryYear.AdjustDelivery + countDN;
                    ontimeDeliveryYear.AdjustDelivery = adjONTIMEYear;
                    ontimeDeliveryYear.SumOfAdjustDelivery = ontimeDeliveryYear.OnTime + adjONTIMEYear;
                    objBs.ontimeDeliveryYearBs.Update(ontimeDeliveryYear);

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