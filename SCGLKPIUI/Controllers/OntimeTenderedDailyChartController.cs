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

                return View();
            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Tender failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult jsonData(string SegmentId,  DateTime? FromDateSearch, DateTime? ToDateSearch) {

            //add summary data
            List<TenderedOntimeChartDailyViewModels> viewSummaryModel = new List<TenderedOntimeChartDailyViewModels>();

            var q = objBs.ontimeTenderBs.GetAll()
                    .Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //.Where(x => x.DepartmentId == DepartmentId);

            //filter segment
            if (!string.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.Segment == SegmentId);

            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.FirstTenderDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.FirstTenderDate >= FromDateSearch && x.FirstTenderDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.FirstTenderDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.FirstTenderDate == ToDateSearch);
            }

            var results = (from c in q
                           group c by new { c.FirstTenderDate } into g
                           select new {
                               FirstTenderDate = g.Key.FirstTenderDate,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfTender)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustTender))
                                        / (double)g.Sum(x => x.SumOfTender)) * 100,
                               SumOfTender = g.Sum(x => x.SumOfTender)
                           }).OrderBy(x => x.FirstTenderDate);

            foreach (var item in results.OrderBy(x => x.FirstTenderDate)) {
                TenderedOntimeChartDailyViewModels model = new TenderedOntimeChartDailyViewModels();
                string dd = item.FirstTenderDate.Value.Day.ToString();
                string mm = item.FirstTenderDate.Value.Month.ToString();
                string yyyy = item.FirstTenderDate.Value.Year.ToString();
                model.FirstTenderDate = item.FirstTenderDate.Value.ToString("dd/MM/yyyy");
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfTender = 500;
                model.SumOfTender = item.SumOfTender;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string SegmentId, DateTime? FromDateSearch, DateTime? ToDateSearch) {

            //add summary data
            List<TenderedOntimePieChartDailyViewModels> viewSummaryModel = new List<TenderedOntimePieChartDailyViewModels>();

            var q = objBs.ontimeTenderBs.GetAll()
                    .Where(x => !String.IsNullOrEmpty(x.DepartmentName));
            //.Where(x => x.DepartmentId == DepartmentId);

            //filter segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.Segment == SegmentId);

            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.FirstTenderDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.FirstTenderDate >= FromDateSearch && x.FirstTenderDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.FirstTenderDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.FirstTenderDate == ToDateSearch);
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
        public JsonResult OntimeTenderedTableDaily(string SegmentId, DateTime? FromDateSearch, DateTime? ToDateSearch) {

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
                q = q.Where(x => x.Segment == SegmentId);

            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.FirstTenderDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.FirstTenderDate >= FromDateSearch && x.FirstTenderDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.FirstTenderDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.FirstTenderDate == ToDateSearch);
            }

            foreach (var item in q.OrderBy(x => x.FirstTenderDate)) {
                TenderedOntimeViewModels model = new TenderedOntimeViewModels();
                string dd = item.FirstTenderDate.Value.Day.ToString();
                string mm = item.FirstTenderDate.Value.Month.ToString();
                string yyyy = item.FirstTenderDate.Value.Year.ToString();
                model.FirstTenderDate = item.FirstTenderDate.Value.ToString("dd/MM/yyyy");
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
        public JsonResult OntimeTenderedSummaryDaily(string SegmentId, DateTime? FromDateSearch, DateTime? ToDateSearch) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<TenderedOntimeSummaryViewModels> viewSummaryModel = new List<TenderedOntimeSummaryViewModels>();

            // filter by department
            var q = objBs.ontimeTenderBs.GetAll()
                    .Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //filter Department
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.Segment == SegmentId);

            //filter from date, to date
            if (FromDateSearch != null && ToDateSearch != null) {
                if (FromDateSearch == ToDateSearch) {
                    q = q.Where(x => x.FirstTenderDate == FromDateSearch);
                }
                else {
                    q = q.Where(x => x.FirstTenderDate >= FromDateSearch && x.FirstTenderDate <= ToDateSearch);
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => x.FirstTenderDate == FromDateSearch);
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => x.FirstTenderDate == ToDateSearch);
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