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

                var ddlShipPoint = ddl.GetDropDownListTenderedMonth("ShippingPoint");
                var ddlShipTo = ddl.GetDropDownListTenderedMonth("ShipTo");
                var ddlTruckType = ddl.GetDropDownListTenderedMonth("TruckType");

                ViewBag.ShipPoint = new SelectList(ddlShipPoint.ToList(), "Id", "Name");
                ViewBag.ShipTo = new SelectList(ddlShipTo.ToList(), "Id", "Name");
                ViewBag.TruckType = new SelectList(ddlTruckType.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
        public JsonResult ShiptoFilter(string SegmentId)
        {
            return Json(objBs.tenderedPendingBs.GetByShipto(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShippingPointFilter(string SegmentId)
        {
            return Json(objBs.tenderedPendingBs.GetByShipPoint(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult truckTypeFilter(string SegmentId)
        {
            return Json(objBs.tenderedPendingBs.GetByTruckType(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PendingTenderTableSummary(string SegmentId, string YearId, string MonthId, string ShipPoint, string ShipTo, string TruckType)
        {
            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<PendingTenderViewModels> viewSummaryModel = new List<PendingTenderViewModels>();

            //filter department
            var q = from d in objBs.tenderedPendingBs.GetByFilter(SegmentId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter Shipping Point
            if (!String.IsNullOrEmpty(ShipPoint))
                q = q.Where(x => x.SHPPOINT == ShipPoint);

            //filter Shipping To
            if (!String.IsNullOrEmpty(ShipTo))
                q = q.Where(x => x.SHIPTO == ShipTo);

            //filter Truck Type
            if (!String.IsNullOrEmpty(TruckType))
                q = q.Where(x => x.TRUCK_TYPE == TruckType);

            foreach (var item in q)
            {
                PendingTenderViewModels model = new PendingTenderViewModels();
                model.Shipment = item.SHPMNTNO;
                model.RegionName = item.REGION_NAME_TH;
                model.SoldtoName = item.SOLDTO_NAME;
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.ShippingPoint = item.SHPPOINT;
                model.TruckType = item.TRUCK_TYPE;
                model.ShcrDate = item.SHCRDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.PlanTender = item.PLNTNRDDATE_D.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.Delays = item.DATEDIFF.ToString();
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}