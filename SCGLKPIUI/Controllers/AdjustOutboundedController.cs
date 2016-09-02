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
                var ddlYear = ddl.GetDropDownListDeliveryMonth("Year");
                var ddlMonth = ddl.GetDropDownListDeliveryMonth("Month");
                var ddlMatName = ddl.GetDropDownListDeliveryMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation outbound adjust failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.outboundDelayBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.outboundDelayBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonOutboundBs.GetAll().Where(x => x.IsDeleted == false)
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAdjustOutboundTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            ViewBag.DepartmentId = DepartmentId;
            ViewBag.SectionId = SectionId;
            ViewBag.MatNameId = MatNameId;
            ViewBag.YearId = YearId;
            ViewBag.MonthId = MonthId;

            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustOutboundedViewModels> viewModel = new List<AdjustOutboundedViewModels>();

            //filter department
            var q = from d in objBs.outboundDelayBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            int c = q.Count();

            foreach (var item in q)
            {
                AdjustOutboundedViewModels model = new AdjustOutboundedViewModels();
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
                model.ActualGI = Convert.ToDateTime(item.ACTGIDATE);
                viewModel.Add(model);
            }

            var ddlReason = (from r in objBs.reasonOutboundBs.GetAll()
                             select new
                             {
                                 Id = r.Id,
                                 Name = r.Name
                             }).Distinct().OrderBy(x => x.Name);
            ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateOutboundReason(List<String> dynamic_select, List<string> txtDN, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId) {
            using (TransactionScope Trans = new TransactionScope()) {

                try {

                    //Write to Adjested table
                    int countDN = 0;
                    for (int i = 0; i < dynamic_select.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(dynamic_select[i]))
                        {
                            string dn = txtDN[i];
                            string reasonId = dynamic_select[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonOutboundBs.GetByID(Convert.ToInt32(reasonId)).Name;
                            bool isadjust = objBs.reasonOutboundBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;

                            DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                            ontimeDn.OUTB_ADJUST = isadjust ? 0 : 0;
                            ontimeDn.OUTB_ADJUST_BY = User.Identity.Name;
                            ontimeDn.OUTB_ADJUST_DATE = DateTime.Now;
                            ontimeDn.OUTB_REASON = reasonName;
                            ontimeDn.OUTB_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeDn.OUTB_REMARK = remark;

                            objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                            OutboundDelay tmp_adjusted = new OutboundDelay();
                            tmp_adjusted = objBs.outboundDelayBs.GetByID(dn);
                            OutboundAdjusted tmp_toInsert = new OutboundAdjusted
                            {
                                CARRIER_ID = tmp_adjusted.CARRIER_ID,
                                DEPARTMENT_ID = tmp_adjusted.DEPARTMENT_ID,
                                DEPARTMENT_Name = tmp_adjusted.DEPARTMENT_Name,
                                SECTION_ID = tmp_adjusted.SECTION_ID,
                                SECTION_NAME = tmp_adjusted.SECTION_NAME,
                                MATFRIGRP = tmp_adjusted.MATFRIGRP,
                                MATNAME = tmp_adjusted.MATNAME,
                                REGION_ID = tmp_adjusted.REGION_ID,
                                REGION_NAME_EN = tmp_adjusted.REGION_NAME_EN,
                                REGION_NAME_TH = tmp_adjusted.REGION_NAME_TH,
                                SOLDTO = tmp_adjusted.SOLDTO,
                                SOLDTO_NAME = tmp_adjusted.SOLDTO_NAME,
                                SHIPTO = tmp_adjusted.SHIPTO,
                                TO_SHPG_LOC_NAME = tmp_adjusted.TO_SHPG_LOC_NAME,
                                VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                PLNOUTBDATE = tmp_adjusted.PLNOUTBDATE,
                                PLNOUTBDATE_D = tmp_adjusted.PLNOUTBDATE_D,
                                ACDLVDATE = tmp_adjusted.ACDLVDATE,
                                ACDLVDATE_D = tmp_adjusted.ACDLVDATE_D,
                                ACTGIDATE = tmp_adjusted.ACTGIDATE,
                                ACTGIDATE_D = tmp_adjusted.ACTGIDATE_D,
                                DELVNO = tmp_adjusted.DELVNO,
                                LOADED_DATE = DateTime.Now,
                                OUTB_ADJUST = isadjust ? 1 : 0,
                                OUTB_ADJUST_BY = User.Identity.Name,
                                OUTB_ADJUST_DATE = DateTime.Now,
                                OUTB_REASON = reasonName,
                                OUTB_REASON_ID = Convert.ToInt32(reasonId),
                                OUTB_REMARK = remark
                            };
                            //insert waiting for approval
                            objBs.outboundAdjustedBs.Insert(tmp_toInsert);
                            //delete OntimeDelays
                            objBs.outboundDelayBs.Delete(dn);

                            countDN++;

                        }
                    }

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