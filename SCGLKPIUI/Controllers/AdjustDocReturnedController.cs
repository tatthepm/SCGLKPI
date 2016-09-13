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

namespace SCGLKPIUI.Controllers
{
    public class AdjustDocReturnedController : BaseController
    {
        // GET: AdjustDocReturned
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

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation doc return adjust failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.docReturnDelayBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.docReturnDelayBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonDocReturnBs.GetAll().Where(x => x.IsDeleted == false)
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAdjustDocReturnTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            ViewBag.DepartmentId = DepartmentId;
            ViewBag.SectionId = SectionId;
            ViewBag.MatNameId = MatNameId;
            ViewBag.YearId = YearId;
            ViewBag.MonthId = MonthId;

            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustDocReturnedViewModels> viewModel = new List<AdjustDocReturnedViewModels>();

            //filter department
            var q = from d in objBs.docReturnDelayBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            int c = q.Count();

            foreach (var item in q)
            {
                AdjustDocReturnedViewModels model = new AdjustDocReturnedViewModels();
                model.DeliveryNote = item.DELVNO;
                model.CarrierId = item.CARRIER_ID;
                model.RegionId = item.REGION_ID;
                model.RegionName = item.REGION_NAME_TH;
                model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                model.Shipto = item.SHIPTO;
                model.ShiptoName = item.TO_SHPG_LOC_NAME;
                model.ShippingPoint = item.SHPPOINT;
                model.TruckType = item.TRUCK_TYPE;
                model.PlanDocReturn = item.PLNDOCRETDATE_SCGL.ToString();
                model.ActualDocReturn = item.DOCRETDATE_SCGL.ToString();
                model.ActualGI = item.ACTGIDATE.ToString();
                viewModel.Add(model);
            }

            var ddlReason = (from r in objBs.reasonDocReturnBs.GetAll()
                             select new
                             {
                                 Id = r.Id,
                                 Name = r.Name
                             }).Distinct().OrderBy(x => x.Name);
            ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateDocReturnReason(List<String> dynamic_select, List<string> txtDN, List<string> txtRemark, string DepartmentId, string SectionId, string MatNameId, string YearId, string MonthId)
        {
            using (TransactionScope Trans = new TransactionScope())
            {

                try
                {

                    // List<string> listSM = new List<string>();
                    int countDN = 0;
                    for (int i = 0; i < dynamic_select.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(dynamic_select[i]))
                        {
                            string dn = txtDN[i];
                            string reasonId = dynamic_select[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonDocReturnBs.GetByID(Convert.ToInt32(reasonId)).Name;
                            bool isadjust = objBs.reasonDocReturnBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;

                            DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                            ontimeDn.SCGL_DOCRET_ADJUST = isadjust ? 0 : 0;
                            ontimeDn.SCGL_DOCRET_ADJUST_BY = User.Identity.Name;
                            ontimeDn.SCGL_DOCRET_ADJUST_DATE = DateTime.Now;
                            ontimeDn.SCGL_DOCRET_REASON = reasonName;
                            ontimeDn.SCGL_DOCRET_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeDn.SCGL_DOCRET_REMARK = remark;
                            objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                            DocReturnDelay tmp_adjusted = objBs.docReturnDelayBs.GetByID(dn);
                            DocReturnAdjusted tmp_toInsert = new DocReturnAdjusted
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
                                PLNDOCRETDATE_SCGL = tmp_adjusted.PLNDOCRETDATE_SCGL,
                                PLNDOCRETDATE_SCGL_D = tmp_adjusted.PLNDOCRETDATE_SCGL_D,
                                DOCRETDATE_SCGL = tmp_adjusted.DOCRETDATE_SCGL,
                                DOCRETDATE_SCGL_D = tmp_adjusted.DOCRETDATE_SCGL_D,
                                ACTGIDATE = tmp_adjusted.ACTGIDATE,
                                ACTGIDATE_D = tmp_adjusted.ACTGIDATE_D,
                                SHPPOINT = tmp_adjusted.SHPPOINT,
                                TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                DELVNO = tmp_adjusted.DELVNO,
                                LOADED_DATE = DateTime.Now,
                                SCGL_DOCRET_ADJUST = isadjust ? 1 : 0,
                                SCGL_DOCRET_ADJUST_BY = User.Identity.Name,
                                SCGL_DOCRET_ADJUST_DATE = DateTime.Now,
                                SCGL_DOCRET_REASON = reasonName,
                                SCGL_DOCRET_REASON_ID = Convert.ToInt32(reasonId),
                                SCGL_DOCRET_REMARK = remark
                            };
                            //insert for approval
                            objBs.docReturnAdjustedBs.Insert(tmp_toInsert);
                            //delete AcceptedDelays
                            objBs.docReturnDelayBs.Delete(dn);

                            countDN++;
                        }
                    }

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countDN + "-DN is adjusted Successfully!" });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index",new { sms = "Operation update reason doc return failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
            }
        }

    }
}