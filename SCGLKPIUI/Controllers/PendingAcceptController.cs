using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Accepted;
using System.Transactions;

namespace SCGLKPIUI.Controllers
{
    public class PendingAcceptController : BaseController
    {
        // GET: PendingAccept
        public ActionResult Index(string sms, string SegmentId, string YearId, string MonthId)
        {
            try
            {
                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListAcceptedMonth("Year");
                var ddlMonth = ddl.GetDropDownListAcceptedMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.acceptedPendingBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new
                                  {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public JsonResult PendingAcceptTableSummary(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<PendingAcceptViewModels> viewSummaryModel = new List<PendingAcceptViewModels>();

            // filter by department
            var q = objBs.acceptedPendingBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DEPARTMENT_Name)
                                               && !String.IsNullOrEmpty(x.SECTION_NAME)
                                               && !String.IsNullOrEmpty(x.MATNAME)
                                               && x.PLNACPDDATE_D.Value.Year == Convert.ToInt32(YearId)
                                               && x.PLNACPDDATE_D.Value.Month == Convert.ToInt32(MonthId));
            //filter department
            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DEPARTMENT_ID == DepartmentId);

            //filter Section 
            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SECTION_ID == SectionId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MATFRIGRP == MatNameId);

            foreach (var item in q)
            {
                PendingAcceptViewModels model = new PendingAcceptViewModels();
                model.Shipment = item.SHPMNTNO;
                model.RegionName = item.REGION_NAME_TH;
                model.SoldtoName = item.SOLDTO_NAME;
                model.ShiptoName = item.SHIPTO;
                model.PlanAccept = item.PLNACPDDATE_D.Value.ToString();
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}