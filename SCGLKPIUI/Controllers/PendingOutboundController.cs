using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Outbounded;
using System.Transactions;

namespace SCGLKPIUI.Controllers
{
    public class PendingOutboundController : BaseController
    {
        // GET: PendingOutbound
        public ActionResult Index(string sms, string SegmentId, string YearId, string MonthId)
        {
            try
            {

                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListOutboundMonth("Year");
                var ddlMonth = ddl.GetDropDownListOutboundMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.outboundPendingBs.GetAll()
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
                return RedirectToAction("Index", new { sms = "Operation Outbound failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            var result = (from m in objBs.acceptedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new
                          {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid)
        {
            var result = (from m in objBs.acceptedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new
                          {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PendingOutboundTableSummary(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<OutboundOntimeSummaryViewModels>
            List<PendingOutboundViewModels> viewSummaryModel = new List<PendingOutboundViewModels>();

            // filter by department
            var q = objBs.outboundPendingBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DEPARTMENT_Name)
                                               && !String.IsNullOrEmpty(x.SECTION_NAME)
                                               && !String.IsNullOrEmpty(x.MATNAME)
                                               && x.PLNOUTBDATE_D.Value.Year == Convert.ToInt32(YearId)
                                               && x.PLNOUTBDATE_D.Value.Month == Convert.ToInt32(MonthId));
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
                PendingOutboundViewModels model = new PendingOutboundViewModels();
                model.DeliveryNote = item.DELVNO;
                model.RegionName = item.REGION_NAME_TH;
                model.SoldtoName = item.SOLDTO_NAME;
                model.ShiptoName = item.SHIPTO;
                model.PlanOutbound = item.PLNOUTBDATE_D.Value.ToString();
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}