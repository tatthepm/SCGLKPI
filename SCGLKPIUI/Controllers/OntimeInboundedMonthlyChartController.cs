﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Inbounded;

namespace SCGLKPIUI.Controllers {
    public class OntimeInboundedMonthlyChartController : BaseController {
        // GET: OntimeInboundedMonthly
        public ActionResult Index(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {
            try {
                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListInboundMonth("Year");
                var ddlMonth = ddl.GetDropDownListInboundMonth("Month");
                var ddlMatName = ddl.GetDropDownListInboundMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Inbound failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeInboundMonthBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeInboundMonthBs.GetAll()
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
            List<InboundedOntimeChartMonthlyViewModels> viewSummaryModel = new List<InboundedOntimeChartMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfInbound)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustInbound))
                                        / (double)g.Sum(x => x.SumOfInbound)) * 100,
                               SumOfInbound = g.Sum(x => x.SumOfInbound)
                           }).ToList().OrderBy(x => int.Parse(x.Month));

            foreach (var item in results) {
                InboundedOntimeChartMonthlyViewModels model = new InboundedOntimeChartMonthlyViewModels();
                model.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(item.Month));
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfInbound = 500;
                model.SumOfInbound = item.SumOfInbound;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {

            //add summary data
            List<InboundedOntimePieChartMonthlyViewModels> viewSummaryModel = new List<InboundedOntimePieChartMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundMonthBs.GetAll()
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

            int TotalInbound = q.Sum(x => x.SumOfInbound);
            var results = (from c in q
                           group c by new { c.MatFriGrp, c.MatName } into g
                           select new {
                               MatFriGrp = g.Key.MatFriGrp,
                               MatName = g.Key.MatName,
                               SumOfInbound = g.Sum(x => x.SumOfInbound),
                               Ratio = ((double)g.Sum(x => x.SumOfInbound) / (double)TotalInbound) * 100
                           });

            var random = new Random();
            foreach (var item in results) {
                InboundedOntimePieChartMonthlyViewModels model = new InboundedOntimePieChartMonthlyViewModels();
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
        public JsonResult OntimeInboundTableMonthly(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {

            // add IEnumerable<AcceptOntimeMonthlyViewModels>
            List<InboundedOntimeMonthlyViewModels> viewModel = new List<InboundedOntimeMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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

            var results = (from c in q
                           group c by new { c.Year, c.Month, c.DepartmentName, c.SectionName, c.MatName } into g
                           select new
                           {
                               Year = g.Key.Year,
                               Month = g.Key.Month,
                               MatName = g.Key.MatName,
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               SumOfInbound = g.Sum(x => x.SumOfInbound),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               AdjustInbound = g.Sum(x => x.AdjustInbound)
                           }).OrderBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => x.DepartmentName);

            foreach (var item in results)
            {
                InboundedOntimeMonthlyViewModels model = new InboundedOntimeMonthlyViewModels();
                model.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(item.Month)).ToString();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfInboud = item.SumOfInbound;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustInbound = item.AdjustInbound;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfInbound) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustInbound) / (double)item.SumOfInbound) * 100, 2);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeInboundTableSummaryMonthly(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<InboundedOntimeSummaryMonthlyViewModels> viewSummaryModel = new List<InboundedOntimeSummaryMonthlyViewModels>();

            // filter by department
            var q = objBs.ontimeInboundMonthBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                           select new {
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               SumOfInbound = g.Sum(x => x.SumOfInbound),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustInbound)
                           }).OrderBy(x => x.DepartmentName);

            foreach (var item in results) {
                InboundedOntimeSummaryMonthlyViewModels model = new InboundedOntimeSummaryMonthlyViewModels();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.SumOfInbound = item.SumOfInbound;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfInbound) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfInbound) * 100, 2);
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);

        }

    }
}