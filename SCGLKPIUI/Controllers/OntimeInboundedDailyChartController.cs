using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Inbounded;

namespace SCGLKPIUI.Controllers {
    public class OntimeInboundedDailyChartController : BaseController {
        // GET: OntimeInboundedDailyChart
        public ActionResult Index(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {
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
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeInboundBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeInboundBs.GetAll()
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
            List<InboundedOntimeChartDailyViewModels> viewSummaryModel = new List<InboundedOntimeChartDailyViewModels>();

            var q = objBs.ontimeInboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

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
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfInbound)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustInbound))
                                        / (double)g.Sum(x => x.SumOfInbound)) * 100,
                               SumOfInbound = g.Sum(x => x.SumOfInbound)
                           }).OrderBy(x => x.ActualGiDate);

            foreach (var item in results.OrderBy(x => x.ActualGiDate)) {
                InboundedOntimeChartDailyViewModels model = new InboundedOntimeChartDailyViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfInbound = 500;
                model.SumOfInbound = item.SumOfInbound;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            //add summary data
            List<InboundedOntimePieChartDailyViewModels> viewSummaryModel = new List<InboundedOntimePieChartDailyViewModels>();

            var q = objBs.ontimeInboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

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
                InboundedOntimePieChartDailyViewModels model = new InboundedOntimePieChartDailyViewModels();
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
        public JsonResult OntimeInboundTableDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<OntimeAccept>
            List<InboundedOntimeViewModels> viewModel = new List<InboundedOntimeViewModels>();

            // filter by department
            // var q = objBs.ontimeInboundBs.GetAll();
            var q = objBs.ontimeInboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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

            var results = (from c in q
                           group c by new { c.ActualGiDate, c.DepartmentName, c.SectionName, c.MatName } into g
                           select new
                           {
                               ActualGiDate = g.Key.ActualGiDate,
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               MatName = g.Key.MatName,
                               Plan = 98.0,
                               SumOfInbound = ((int)g.Sum(x => x.SumOfInbound)),
                               OnTime = ((int)g.Sum(x => x.OnTime)),
                               Delay = ((int)g.Sum(x => x.Delay)),
                               AdjustInbound = ((int)g.Sum(x => x.AdjustInbound)),
                           }).OrderBy(x => x.ActualGiDate).ToList();

            foreach (var item in results)
            {
                InboundedOntimeViewModels model = new InboundedOntimeViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfInbound = item.SumOfInbound;
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
        public JsonResult OntimeInboundSummaryDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<InboundedOntimeSummaryViewModels> viewSummaryModel = new List<InboundedOntimeSummaryViewModels>();

            // filter by department
            // var q = objBs.ontimeInboundBs.GetAll();

            var q = objBs.ontimeInboundBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                               SumOfInbound = g.Sum(x => x.SumOfInbound),
                               OnTime = g.Sum(x => x.OnTime),
                               Delay = g.Sum(x => x.Delay),
                               Adjust = g.Sum(x => x.AdjustInbound)
                           }).OrderBy(x => x.DepartmentName);

            foreach (var item in results) {
                InboundedOntimeSummaryViewModels model = new InboundedOntimeSummaryViewModels();
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