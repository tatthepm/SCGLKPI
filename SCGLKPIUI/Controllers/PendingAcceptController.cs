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
                ViewBag.MatNameId = new SelectList(objBs.acceptPendingBs.GetByMatName(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.acceptPendingBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.acceptPendingBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PendingAcceptTableSummary(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<PendingAcceptViewModels> viewSummaryModel = new List<PendingAcceptViewModels>();

            //filter department
            var q = from d in objBs.acceptPendingBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

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