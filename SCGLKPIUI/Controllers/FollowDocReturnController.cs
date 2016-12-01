using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.DocReturned;
using System.Transactions;
namespace SCGLKPIUI.Controllers
{
    public class FollowDocReturnController : BaseController
    {
        // GET: FollowDocReturn
        public ActionResult Index(string sms, string SegmentId, string YearId, string MonthId)
        {
            try
            {

                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListDeliveryMonth("Year");
                var ddlMonth = ddl.GetDropDownListDeliveryMonth("Month");
                var ddlMatName = ddl.GetDropDownListDeliveryMonth("Matname");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.docReturnPendingBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.docReturnPendingBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FollowDocReturnTableSummary(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<FollowDocReturnViewModels> viewSummaryModel = new List<FollowDocReturnViewModels>();
            int year = Convert.ToInt32(YearId);
            int month = Convert.ToInt32(MonthId);
            //filter department
            var q = from d in objBs.docReturnPendingBs.GetAll().Where(x => x.ACTGIDATE_D.Value.Year == year && x.ACTGIDATE_D.Value.Month == month && x.DEPARTMENT_ID != null)
                    select d;

            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DEPARTMENT_ID == DepartmentId);

            //filter Section
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SECTION_ID == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MATFRIGRP == MatNameId);

            foreach (var row in q.ToList())
            {
                if ((row.DATEDIFF > 0) && (row.DATEDIFF <= 3))
                {
                    row.DATEDIFF = 3;
                }
                else if ((row.DATEDIFF > 3) && (row.DATEDIFF <= 6))
                {
                    row.DATEDIFF = 6;
                }
                else if ((row.DATEDIFF > 6) && (row.DATEDIFF <= 10))
                {
                    row.DATEDIFF = 10;
                }
                else
                {
                    row.DATEDIFF = 90;
                }
            }

            var data = from p in q
                       group p by new { p.DEPARTMENT_Name, p.SECTION_NAME }
            into g
                       select new { index = g.Key, DocReturn = g, Count = g.Count() };

            foreach (var i in data)
            {
                var dataByDept = from m in i.DocReturn
                                 group m by new { m.DATEDIFF }
                into n
                                 select new { index = n.Key.DATEDIFF, Count = n.Count() };

                FollowDocReturnViewModels model = new FollowDocReturnViewModels();

                model.Department = i.index.DEPARTMENT_Name;
                model.Section = i.index.SECTION_NAME;
                foreach (var x in dataByDept)
                {
                    if (x.index == 3)
                    {
                        model.DelayNormal = model.DelayNormal + x.Count;
                    }
                    else if (x.index == 6)
                    {
                        model.DelayAlert = model.DelayAlert + x.Count;
                    }
                    else if (x.index == 10)
                    {
                        model.DelayAlarm = model.DelayAlarm + x.Count;
                    }
                    else if (x.index == 90)
                    {
                        model.DelaySuperAlarm = model.DelaySuperAlarm + x.Count;
                        model.More10 = model.More10 + x.Count;
                    }
                    model.Total = model.Total + x.Count;
                }

                viewSummaryModel.Add(model);
            }

            //foreach (var i in data)
            //{
            //    if (i.index == 1)
            //    {
            //        foreach (var item in i.DocReturn)
            //        {
            //            PendingDocReturnedViewModels model = new PendingDocReturnedViewModels();
            //            model.Shipment = item.SHPMNTNO;
            //            model.DeliveryNote = item.DELVNO;
            //            model.RegionName = item.REGION_NAME_TH;
            //            model.SoldtoName = item.SOLDTO_NAME;
            //            model.ShiptoName = item.TO_SHPG_LOC_NAME;
            //            model.ShippingPoint = item.SHPPOINT;
            //            model.TruckType = item.TRUCK_TYPE;
            //            model.ActualGIDate = item.ACTGIDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            //            model.PlanDocReturn = item.PLNDOCRETDATE_SCGL_D.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            //            model.Delays = item.DATEDIFF.ToString();
            //            viewSummaryModel.Add(model);
            //        }
            //    }
            //}


            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FollowDocReturnByHub(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<FollowDocReturnHubViewModels> viewSummaryModel = new List<FollowDocReturnHubViewModels>();
            int year = Convert.ToInt32(YearId);
            int month = Convert.ToInt32(MonthId);
            //filter department
            List<string> hubLists = objBs.HubMastersBs.GetActive().Select(x=>x.Hub_Name).ToList();

            var q = from d in objBs.docReturnPendingBs.GetAll().Where(x => x.ACTGIDATE_D.Value.Year == year && x.ACTGIDATE_D.Value.Month == month && x.DEPARTMENT_ID != null)
                    where hubLists.Contains(d.TO_SHPG_LOC_NAME)
                    select d;

            //filter Department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DEPARTMENT_ID == DepartmentId);

            //filter Section
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SECTION_ID == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MATFRIGRP == MatNameId);

            foreach (var row in q.ToList())
            {
                if ((row.DATEDIFF > 0) && (row.DATEDIFF <= 3))
                {
                    row.DATEDIFF = 3;
                }
                else if ((row.DATEDIFF > 3) && (row.DATEDIFF <= 6))
                {
                    row.DATEDIFF = 6;
                }
                else if ((row.DATEDIFF > 6) && (row.DATEDIFF <= 10))
                {
                    row.DATEDIFF = 10;
                }
                else
                {
                    row.DATEDIFF = 90;
                }
            }

            var data = from p in q
                       group p by new { p.DEPARTMENT_Name, p.TO_SHPG_LOC_NAME }
            into g
                       select new { index = g.Key, DocReturn = g, Count = g.Count() };

            foreach (var i in data)
            {
                var dataByDept = from m in i.DocReturn
                                 group m by new { m.DATEDIFF }
                into n
                                 select new { index = n.Key.DATEDIFF, Count = n.Count() };

                FollowDocReturnHubViewModels model = new FollowDocReturnHubViewModels();

                model.Department = i.index.DEPARTMENT_Name;
                model.HubName = i.index.TO_SHPG_LOC_NAME;
                foreach (var x in dataByDept)
                {
                    if (x.index == 3)
                    {
                        model.DelayNormal = model.DelayNormal + x.Count;
                    }
                    else if (x.index == 6)
                    {
                        model.DelayAlert = model.DelayAlert + x.Count;
                    }
                    else if (x.index == 10)
                    {
                        model.DelayAlarm = model.DelayAlarm + x.Count;
                    }
                    else if (x.index == 90)
                    {
                        model.DelaySuperAlarm = model.DelaySuperAlarm + x.Count;
                        model.More10 = model.More10 + x.Count;
                    }
                    model.Total = model.Total + x.Count;
                }

                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}