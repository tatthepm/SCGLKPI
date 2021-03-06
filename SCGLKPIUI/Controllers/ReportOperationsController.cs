﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;

namespace SCGLKPIUI.Controllers
{
    public class ReportOperationsController : BaseController
    {
        public ActionResult Index(string DepartmentId, string SectionId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId)
        {
            try
            {
                DropDownList ddl = new DropDownList();

                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlMatName = ddl.GetDropDownListAcceptedMonth("Matname");

                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
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
            var result = (from m in objBs.OperationDailyBs.GetAll()
                          where m.DepartmentId == departmentId
                          select new
                          {
                              Id = m.SectionId,
                              Name = m.SectionName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string sectionId, string departmentId)
        {
            var result = (from m in objBs.OperationDailyBs.GetAll()
                          where m.DepartmentId == departmentId
                          && m.SectionId == sectionId
                          select new
                          {
                              Id = m.MatFriGrp,
                              Name = m.MatName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult jsonData(string DepartmentId, string SectionId, string FromDateSearch, string ToDateSearch, string MatNameId)
        {

            //add summary data
            List<ReportOperationsViewModels> viewSummaryModel = new List<ReportOperationsViewModels>();

            if (FromDateSearch != null && ToDateSearch == null)
            {
                ToDateSearch = FromDateSearch;
            }
            if (FromDateSearch == null && ToDateSearch != null)
            {
                FromDateSearch = ToDateSearch;
            }

            DateTime DateFrom = Convert.ToDateTime(FromDateSearch, new System.Globalization.CultureInfo("en-US", false).DateTimeFormat);

            DateTime DateTo = Convert.ToDateTime(ToDateSearch, new System.Globalization.CultureInfo("en-US", false).DateTimeFormat);

            var q = (from c in objBs.OperationDailyBs.GetByDate(DateFrom, DateTo) select c);

            if (!String.IsNullOrEmpty(DepartmentId))
                q = q.Where(x => x.DepartmentId == DepartmentId);

            if (!String.IsNullOrEmpty(SectionId))
                q = q.Where(x => x.SectionId == SectionId);

            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MatFriGrp == MatNameId);

            var results = (from c in q
                           group c by new { c.ActualGiDate, c.DepartmentName, c.SectionName } into g
                           select new
                           {
                               ActualGiDate = g.Key.ActualGiDate,
                               DepartmentName = g.Key.DepartmentName,
                               SectionName = g.Key.SectionName,
                               OnTimeTender = g.Sum(x => x.OnTimeTender),
                               AdjustedTender = g.Sum(x => x.AdjustedTender),
                               SumOfTender = g.Sum(x => x.SumOfTender),
                               OnTimeAccept = g.Sum(x => x.OnTimeAccept),
                               AdjustedAccept = g.Sum(x => x.AdjustedAccept),
                               SumOfAccept = g.Sum(x => x.SumOfAccept),
                               OnTimeInbound = g.Sum(x => x.OnTimeInbound),
                               AdjustedInbound = g.Sum(x => x.AdjustedInbound),
                               SumOfInbound = g.Sum(x => x.SumOfInbound),
                               OnTimeOutbound = g.Sum(x => x.OnTimeOutbound),
                               AdjustedOutbound = g.Sum(x => x.AdjustedOutbound),
                               SumOfOutbound = g.Sum(x => x.SumOfOutbound),
                               OnTimeDelivery = g.Sum(x => x.OnTimeDelivery),
                               AdjustedDelivery = g.Sum(x => x.AdjustedDelivery),
                               SumOfDelivery = g.Sum(x => x.SumOfDelivery),
                               OnTimeDocreturn = g.Sum(x => x.OnTimeDocreturn),
                               AdjustedDocreturn = g.Sum(x => x.AdjustedDocreturn),
                               SumOfDocreturn = g.Sum(x => x.SumOfDocreturn)
                           }).OrderBy(x => x.ActualGiDate);
            foreach (var item in results)
            {
                ReportOperationsViewModels model = new ReportOperationsViewModels();
                model.Department = item.DepartmentName;
                model.Section = item.SectionName;
                //model.MatName = item.MatFreight;
                model.ActualGiDate = item.ActualGiDate.ToString("dd/MM/yyyy");
                model.Plan = 98.0;
                model.OnTimeTender = item.OnTimeTender;
                model.AdjustTender = item.AdjustedTender;
                model.SumOfTender = item.SumOfTender;
                model.OnTimeAccept = item.OnTimeAccept;
                model.AdjustAccept = item.AdjustedAccept;
                model.SumOfAccept = item.SumOfAccept;
                model.OnTimeInbound = item.OnTimeInbound;
                model.AdjustInbound = item.AdjustedInbound;
                model.SumOfInbound = item.SumOfInbound;
                model.OnTimeOutbound = item.OnTimeOutbound;
                model.AdjustOutbound = item.AdjustedOutbound;
                model.SumOfOutbound = item.SumOfOutbound;
                model.OnTimeOntime = item.OnTimeDelivery;
                model.AdjustOntime = item.AdjustedDelivery;
                model.SumOfOntime = item.SumOfDelivery;
                model.OnTimeDocReturn = item.OnTimeDocreturn;
                model.AdjustDocReturn = item.AdjustedDocreturn;
                model.SumOfDocReturn = item.SumOfDocreturn;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DetailData(string DepartmentId, string SectionId, string FromDateSearch, string ToDateSearch, string MatNameId)
        {

            //add summary data
            List<ReportOperationsViewModels> viewSummaryModel = new List<ReportOperationsViewModels>();

            if (FromDateSearch != null && ToDateSearch == null)
            {
                ToDateSearch = FromDateSearch;
            }
            if (FromDateSearch == null && ToDateSearch != null)
            {
                FromDateSearch = ToDateSearch;
            }

            DateTime DateFrom = Convert.ToDateTime(FromDateSearch, new System.Globalization.CultureInfo("en-US", false).DateTimeFormat);

            DateTime DateTo = Convert.ToDateTime(ToDateSearch, new System.Globalization.CultureInfo("en-US", false).DateTimeFormat);

            var criteria = new List<Tuple<string, string>>();
            if (!String.IsNullOrEmpty(DepartmentId))
                criteria.Add(new Tuple<string, string>("DEPARTMENT_ID", DepartmentId));
            if (!String.IsNullOrEmpty(SectionId))
                criteria.Add(new Tuple<string, string>("SECTION_ID", SectionId));
            if (!String.IsNullOrEmpty(MatNameId))
                criteria.Add(new Tuple<string, string>("MATFRIGRP", MatNameId));

            IEnumerable<dynamic> DN = objBs.dWH_ONTIME_DNBs.GetByFilter(DateFrom, DateTo, criteria).ToList();

            IEnumerable<dynamic> SH = objBs.dWH_ONTIME_SHIPMENTBs.GetByFilter(DateFrom, DateTo, criteria).ToList();

            //IEnumerable<dynamic> DN = objBs.dWH_ONTIME_DNBs.GetByDate(DateFrom, DateTo).ToList();

            //IEnumerable<dynamic> SH = objBs.dWH_ONTIME_SHIPMENTBs.GetByDate(DateFrom, DateTo).ToList();

            IEnumerable<dynamic> q = (from dn in DN
                                      join sh in SH
                                      //on new { dn.DELVNO, dn.SHPMNTNO } equals new { sh.DELVNO, sh.SHPMNTNO }
                                      on dn.DELVNO equals sh.DELVNO
                                      select new
                                      {
                                          DeliveryNo = dn.DELVNO,
                                          DEPARTMENT_ID = dn.DEPARTMENT_ID,
                                          DEPARTMENT_NAME = dn.DEPARTMENT_NAME,
                                          SECTION_ID = dn.SECTION_ID,
                                          SECTION_NAME = dn.SECTION_NAME,
                                          MatFreight = dn.MATFRIGRP,

                                          OrderComplete = dn.ORDCMPDATE == null ? "No Data" : sh.ORDCMPDATE,
                                          ShipmentCreate = dn.SHCRDATE == null ? "No Data" : sh.SHCRDATE,
                                          RequestedDate = dn.REQUESTED_DATE == null ? "No Data" : sh.REQUESTED_DATE,

                                          PlanTender = sh.PLNTNRDDATE == null ? "No Data" : sh.PLNTNRDDATE,
                                          FirstTender = sh.FTNRDDATE == null ? "No Data" : sh.FTNRDDATE,
                                          LastTender = sh.LTNRDDATE == null ? "No Data" : sh.LTNRDDATE,
                                          TenderOntime = sh.TNRD_ONTIME == null ? 0 : sh.TNRD_ONTIME,
                                          TenderAdjust = sh.TNRD_ADJUST == null ? 0 : sh.TNRD_ADJUST,
                                          isTenderKPI = sh.TNRD_COUNT == null ? 0 : sh.TNRD_COUNT,

                                          PlanAccept = sh.PLNACPDDATE == null ? "No Data" : sh.PLNACPDDATE,
                                          AcceptDate = sh.LACPDDATE == null ? "No Data" : sh.LACPDDATE,
                                          AcceptOntime = sh.ACPD_ONTIME == null ? 0 : sh.ACPD_ONTIME,
                                          AcceptAdjust = sh.ACPD_ADJUST == null ? 0 : sh.ACPD_ADJUST,
                                          isAcceptKPI = sh.ACPD_COUNT == null ? 0 : sh.ACPD_COUNT,

                                          PlanInbound = dn.PLNINBDATE == null ? "No Data" : dn.PLNINBDATE,
                                          InboundDate = dn.ACTGIDATE == null ? "No Data" : dn.ACTGIDATE,
                                          InboundOntime = dn.INB_ONTIME_FLAG == null ? 0 : dn.INB_ONTIME_FLAG,
                                          InboundAdjust = dn.INB_ADJUST == null ? 0 : dn.INB_ADJUST,
                                          isInboundKPI = dn.INB_COUNT == null ? 0 : dn.INB_COUNT,

                                          PlanOutbound = dn.PLNOUTBDATE == null ? "No Data" : dn.PLNOUTBDATE,
                                          OutboundDate = dn.ACDLVDATE == null ? "No Data" : dn.ACDLVDATE,
                                          OutboundOntime = dn.OUTB_ONTIME_FLAG == null ? 0 : dn.OUTB_ONTIME_FLAG,
                                          OutboundAdjust = dn.OUTB_ADJUST == null ? 0 : dn.OUTB_ADJUST,
                                          isOutboundKPI = dn.OUTB_COUNT == null ? 0 : dn.OUTB_COUNT,

                                          PlanDelivery = dn.PLNONTIMEDATE == null ? "No Data" : dn.PLNONTIMEDATE,
                                          DeliveryDate = dn.ACDLVDATE == null ? "No Data" : dn.ACDLVDATE,
                                          DeliveryOntime = dn.ON_TIME_FLAG == null ? 0 : dn.ON_TIME_FLAG,
                                          DeliveryAdjust = dn.ON_TIME_ADJUST == null ? 0 : dn.ON_TIME_ADJUST,
                                          isDeliveryKPI = dn.ON_TIME_COUNT == null ? 0 : dn.ON_TIME_COUNT,

                                          PlanDocReturn = dn.PLNDOCRETDATE_SCGL == null ? "No Data" : dn.PLNDOCRETDATE_SCGL,
                                          DocReturnDate = dn.DOCRETDATE_SCGL == null ? "No Data" : dn.DOCRETDATE_SCGL,
                                          DocReturnOntime = dn.SCGL_DOCRET_ONTIME_FLAG == null ? 0 : dn.SCGL_DOCRET_ONTIME_FLAG,
                                          DocReturnAdjust = dn.SCGL_DOCRET_ADJUST == null ? 0 : dn.SCGL_DOCRET_ADJUST,
                                          isDocReturnKPI = dn.SCGL_DOCRET_COUNT == null ? 0 : dn.SCGL_DOCRET_COUNT,

                                          //ACTGIDATE = dn.ACTGIDATE_D
                                      }).ToList();

            return Json(q, JsonRequestBehavior.AllowGet);
        }
    }
}