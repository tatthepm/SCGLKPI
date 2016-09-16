using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.DocReturned;

namespace SCGLKPIUI.Controllers {
    public class OntimeDocReturnedDailyChartController : BaseController {
        // GET: OntimeDocReturnedDailyChart
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
                return RedirectToAction("Index", new { sms = "Operation document return failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeDocReturnBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeDocReturnBs.GetAll()
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
            List<DocReturnedOntimeChartDailyViewModels> viewSummaryModel = new List<DocReturnedOntimeChartDailyViewModels>();

            var q = objBs.ontimeDocReturnBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

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
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) >= FromDateSearch.Value.Date && Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }

            var results = (from c in q
                           group c by new { c.ActualGiDate } into g
                           select new {
                               ActualGiDate = g.Key.ActualGiDate,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfDocReturn)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustDocReturn))
                                        / (double)g.Sum(x => x.SumOfDocReturn)) * 100,
                               SumOfDocReturn = g.Sum(x => x.SumOfDocReturn)
                           }).OrderBy(x => x.ActualGiDate);

            foreach (var item in results.OrderBy(x => x.ActualGiDate)) {
                DocReturnedOntimeChartDailyViewModels model = new DocReturnedOntimeChartDailyViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfDocReturn = 500;
                model.SumOfDocReturn = item.SumOfDocReturn;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            //add summary data
            List<DocReturnedOntimePieChartDailyViewModels> viewSummaryModel = new List<DocReturnedOntimePieChartDailyViewModels>();

            var q = objBs.ontimeDocReturnBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

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
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) >= FromDateSearch.Value.Date && Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }
            int TotalDocReturn = q.Sum(x => x.SumOfDocReturn);
            var results = (from c in q
                           group c by new { c.MatFriGrp, c.MatName } into g
                           select new {
                               MatFriGrp = g.Key.MatFriGrp,
                               MatName = g.Key.MatName,
                               SumOfDocReturn = g.Sum(x => x.SumOfDocReturn),
                               Ratio = ((double)g.Sum(x => x.SumOfDocReturn) / (double)TotalDocReturn) * 100
                           });

            var random = new Random();
            foreach (var item in results) {
                DocReturnedOntimePieChartDailyViewModels model = new DocReturnedOntimePieChartDailyViewModels();
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
        public JsonResult OntimeDocReturnTableDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<OntimeAccept>
            List<DocReturnedOntimeViewModels> viewModel = new List<DocReturnedOntimeViewModels>();

            // filter by department
            // var q = objBs.ontimeInboundBs.GetAll();
            var q = objBs.ontimeDocReturnBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) >= FromDateSearch.Value.Date && Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }

            foreach (var item in q.OrderBy(x => x.ActualGiDate)) {
                DocReturnedOntimeViewModels model = new DocReturnedOntimeViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfDocReturn = item.SumOfDocReturn;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustDocReturn = item.AdjustDocReturn;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfDocReturn) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustDocReturn) / (double)item.SumOfDocReturn) * 100, 2);
                viewModel.Add(model);
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult OntimeDocReturnSummaryDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<DocReturnedOntimeSummaryViewModels> viewSummaryModel = new List<DocReturnedOntimeSummaryViewModels>();

            // filter by department
            // var q = objBs.ontimeInboundBs.GetAll();

            var q = objBs.ontimeDocReturnBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
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
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) >= FromDateSearch.Value.Date && Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.ActualGiDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }

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
                DocReturnedOntimeSummaryViewModels model = new DocReturnedOntimeSummaryViewModels();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.SumOfDocReturn = item.SumOfDocReturn;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfDocReturn) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfDocReturn) * 100, 2);
                viewSummaryModel.Add(model);
            }
            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

    }
}