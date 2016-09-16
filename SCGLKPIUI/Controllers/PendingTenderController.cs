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

namespace SCGLKPIUI.Controllers
{
    public class PendingTenderController : BaseController
    {
        // GET: PendingTendered
        public ActionResult Index(string sms, string SegmentId, string YearId, string MonthId)
        {
            try
            {

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
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public JsonResult PendingTenderTableSummary(string SegmentId, string YearId, string MonthId)
        {
            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<PendingTenderViewModels> viewSummaryModel = new List<PendingTenderViewModels>();

            //filter department
            var q = from d in objBs.tenderedPendingBs.GetByFilter(SegmentId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SEGMENT == SegmentId);

            foreach (var item in q)
            {
                PendingTenderViewModels model = new PendingTenderViewModels();
                model.Shipment = item.SHPMNTNO;
                model.RegionName = item.REGION_NAME_TH;
                model.SoldtoName = item.SOLDTO_NAME;
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.ShippingPoint = item.SHPPOINT;
                model.TruckType = item.TRUCK_TYPE;
                model.PlanTender = item.PLNTNRDDATE_D.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}