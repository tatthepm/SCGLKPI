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
    public class PendingTenderedController : BaseController {
        // GET: PendingTendered
        public ActionResult Index(string sms, string SegmentId, string YearId, string MonthId) {
            try {

                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlSeg = ddl.GetDropDownListSegment();
                var ddlYear = ddl.GetDropDownListTenderedMonth("Year");
                var ddlMonth = ddl.GetDropDownListTenderedMonth("Month");
                ViewBag.SegmentId = new SelectList(ddlSeg.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public ActionResult GetPendingTenderedData(string SegmentId, string YearId, string MonthId) {
            try {
                ViewBag.SegmentId = SegmentId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<PendingTenderedViewModels> viewModel = new List<PendingTenderedViewModels>();

                //filter department
                var q = from d in objBs.tenderedDelayBs.GetAll()
                        where d.SEGMENT == SegmentId
                        && d.PLNTNRDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.PLNTNRDDATE_D.Value.Year == Convert.ToInt32(YearId)
                        select d;

                int c = q.Count();

                foreach (var item in q) {
                    PendingTenderedViewModels model = new PendingTenderedViewModels();
                    model.Shipment = item.SHPMNTNO;
                    model.RegionId = item.REGION_ID;
                    model.RegionName = item.REGION_NAME_TH;
                    model.Soldto = item.SOLDTO;
                    model.SoldtoName = item.SOLDTO_NAME;
                    model.Shipto = item.SHIPTO;
                    model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                    model.PlanTender = Convert.ToDateTime(item.PLNTNRDDATE);
                    viewModel.Add(model);
                }

                return PartialView("pv_PendingTendered", viewModel);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation getDelayTenderedData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}