using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Inbounded;

namespace SCGLKPIUI.Controllers {
    public class OntimeInboundedYearlyChartController : BaseController {
        // GET: OntimeInboundedYearlyChart
        public ActionResult Index(string DepartmentId, string SectionId, string MatNameId) {
            try {
                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlMatName = ddl.GetDropDownListInboundMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation inbound failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeInboundYearBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeInboundYearBs.GetAll()
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
            List<InboundedOntimeChartYearlyViewModels> viewSummaryModel = new List<InboundedOntimeChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfInbound)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustInbound))
                                        / (double)g.Sum(x => x.SumOfInbound)) * 100,
                               SumOfInbound = g.Sum(x => x.SumOfInbound)
                           }).OrderBy(x => x.Year);

            foreach (var item in results.OrderBy(x => x.Year)) {
                InboundedOntimeChartYearlyViewModels model = new InboundedOntimeChartYearlyViewModels();
                model.Year = item.Year;
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfInbound = 500;
                model.SumOfInbound = item.SumOfInbound;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, string MatNameId) {

            //add summary data
            List<InboundedOntimePieChartYearlyViewModels> viewSummaryModel = new List<InboundedOntimePieChartYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundMonthBs.GetAll()
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

            int TotalDelivery = q.Sum(x => x.SumOfInbound);
            var results = (from c in q
                           group c by new { c.MatFriGrp, c.MatName } into g
                           select new {
                               MatFriGrp = g.Key.MatFriGrp,
                               MatName = g.Key.MatName,
                               SumOfDelivery = g.Sum(x => x.SumOfInbound),
                               Ratio = ((double)g.Sum(x => x.SumOfInbound) / (double)TotalDelivery) * 100
                           });

            var random = new Random();
            foreach (var item in results) {
                InboundedOntimePieChartYearlyViewModels model = new InboundedOntimePieChartYearlyViewModels();
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
        public JsonResult OntimeInboundedTableYearly(string DepartmentId, string SectionId, string MatNameId) {

            // add IEnumerable<AcceptOntimeMonthlyViewModels>
            List<InboundedOntimeYearlyViewModels> viewModel = new List<InboundedOntimeYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                InboundedOntimeYearlyViewModels model = new InboundedOntimeYearlyViewModels();
                model.Year = item.Year;
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfInboud = item.SumOfInbound;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustInbound = item.AdjustInbound;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfInbound) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustInbound) / (double)item.SumOfInbound) * 100, 2);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeInboundedTableSummaryYearly(string DepartmentId, string SectionId, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<InboundedOntimeSummaryYearlyViewModels> viewSummaryModel = new List<InboundedOntimeSummaryYearlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundYearBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                               SumOfInbound = g.Sum(x => x.SumOfInbound),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustInbound)
                           }).OrderBy(x => x.DepartmentName);

            foreach (var item in results) {
                InboundedOntimeSummaryYearlyViewModels model = new InboundedOntimeSummaryYearlyViewModels();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.SumOfInbound = item.SumOfInbound;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfInbound) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfInbound) * 100, 2);
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);

        }

    }
}