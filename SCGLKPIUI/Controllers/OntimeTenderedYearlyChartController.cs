using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Tendered;

namespace SCGLKPIUI.Controllers {
    public class OntimeTenderedYearlyChartController : BaseController {
        // GET: OntimeTenderedYearlyChart
        public ActionResult Index(string SegmentId) {
            try
            {
                DropDownList ddl = new DropDownList();
                var ddlSeg = ddl.GetDropDownListSegment();
                ViewBag.SegmentId = new SelectList(ddlSeg.ToList(), "Id", "Name");
                var ddlShipPoint = ddl.GetDropDownListTenderedMonth("ShippingPoint");
                var ddlShipTo = ddl.GetDropDownListTenderedMonth("ShipTo");
                var ddlTruckType = ddl.GetDropDownListTenderedMonth("TruckType");

                ViewBag.ShipPoint = new SelectList(ddlShipPoint.ToList(), "Id", "Name");
                ViewBag.ShipTo = new SelectList(ddlShipTo.ToList(), "Id", "Name");
                ViewBag.TruckType = new SelectList(ddlTruckType.ToList(), "Id", "Name");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Tender failed " + ex.InnerException.InnerException.Message.ToString() });
            }
            return View();
        }
        public JsonResult ShiptoFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderYearBs.GetByShipto(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShippingPointFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderYearBs.GetByShipPoint(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult truckTypeFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderYearBs.GetByTruckType(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult jsonData(string SegmentId, string ShipPoint, string ShipTo, string TruckType) {

            //add summary data
            List<TenderedOntimeChartYearlyViewModels> viewSummaryModel = new List<TenderedOntimeChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName));

            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

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
                           group c by new { c.Year } into g
                           select new {
                               Year = g.Key.Year,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfTender)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustTender))
                                        / (double)g.Sum(x => x.SumOfTender)) * 100,
                               SumOfTender = g.Sum(x => x.SumOfTender)
                           }).OrderBy(x => x.Year);

            foreach (var item in results.OrderBy(x => x.Year)) {
                TenderedOntimeChartYearlyViewModels model = new TenderedOntimeChartYearlyViewModels();
                model.Year = item.Year;
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfTender = 500;
                model.SumOfTender = item.SumOfTender;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string SegmentId, string ShipPoint, string ShipTo, string TruckType) {

            //add summary data
            List<TenderedOntimePieChartYearlyViewModels> viewSummaryModel = new List<TenderedOntimePieChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderMonthBs.GetAll()
                .Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                && !String.IsNullOrEmpty(x.SectionName)
                && !String.IsNullOrEmpty(x.MatName));

            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

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
                TenderedOntimePieChartYearlyViewModels model = new TenderedOntimePieChartYearlyViewModels();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                //string[] labelArray = (item.MatName + "-" + Math.Round(item.Ratio, 2).ToString()).Split('-');
                model.value = Math.Round(item.Ratio, 2);
                model.color = color;
                model.highlight = color;
                model.label = item.MatName + "-" + Math.Round(item.Ratio, 2).ToString();
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeTenderedTableYearly(string SegmentId, string ShipPoint, string ShipTo, string TruckType) {

            // add IEnumerable<AcceptOntimeMonthlyViewModels>
            List<TenderedOntimeYearlyViewModels> viewModel = new List<TenderedOntimeYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName));
            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

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
                           group c by new { c.Year, c.Segment, c.SectionName, c.MatName } into g
                           select new
                           {
                               Year = g.Key.Year,
                               MatName = g.Key.MatName,
                               Segment = g.Key.Segment,
                               SectionName = g.Key.SectionName,
                               SumOfTender = g.Sum(x => x.SumOfTender),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               AdjustTender = g.Sum(x => x.AdjustTender)
                           }).OrderBy(x => x.Year).ThenBy(x => x.Segment);

            foreach (var item in results)
            {
                TenderedOntimeYearlyViewModels model = new TenderedOntimeYearlyViewModels();
                model.Year = item.Year;
                model.SegmentName = item.Segment;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfTender = item.SumOfTender;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Plan = 98.0;
                model.AdjustTender = item.AdjustTender;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfTender) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustTender) / (double)item.SumOfTender) * 100, 2);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeTenderedTableSummaryYearly(string SegmentId, string ShipPoint, string ShipTo, string TruckType) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<TenderedOntimeSummaryYearlyViewModels> viewSummaryModel = new List<TenderedOntimeSummaryYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeTenderYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName));

            //filter segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SubSegment == SegmentId);

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
                TenderedOntimeSummaryYearlyViewModels model = new TenderedOntimeSummaryYearlyViewModels();
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