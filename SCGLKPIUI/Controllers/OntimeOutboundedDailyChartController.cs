using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Outbounded;

namespace SCGLKPIUI.Controllers {
    public class OntimeOutboundedDailyChartController : BaseController {
        // GET: OntimeOutboundedDailyChart
        public ActionResult Index(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {
            try {
                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlMatName = ddl.GetDropDownListMatNameDaily("ontime-accepted");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation outbound failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeOutboundBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeOutboundBs.GetAll()
                          where m.DepartmentId == departmentId
                          && m.SectionId == sectionid
                          select new {
                              Id = m.MatFriGrp,
                              Name = m.MatName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonData(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            //add summary data
            List<OutboundedOntimeChartDailyViewModels> viewSummaryModel = new List<OutboundedOntimeChartDailyViewModels>();

            var q = objBs.ontimeOutboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //filter department
            if (!string.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter section
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

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
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfOutbound)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustOutbound))
                                        / (double)g.Sum(x => x.SumOfOutbound)) * 100,
                               SumOfOutbound = g.Sum(x => x.SumOfOutbound)
                           }).OrderBy(x => x.ActualGiDate);

            foreach (var item in results.OrderBy(x => x.ActualGiDate)) {
                OutboundedOntimeChartDailyViewModels model = new OutboundedOntimeChartDailyViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfOutbound = 500;
                model.SumOfOutbound = item.SumOfOutbound;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            //add summary data
            List<OutboundedOntimePieChartDailyViewModels> viewSummaryModel = new List<OutboundedOntimePieChartDailyViewModels>();

            var q = objBs.ontimeOutboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //filter department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter section
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

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
            int TotalOutbound = q.Sum(x => x.SumOfOutbound);
            var results = (from c in q
                           group c by new { c.MatFriGrp, c.MatName } into g
                           select new {
                               MatFriGrp = g.Key.MatFriGrp,
                               MatName = g.Key.MatName,
                               SumOfOutbound = g.Sum(x => x.SumOfOutbound),
                               Ratio = ((double)g.Sum(x => x.SumOfOutbound) / (double)TotalOutbound) * 100
                           });

            var random = new Random();
            foreach (var item in results) {
                OutboundedOntimePieChartDailyViewModels model = new OutboundedOntimePieChartDailyViewModels();
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
        public JsonResult OntimeOutboundedTableDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<OntimeAccept>
            List<OutboundedOntimeViewModels> viewModel = new List<OutboundedOntimeViewModels>();

            // filter by department
            var q = objBs.ontimeOutboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                              && !String.IsNullOrEmpty(x.SectionName)
                                              && !String.IsNullOrEmpty(x.MatName));

            //&& x.DepartmentId == DepartmentId);
            //int countq = q.Count();
            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId)) {
                q = q.Where(x => x.MatFriGrp == MatNameId);
            }

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

            foreach (var item in q.OrderBy(x => x.ActualGiDate)) {
                OutboundedOntimeViewModels model = new OutboundedOntimeViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfOutbound = item.SumOfOutbound;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustOutbound = item.AdjustOutbound;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfOutbound) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustOutbound) / (double)item.SumOfOutbound) * 100, 2);
                viewModel.Add(model);
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeOutboundedSummaryDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<OutboundedOntimeSummaryViewModels> viewSummaryModel = new List<OutboundedOntimeSummaryViewModels>();

            // filter by department
            var q = objBs.ontimeOutboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                                && !String.IsNullOrEmpty(x.SectionName)
                                                && !String.IsNullOrEmpty(x.MatName));

            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId)) {
                q = q.Where(x => x.MatFriGrp == MatNameId);
            }

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
                           group c by new { c.DepartmentName, c.SectionName } into g
                           select new {
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               SumOfOutbound = g.Sum(x => x.SumOfOutbound),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustOutbound)
                           }).OrderBy(x => x.DepartmentName);

            foreach (var item in results) {
                OutboundedOntimeSummaryViewModels model = new OutboundedOntimeSummaryViewModels();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.SumOfOutbound = item.SumOfOutbound;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfOutbound) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfOutbound) * 100, 2);
                viewSummaryModel.Add(model);
            }
            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}