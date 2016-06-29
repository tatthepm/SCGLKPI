using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;

namespace SCGLKPIUI.Controllers {
    public class OntimeAcceptedMonthlyChartController : BaseController {
        // GET: OntimeAcceptedMonthlyChart
        public ActionResult Index(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListAcceptedMonth("Year");
                var ddlMonth = ddl.GetDropDownListAcceptedMonth("Month");
                var ddlMatName = ddl.GetDropDownListAcceptedMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            } catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeAcceptMonthBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeAcceptMonthBs.GetAll()
                          where m.DepartmentId == departmentId
                          && m.SectionId == sectionid
                          select new {
                              Id = m.MatFriGrp,
                              Name = m.MatName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {

            //add summary data
            List<AcceptOntimeChartMonthlyViewModels> viewSummaryModel = new List<AcceptOntimeChartMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName)
                                               && x.Year == YearId);
            //&& x.DepartmentId == DepartmentId
            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            // DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(m.Month)).ToString()
            var results = (from c in q
                           group c by new { c.Month } into g
                           select new {
                               Month = g.Key.Month,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfAccept)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustAccept))
                                        / (double)g.Sum(x => x.SumOfAccept)) * 100,
                               SumOfAccept = g.Sum(x => x.SumOfAccept)
                           }).OrderBy(x => x.Month);

            foreach (var item in results.OrderBy(x => x.Month)) {
                AcceptOntimeChartMonthlyViewModels model = new AcceptOntimeChartMonthlyViewModels();
                model.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(item.Month));
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfAccept = 500;
                model.SumOfAccept = item.SumOfAccept;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {

            //add summary data
            List<AcceptOntimePieChartMonthlyViewModels> viewSummaryModel = new List<AcceptOntimePieChartMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptMonthBs.GetAll()
                .Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                && !String.IsNullOrEmpty(x.SectionName)
                && !String.IsNullOrEmpty(x.MatName)
                && x.Year == YearId);
            //&& x.DepartmentId == DepartmentId

            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            int TotalAccept = q.Sum(x => x.SumOfAccept);
            var results = (from c in q
                           group c by new { c.MatFriGrp, c.MatName } into g
                           select new {
                               MatFriGrp = g.Key.MatFriGrp,
                               MatName = g.Key.MatName,
                               SumOfAccept = g.Sum(x => x.SumOfAccept),
                               Ratio = ((double)g.Sum(x => x.SumOfAccept) / (double)TotalAccept) * 100
                           });

            var random = new Random();
            foreach (var item in results) {
                AcceptOntimePieChartMonthlyViewModels model = new AcceptOntimePieChartMonthlyViewModels();
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
        public JsonResult OntimeAcceptTableMonthly(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {

            // add IEnumerable<AcceptOntimeMonthlyViewModels>
            List<AcceptOntimeMonthlyViewModels> viewModel = new List<AcceptOntimeMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName)
                                               && x.Year == YearId);
            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            foreach (var item in q.OrderBy(x => x.Month).ThenBy(x => x.DepartmentName))
            {
                AcceptOntimeMonthlyViewModels model = new AcceptOntimeMonthlyViewModels();
                model.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(item.Month)).ToString();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfAccept = item.SumOfAccept;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustAccept = item.AdjustAccept;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfAccept) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustAccept) / (double)item.SumOfAccept) * 100, 2);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeAcceptTableSummaryMonthly(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<AcceptOntimeSummaryMonthlyViewModels> viewSummaryModel = new List<AcceptOntimeSummaryMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName)
                                               && x.Year == YearId);
            //filter department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => x.Month == MonthId);

            var results = (from c in q
                           group c by new { c.DepartmentName, c.SectionName } into g
                           select new
                           {
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               SumOfAccept = g.Sum(x => x.SumOfAccept),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustAccept)
                           }).OrderBy(x => x.DepartmentName);

            foreach (var item in results)
            {
                AcceptOntimeSummaryMonthlyViewModels model = new AcceptOntimeSummaryMonthlyViewModels();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.SumOfAccept = item.SumOfAccept;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfAccept) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfAccept) * 100, 2);
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);

        }
    }
}