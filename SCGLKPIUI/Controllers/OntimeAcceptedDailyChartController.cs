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
    public class OntimeAcceptedDailyChartController : BaseController {
        // GET: OntimeAcceptedChart
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
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId) {
            var result = (from m in objBs.ontimeAcceptBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid) {
            var result = (from m in objBs.ontimeAcceptBs.GetAll()
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
            List<AcceptOntimeChartDailyViewModels> viewSummaryModel = new List<AcceptOntimeChartDailyViewModels>();

            var q = objBs.ontimeAcceptBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

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
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) >= FromDateSearch.Value.Date && Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }

            var results = (from c in q
                           group c by new { c.AcceptDate } into g
                           select new {
                               AcceptedDate = g.Key.AcceptDate,
                               Plan = 98.0,
                               Actual = ((double)g.Sum(x => x.OnTime) / (double)g.Sum(x => x.SumOfAccept)) * 100,
                               Adjust = (((double)g.Sum(x => x.OnTime) + (double)g.Sum(x => x.AdjustAccept))
                                        / (double)g.Sum(x => x.SumOfAccept)) * 100,
                               SumOfAccept = g.Sum(x => x.SumOfAccept)
                           }).OrderBy(x => x.AcceptedDate);

            foreach (var item in results.OrderBy(x => x.AcceptedDate)) {
                AcceptOntimeChartDailyViewModels model = new AcceptOntimeChartDailyViewModels();
                string dd = item.AcceptedDate.Value.Day.ToString();
                string mm = item.AcceptedDate.Value.Month.ToString();
                string yyyy = item.AcceptedDate.Value.Year.ToString();
                model.AcceptedDate = mm + "/" + dd + "/" + yyyy;
                model.Plan = item.Plan;
                model.Actual = Math.Round(item.Actual, 2);
                model.Adjust = Math.Round(item.Adjust, 2);
                model.PlanOfAccept = 500;
                model.SumOfAccept = item.SumOfAccept;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult jsonPieData(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            //add summary data
            List<AcceptOntimePieChartDailyViewModels> viewSummaryModel = new List<AcceptOntimePieChartDailyViewModels>();

            var q = objBs.ontimeAcceptBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

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
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) >= FromDateSearch.Value.Date && Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }
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
                AcceptOntimePieChartDailyViewModels model = new AcceptOntimePieChartDailyViewModels();
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
        public JsonResult OntimeAcceptTableDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<OntimeAccept>
            List<AcceptOntimeViewModels> viewModel = new List<AcceptOntimeViewModels>();

            // filter by department

            var q = objBs.ontimeAcceptBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //var q = objBs.ontimeAcceptBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
            //                                   && !String.IsNullOrEmpty(x.SectionName)
            //                                   && !String.IsNullOrEmpty(x.MatName));
            //&& x.DepartmentId == DepartmentId);
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
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    DateTime A1 = Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString());
                    DateTime A2 = Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString());
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) >= Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }

            foreach (var item in q.OrderBy(x => x.AcceptDate)) {
                AcceptOntimeViewModels model = new AcceptOntimeViewModels();
                string dd = item.AcceptDate.Value.Day.ToString();
                string mm = item.AcceptDate.Value.Month.ToString();
                string yyyy = item.AcceptDate.Value.Year.ToString();
                model.AcceptDate = dd + "/" + mm + "/" + yyyy;
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.MatName = item.MatName;
                model.SumOfAccept = item.SumOfAccept;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.AdjustAccept = item.AdjustAccept;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfAccept) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.AdjustAccept) / (double)item.SumOfAccept) * 100, 2);
                viewModel.Add(model);
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OntimeAcceptSummaryDaily(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId) {

            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<AcceptOntimeSummaryViewModels> viewSummaryModel = new List<AcceptOntimeSummaryViewModels>();

            // filter by department
            var q = objBs.ontimeAcceptBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName));

            //var q = objBs.ontimeAcceptBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DepartmentName)
            //                                   && !String.IsNullOrEmpty(x.SectionName)
            //                                   && !String.IsNullOrEmpty(x.MatName));

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
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
                }
                else {
                    q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) >= FromDateSearch.Value.Date && Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) <= Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
                }
            }
            if (FromDateSearch != null && ToDateSearch == null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(FromDateSearch.Value.Date.ToShortDateString()));
            }
            if (FromDateSearch == null && ToDateSearch != null) {
                q = q.Where(x => Convert.ToDateTime(x.AcceptDate.Value.Date.ToShortDateString()) == Convert.ToDateTime(ToDateSearch.Value.Date.ToShortDateString()));
            }

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
                AcceptOntimeSummaryViewModels model = new AcceptOntimeSummaryViewModels();
                model.DepartmentName = item.DepartmentName;
                model.SectionName = item.SectionName;
                model.SumOfAccept = item.SumOfAccept;
                model.OnTime = item.OnTime;
                model.Delay = item.Delay;
                model.Adjust = item.Adjust;
                model.Plan = 98.0;
                model.Percent = Math.Round(((double)item.OnTime / (double)item.SumOfAccept) * 100, 2);
                model.PercentAdjust = Math.Round((((double)item.OnTime + (double)item.Adjust) / (double)item.SumOfAccept) * 100, 2);
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}