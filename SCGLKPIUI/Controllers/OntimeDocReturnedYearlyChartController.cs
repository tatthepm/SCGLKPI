using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.DocReturned;

namespace SCGLKPIUI.Controllers {
    public class OntimeDocReturnedYearlyChartController : BaseController {
        // GET: OntimeDocReturnedYearlyChart
        public ActionResult Index(string DepartmentId, string SectionId, string MatNameId) {
            try {
                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlMatName = ddl.GetDropDownListDocReturnMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation docreturn failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeDocReturnYearBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeDocReturnYearBs.GetAll()
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
            List<DocReturnedOntimeChartYearlyViewModels> viewSummaryModel = new List<DocReturnedOntimeChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeDocReturnYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfDocReturn)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustDocReturn))
                                        / (double)g.Sum(x => x.SumOfDocReturn)) * 100,
                               SumOfDocReturn = g.Sum(x => x.SumOfDocReturn)
                           }).OrderBy(x => x.Year);

            foreach (var item in results.OrderBy(x => x.Year)) {
                DocReturnedOntimeChartYearlyViewModels model = new DocReturnedOntimeChartYearlyViewModels();
                model.Year = item.Year;
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfDocReturn = 500;
                model.SumOfDocReturn = item.SumOfDocReturn;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, string MatNameId) {

            //add summary data
            List<DocReturnedOntimePieChartYearlyViewModels> viewSummaryModel = new List<DocReturnedOntimePieChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeDocReturnMonthBs.GetAll()
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

            int TotalDelivery = q.Sum(x => x.SumOfDocReturn);
            var results = (from c in q
                           group c by new { c.MatFriGrp, c.MatName } into g
                           select new {
                               MatFriGrp = g.Key.MatFriGrp,
                               MatName = g.Key.MatName,
                               SumOfDocReturn = g.Sum(x => x.SumOfDocReturn),
                               Ratio = ((double)g.Sum(x => x.SumOfDocReturn) / (double)TotalDelivery) * 100
                           });

            var random = new Random();
            foreach (var item in results) {
                DocReturnedOntimePieChartYearlyViewModels model = new DocReturnedOntimePieChartYearlyViewModels();
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
        public JsonResult OntimeDocReturnedTableYearly(string DepartmentId, string SectionId, string MatNameId) {

            // add IEnumerable<AcceptOntimeMonthlyViewModels>
            List<DocReturnedOntimeYearlyViewModels> viewModel = new List<DocReturnedOntimeYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeDocReturnYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                DocReturnedOntimeYearlyViewModels model = new DocReturnedOntimeYearlyViewModels();
                model.Year = item.Year;
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfDocReturn = item.SumOfDocReturn;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustDocReturn = item.AdjustDocReturn;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfDocReturn) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustDocReturn) / (double)item.SumOfDocReturn) * 100, 2);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeDocReturnedTableSummaryYearly(string DepartmentId, string SectionId, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<DocReturnedOntimeSummaryYearlyViewModels> viewSummaryModel = new List<DocReturnedOntimeSummaryYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeDocReturnYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                               SumOfDocReturn = g.Sum(x => x.SumOfDocReturn),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustDocReturn)
                           }).OrderBy(x => x.DepartmentName);

            foreach (var item in results) {
                DocReturnedOntimeSummaryYearlyViewModels model = new DocReturnedOntimeSummaryYearlyViewModels();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.SumOfDocReturn = item.SumOfDocReturn;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfDocReturn) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfDocReturn) * 100, 2);
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);

        }
    }
}