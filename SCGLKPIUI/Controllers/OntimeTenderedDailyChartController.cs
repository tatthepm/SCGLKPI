using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;

namespace SCGLKPIUI.Controllers {
    public class OntimeTenderedDailyChartController : BaseController {
        // GET: OntimeTenderedDailyChart
        public ActionResult Index(string SegmentId, DateTime? FromDateSearch, DateTime? ToDateSearch) {
            try {
                DropDownList ddl = new DropDownList();
                var ddlSeg = ddl.GetDropDownListSegment();
                ViewBag.SegmentId = new SelectList(ddlSeg.ToList(), "Id", "Name");
                var ddlShipPoint = ddl.GetDropDownListTenderedMonth("ShippingPoint");
                var ddlShipTo = ddl.GetDropDownListTenderedMonth("ShipTo");
                var ddlTruckType = ddl.GetDropDownListTenderedMonth("TruckType");

                ViewBag.ShipPoint = new SelectList(ddlShipPoint.ToList(), "Id", "Name");
                ViewBag.ShipTo = new SelectList(ddlShipTo.ToList(), "Id", "Name");
                ViewBag.TruckType = new SelectList(ddlTruckType.ToList(), "Id", "Name");

                return View();
            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Tender failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult ShiptoFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderBs.GetByShipto(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShippingPointFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderBs.GetByShipPoint(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult truckTypeFilter(string SegmentId)
        {
            return Json(objBs.ontimeTenderBs.GetByTruckType(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonData(string SegmentId,  DateTime? FromDateSearch, DateTime? ToDateSearch, string ShipPoint, string ShipTo, string TruckType) {

            //add summary data
            List<TenderedOntimeChartDailyViewModels> viewSummaryModel = new List<TenderedOntimeChartDailyViewModels>();

            var q = objBs.ontimeTenderBs.GetAll()
                    .Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //.Where(x => x.DepartmentId == DepartmentId);

            //filter segment
            if (!string.IsNullOrEmpty(SegmentId))
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

            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.ActualGiDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.ActualGiDate >= FromDateSearch && x.ActualGiDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.ActualGiDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.ActualGiDate == ToDateSearch);
            }

            var results = (from c in q
                           group c by new { c.ActualGiDate } into g
                           select new {
                               ActualGiDate = g.Key.ActualGiDate,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfTender)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustTender))
                                        / (double)g.Sum(x => x.SumOfTender)) * 100,
                               SumOfTender = g.Sum(x => x.SumOfTender)
                           }).OrderBy(x => x.ActualGiDate);

            foreach (var item in results.OrderBy(x => x.ActualGiDate)) {
                TenderedOntimeChartDailyViewModels model = new TenderedOntimeChartDailyViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfTender = 500;
                model.SumOfTender = item.SumOfTender;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string SegmentId, DateTime? FromDateSearch, DateTime? ToDateSearch, string ShipPoint, string ShipTo, string TruckType) {

            //add summary data
            List<TenderedOntimePieChartDailyViewModels> viewSummaryModel = new List<TenderedOntimePieChartDailyViewModels>();

            var q = objBs.ontimeTenderBs.GetAll()
                    .Where(x => !String.IsNullOrEmpty(x.DepartmentName));
            //.Where(x => x.DepartmentId == DepartmentId);

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


            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.ActualGiDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.ActualGiDate >= FromDateSearch && x.ActualGiDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.ActualGiDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.ActualGiDate == ToDateSearch);
            }
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
                TenderedOntimePieChartDailyViewModels model = new TenderedOntimePieChartDailyViewModels();
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
        public JsonResult OntimeTenderedTableDaily(string SegmentId, DateTime? FromDateSearch, DateTime? ToDateSearch, string ShipPoint, string ShipTo, string TruckType) {

            // add IEnumerable<OntimeAccept>
            List<TenderedOntimeViewModels> viewModel = new List<TenderedOntimeViewModels>();

            // filter by department
            var q = objBs.ontimeTenderBs.GetAll()
                    .Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //var q = objBs.ontimeTenderBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
            //                                   && !String.IsNullOrEmpty(x.SectionName)
            //                                   && !String.IsNullOrEmpty(x.MatName));
            //&& x.DepartmentId == DepartmentId);
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


            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.ActualGiDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.ActualGiDate >= FromDateSearch && x.ActualGiDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.ActualGiDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.ActualGiDate == ToDateSearch);
            }

            var results = (from c in q
                           group c by new { c.ActualGiDate, c.DepartmentName, c.SectionName, c.MatName, c.Segment } into g
                           select new
                           {
                               ActualGiDate = g.Key.ActualGiDate,
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               MatName = g.Key.MatName,
                               Segment = g.Key.Segment,
                               Plan = 98.0,
                               SumOfTender = ((int)g.Sum(x => x.SumOfTender)),
                               OnTime = ((int)g.Sum(x => x.OnTime)),
                               Delay = ((int)g.Sum(x => x.Delay)),
                               AdjustTender = ((int)g.Sum(x => x.AdjustTender)),
                           }).OrderBy(x => x.ActualGiDate).ToList();

            foreach (var item in results)
            {
                TenderedOntimeViewModels model = new TenderedOntimeViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
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
        public JsonResult OntimeTenderedSummaryDaily(string SegmentId, DateTime? FromDateSearch, DateTime? ToDateSearch, string ShipPoint, string ShipTo, string TruckType) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<TenderedOntimeSummaryViewModels> viewSummaryModel = new List<TenderedOntimeSummaryViewModels>();

            // filter by department
            var q = objBs.ontimeTenderBs.GetAll()
                    .Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //filter Department
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

            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.ActualGiDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.ActualGiDate >= FromDateSearch && x.ActualGiDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.ActualGiDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.ActualGiDate == ToDateSearch);
            }

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
                TenderedOntimeSummaryViewModels model = new TenderedOntimeSummaryViewModels();
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