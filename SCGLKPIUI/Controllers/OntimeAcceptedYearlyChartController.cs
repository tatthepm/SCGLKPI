using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Accepted;

namespace SCGLKPIUI.Controllers {
    public class OntimeAcceptedYearlyChartController : BaseController {
        // GET: OntimeAcceptedYearlyChart
        public ActionResult Index(string DepartmentId, string SectionId, string MatNameId) {
            try {
                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlMatName = ddl.GetDropDownListAcceptedMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeAcceptYearBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeAcceptYearBs.GetAll()
                          where m.DepartmentId == departmentId
                          && m.SectionId == sectionid
                          select new {
                              Id = m.MatFriGrp,
                              Name = m.MatName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonData(string DepartmentId, string SectionId, string MatNameId) {

            //add summary data
            List<AcceptOntimeChartYearlyViewModels> viewSummaryModel = new List<AcceptOntimeChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName));

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


            var results = (from c in q
                           group c by new { c.Year } into g
                           select new {
                               Year = g.Key.Year,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfAccept)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustAccept))
                                        / (double)g.Sum(x => x.SumOfAccept)) * 100,
                               SumOfAccept = g.Sum(x => x.SumOfAccept)
                           }).OrderBy(x => x.Year);

            foreach (var item in results.OrderBy(x => x.Year)) {
                AcceptOntimeChartYearlyViewModels model = new AcceptOntimeChartYearlyViewModels();
                model.Year = item.Year;
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfAccept = 500;
                model.SumOfAccept = item.SumOfAccept;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, string MatNameId) {

            //add summary data
            List<AcceptOntimePieChartYearlyViewModels> viewSummaryModel = new List<AcceptOntimePieChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptYearBs.GetAll()
                .Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                && !String.IsNullOrEmpty(x.SectionName)
                && !String.IsNullOrEmpty(x.MatName));

            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

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
                AcceptOntimePieChartYearlyViewModels model = new AcceptOntimePieChartYearlyViewModels();
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
        public JsonResult OntimeAcceptedTableYearly(string DepartmentId, string SectionId, string MatNameId) {

            // add IEnumerable<AcceptOntimeMonthlyViewModels>
            List<AcceptOntimeYearlyViewModels> viewModel = new List<AcceptOntimeYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName));

            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);


            foreach (var item in q.OrderBy(x => x.Year).ThenBy(x => x.DepartmentName)) {
                AcceptOntimeYearlyViewModels model = new AcceptOntimeYearlyViewModels();
                model.Year = item.Year;
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
        public JsonResult OntimeAcceptedTableSummaryYearly(string DepartmentId, string SectionId, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<AcceptOntimeSummaryYearlyViewModels> viewSummaryModel = new List<AcceptOntimeSummaryYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
                                               && !String.IsNullOrEmpty(x.SectionName)
                                               && !String.IsNullOrEmpty(x.MatName));

            //filter department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

            var results = (from c in q
                           group c by new { c.DepartmentName, c.SectionName } into g
                           select new {
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               SumOfAccept = g.Sum(x => x.SumOfAccept),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustAccept)
                           }).OrderBy(x => x.DepartmentName);

            foreach (var item in results) {
                AcceptOntimeSummaryYearlyViewModels model = new AcceptOntimeSummaryYearlyViewModels();
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