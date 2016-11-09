using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Tendered;


namespace SCGLKPIUI.Controllers
{
    public class OntimeTenderedMonthlyChartController : BaseController
    {
        // GET: OntimeTenderedMonthlyChart
        public ActionResult Index(string SegmentId, string YearId, string MonthId)
        {
            try {
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
            }
            catch(Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Tender failed " + ex.InnerException.InnerException.Message.ToString() });
            }
            return View();
        }
        public JsonResult ShiptoFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderMonthBs.GetByShipto(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShippingPointFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderMonthBs.GetByShipPoint(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult truckTypeFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderMonthBs.GetByTruckType(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult jsonData(string SegmentId, string YearId, string MonthId, string ShipPoint, string ShipTo, string TruckType) {

            //add summary data
            List<TenderedOntimeChartMonthlyViewModels> viewSummaryModel = new List<TenderedOntimeChartMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName)
                                               && x.Year == YearId);
            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            //filter Shipping Point
            if (!String.IsNullOrEmpty(ShipPoint))
                q = q.Where(x => x.SHPPOINT == ShipPoint);

            //filter Shipping To
            if (!String.IsNullOrEmpty(ShipTo))
                q = q.Where(x => x.SHIPTO == ShipTo);

            //filter Truck Type
            if (!String.IsNullOrEmpty(TruckType))
                q = q.Where(x => x.TRUCK_TYPE == TruckType);

            var results = (from c in q
                           group c by new { c.Month } into g
                           select new {
                               Month = g.Key.Month,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfTender)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustTender))
                                        / (double)g.Sum(x => x.SumOfTender)) * 100,
                               SumOfTender = g.Sum(x => x.SumOfTender)
                           }).OrderBy(x => x.Month);

            foreach (var item in results.OrderBy(x => x.Month)) {
                TenderedOntimeChartMonthlyViewModels model = new TenderedOntimeChartMonthlyViewModels();
                model.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(item.Month));
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfTender = 500;
                model.SumOfTender = item.SumOfTender;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string SegmentId, string YearId, string MonthId, string ShipPoint, string ShipTo, string TruckType) {

            //add summary data
            List<TenderedOntimePieChartMonthlyViewModels> viewSummaryModel = new List<TenderedOntimePieChartMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderMonthBs.GetAll()
                .Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                && !String.IsNullOrEmpty(x.SectionName)
                && !String.IsNullOrEmpty(x.MatName)
                && x.Year == YearId);

            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            //filter Shipping Point
            if (!String.IsNullOrEmpty(ShipPoint))
                q = q.Where(x => x.SHPPOINT == ShipPoint);

            //filter Shipping To
            if (!String.IsNullOrEmpty(ShipTo))
                q = q.Where(x => x.SHIPTO == ShipTo);

            //filter Truck Type
            if (!String.IsNullOrEmpty(TruckType))
                q = q.Where(x => x.TRUCK_TYPE == TruckType);

            int TotalTender = q.Sum(x => x.SumOfTender);
            var results = (from c in q
                           group c by new { c.MatFriGrp, c.MatName } into g
                           select new {
                               MatFriGrp = g.Key.MatFriGrp,
                               MatName = g.Key.MatName,
                               SumOfTender = g.Sum(x => x.SumOfTender),
                               Ratio = ((double)g.Sum(x => x.SumOfTender) / (double)TotalTender) * 100
                           });

            var random = new Random();
            foreach (var item in results) {
                TenderedOntimePieChartMonthlyViewModels model = new TenderedOntimePieChartMonthlyViewModels();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"

                model.value = Math.Round(item.Ratio, 2);
                model.color = color;
                model.highlight = color;
                model.label = item.MatName + "-" + Math.Round(item.Ratio, 2).ToString();
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeTenderedTableMonthly(string SegmentId, string YearId, string MonthId, string ShipPoint, string ShipTo, string TruckType) {

            // add IEnumerable<AcceptOntimeMonthlyViewModels>
            List<TenderedOntimeMonthlyViewModels> viewModel = new List<TenderedOntimeMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName)
                                               && x.Year == YearId);
            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            //filter Shipping Point
            if (!String.IsNullOrEmpty(ShipPoint))
                q = q.Where(x => x.SHPPOINT == ShipPoint);

            //filter Shipping To
            if (!String.IsNullOrEmpty(ShipTo))
                q = q.Where(x => x.SHIPTO == ShipTo);

            //filter Truck Type
            if (!String.IsNullOrEmpty(TruckType))
                q = q.Where(x => x.TRUCK_TYPE == TruckType);

            foreach (var item in q.OrderBy(x => x.Month).ThenBy(x => x.SubSegment)) {
                TenderedOntimeMonthlyViewModels model = new TenderedOntimeMonthlyViewModels();
                model.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(item.Month)).ToString();
                model.SegmentName = item.Segment;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfTender = item.SumOfTender;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustTender = item.AdjustTender;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfTender) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustTender) / (double)item.SumOfTender) * 100, 2);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeTenderedTableSummaryMonthly(string SegmentId, string YearId, string MonthId, string ShipPoint, string ShipTo, string TruckType) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<TenderedOntimeSummaryMonthlyViewModels> viewSummaryModel = new List<TenderedOntimeSummaryMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.SubSegment)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName)
                                               && x.Year == YearId);
            //filter segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            //filter Shipping Point
            if (!String.IsNullOrEmpty(ShipPoint))
                q = q.Where(x => x.SHPPOINT == ShipPoint);

            //filter Shipping To
            if (!String.IsNullOrEmpty(ShipTo))
                q = q.Where(x => x.SHIPTO == ShipTo);

            //filter Truck Type
            if (!String.IsNullOrEmpty(TruckType))
                q = q.Where(x => x.TRUCK_TYPE == TruckType);

            var results = (from c in q
                           group c by new { c.Segment, c.SectionName } into g
                           select new {
                               SegmentId = g.Key.Segment,
                               SectionName = g.Key.SectionName,
                               SumOfTender = g.Sum(x => x.SumOfTender),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustTender)
                           }).OrderBy(x => x.SegmentId);

            foreach (var item in results) {
                TenderedOntimeSummaryMonthlyViewModels model = new TenderedOntimeSummaryMonthlyViewModels();
                model.SegmentName = item.SegmentId;
                model.SectionName = item.SectionName;
                model.SumOfTender = item.SumOfTender;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfTender) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfTender) * 100, 2);
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);

        }

    }
}