using System;
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
    public class ReportSalesController : BaseController
    {
        public ActionResult Index(string SegmentId, string CustomerId, string CarrierId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId)
        {
            try
            {
                DropDownList ddl = new DropDownList();
                var ddlSeg = ddl.GetDropDownListSegment();
                var ddlCust = ddl.GetDropDownList("Customer");
                var ddlMatName = ddl.GetDropDownListAcceptedMonth("Matname");
                ViewBag.SegmentId = new SelectList(ddlSeg.ToList(), "Id", "Name");
                ViewBag.CustomerId = new SelectList(ddlCust.ToList(), "Id", "Name");
                ViewBag.MatNameId = new SelectList(ddlMatName.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult CustomerFilter(string segmentId)
        {
            var result = (from m in objBs.ontimeDeliveryBs.GetAll()
                          where m.SubSegment == segmentId
                          select new
                          {
                              Id = m.SoldToId,
                              Name = m.SoldToName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string segmentId, string customerId)
        {
            var result = (from m in objBs.ontimeDeliveryBs.GetAll()
                          where m.SubSegment == segmentId
                          && m.SoldToId == customerId
                          select new
                          {
                              Id = m.MatFriGrp,
                              Name = m.MatName
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult jsonData(string SegmentId, string CustomerId, string FromDateSearch, string ToDateSearch, string MatNameId)
        {

            //add summary data
            List<ReportSalesViewModels> viewSummaryModel = new List<ReportSalesViewModels>();

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
            if (!String.IsNullOrEmpty(CustomerId))
                criteria.Add(new Tuple<string, string>("SOLDTO", CustomerId));
            if (!String.IsNullOrEmpty(SegmentId))
                criteria.Add(new Tuple<string, string>("DATA_SUBGRP", SegmentId));
            if (!String.IsNullOrEmpty(MatNameId))
                criteria.Add(new Tuple<string, string>("MATFRIGRP", MatNameId));

            IEnumerable<dynamic> DN = objBs.dWH_ONTIME_DNBs.GetByFilter(DateFrom, DateTo, criteria).ToList();

            IEnumerable<dynamic> SH = objBs.dWH_ONTIME_SHIPMENTBs.GetByFilter(DateFrom, DateTo,criteria).ToList();

            //DN = DN.Where(x => x.DATA_SUBGRP == SegmentId && x.SOLDTO == CustomerId);

            ////filter matname
            //if (!String.IsNullOrEmpty(MatNameId))
            //    DN = DN.Where(x => x.MATFRIGRP == MatNameId);

            //SH = SH.Where(x => x.DATA_SUBGRP == SegmentId && x.SOLDTO == CustomerId);

            ////filter matname
            //if (!String.IsNullOrEmpty(MatNameId))
            //    SH = SH.Where(x => x.MATFRIGRP == MatNameId);

            IEnumerable<dynamic> q = (from dn in DN
                                      join sh in SH on dn.DELVNO equals sh.DELVNO
                                      select new
                                      {
                                          DELVNO = dn.DELVNO,
                                          SHPMNTNO = sh.SHPMNTNO,
                                          SUB_SEGMENT = sh.DATA_SUBGRP,
                                          MATFRIGRP = dn.MATFRIGRP,
                                          SOLDTO = dn.SOLDTO,
                                          SOLDTO_NAME = dn.SOLDTO_NAME,
                                          ACPD_ONTIME = sh.ACPD_ONTIME == null ? 0 : sh.ACPD_ONTIME,
                                          ACPD_ADJUST = sh.ACPD_ADJUST == null ? 0 : sh.ACPD_ADJUST,
                                          ACPD_COUNT = sh.ACPD_COUNT == null ? 0 : sh.ACPD_COUNT,
                                          TNRD_ONTIME = sh.TNRD_ONTIME == null ? 0 : sh.TNRD_ONTIME,
                                          TNRD_ADJUST = sh.TNRD_ADJUST == null ? 0 : sh.TNRD_ADJUST,
                                          TNRD_COUNT = sh.TNRD_COUNT == null ? 0 : sh.TNRD_COUNT,
                                          INB_ONTIME = dn.INB_ONTIME_FLAG == null ? 0 : dn.INB_ONTIME_FLAG,
                                          INB_ADJUST = dn.INB_ADJUST == null ? 0 : dn.INB_ADJUST,
                                          INB_COUNT = dn.INB_COUNT == null ? 0 : dn.INB_COUNT,
                                          OUTB_ONTIME = dn.OUTB_ONTIME_FLAG == null ? 0 : dn.OUTB_ONTIME_FLAG,
                                          OUTB_ADJUST = dn.OUTB_ADJUST == null ? 0 : dn.OUTB_ADJUST,
                                          OUTB_COUNT = dn.OUTB_COUNT == null ? 0 : dn.OUTB_COUNT,
                                          ONTIME_ONTIME = dn.ON_TIME_FLAG == null ? 0 : dn.ON_TIME_FLAG,
                                          ONTIME_ADJUST = dn.ON_TIME_ADJUST == null ? 0 : dn.ON_TIME_ADJUST,
                                          ONTIME_COUNT = dn.ON_TIME_COUNT == null ? 0 : dn.ON_TIME_COUNT,
                                          SCGL_DOCRTN_ONTIME = dn.SCGL_DOCRET_ONTIME_FLAG == null ? 0 : dn.SCGL_DOCRET_ONTIME_FLAG,
                                          SCGL_DOCRTN_ADJUST = dn.SCGL_DOCRET_ADJUST == null ? 0 : dn.SCGL_DOCRET_ADJUST,
                                          SCGL_DOCRTN_COUNT = dn.SCGL_DOCRET_COUNT == null ? 0 : dn.SCGL_DOCRET_COUNT,
                                          ACTGIDATE = dn.ACTGIDATE_D
                                      }).ToList();

            var results = (from c in q
                           group c by new { c.ACTGIDATE, c.SOLDTO_NAME, c.MATFRIGRP, c.SUB_SEGMENT } into g
                           select new
                           {
                               ActualGiDate = g.Key.ACTGIDATE,
                               Segment = g.Key.SUB_SEGMENT,
                               SoldToName = g.Key.SOLDTO_NAME,
                               MatFreight = g.Key.MATFRIGRP,
                               Plan = 98.0,
                               SumOfAccept = (int)g.Sum(x => x.ACPD_COUNT == null ? 0 : x.ACPD_COUNT),
                               PcAccept_Ontime = (double)g.Sum(x => x.ACPD_COUNT) == 0 ? 0 : ((double)g.Sum(x => x.ACPD_ONTIME) / (double)g.Sum(x => x.ACPD_COUNT)) * 100,
                               PcAccept_Adjust = (double)g.Sum(x => x.ACPD_COUNT) == 0 ? 0 : (((double)g.Sum(x => x.ACPD_ADJUST) + (double)g.Sum(x => x.ACPD_ONTIME))
                                        / (double)g.Sum(x => x.ACPD_COUNT)) * 100,
                               SumOfTender = (int)g.Sum(x => x.TNRD_COUNT == null ? 0 : x.TNRD_COUNT),
                               PcTender_Ontime = (double)g.Sum(x => x.TNRD_COUNT) == 0 ? 0 : ((double)g.Sum(x => x.TNRD_ONTIME) / (double)g.Sum(x => x.TNRD_COUNT)) * 100,
                               PcTender_Adjust = (double)g.Sum(x => x.TNRD_COUNT) == 0 ? 0 : (((double)g.Sum(x => x.TNRD_ADJUST) + (double)g.Sum(x => x.TNRD_ONTIME))
                                        / (double)g.Sum(x => x.TNRD_COUNT)) * 100,
                               SumOfInbound = (int)g.Sum(x => x.INB_COUNT == null ? 0 : x.INB_COUNT),
                               PcInbound_Ontime = (double)g.Sum(x => x.INB_COUNT) == 0 ? 0 : ((double)g.Sum(x => x.INB_ONTIME) / (double)g.Sum(x => x.INB_COUNT)) * 100,
                               PcInbound_Adjust = (double)g.Sum(x => x.INB_COUNT) == 0 ? 0 : (((double)g.Sum(x => x.INB_ADJUST) + (double)g.Sum(x => x.INB_ONTIME))
                                        / (double)g.Sum(x => x.INB_COUNT)) * 100,
                               SumOfOutbound = (int)g.Sum(x => x.OUTB_COUNT == null ? 0 : x.OUTB_COUNT),
                               PcOutbound_Ontime = (double)g.Sum(x => x.OUTB_COUNT) == 0 ? 0 : ((double)g.Sum(x => x.OUTB_ONTIME) / (double)g.Sum(x => x.OUTB_COUNT)) * 100,
                               PcOutbound_Adjust = (double)g.Sum(x => x.OUTB_COUNT) == 0 ? 0 : (((double)g.Sum(x => x.OUTB_ADJUST) + (double)g.Sum(x => x.OUTB_ONTIME))
                                        / (double)g.Sum(x => x.OUTB_COUNT)) * 100,
                               SumOfDocReturn = (int)g.Sum(x => x.SCGL_DOCRTN_COUNT == null ? 0 : x.SCGL_DOCRTN_COUNT),
                               PcDocReturn_Ontime = (double)g.Sum(x => x.SCGL_DOCRTN_COUNT) == 0 ? 0 : ((double)g.Sum(x => x.SCGL_DOCRTN_ONTIME) / (double)g.Sum(x => x.SCGL_DOCRTN_COUNT)) * 100,
                               PcDocReturn_Adjust = (double)g.Sum(x => x.SCGL_DOCRTN_COUNT) == 0 ? 0 : (((double)g.Sum(x => x.SCGL_DOCRTN_ADJUST) + (double)g.Sum(x => x.SCGL_DOCRTN_ONTIME))
                                        / (double)g.Sum(x => x.SCGL_DOCRTN_COUNT)) * 100,
                               SumOfOntime = (int)g.Sum(x => x.ONTIME_COUNT == null ? 0 : x.ONTIME_COUNT),
                               PcOntime_Ontime = (double)g.Sum(x => x.ONTIME_COUNT) == 0 ? 0 : ((double)g.Sum(x => x.ONTIME_ONTIME) / (double)g.Sum(x => x.ONTIME_COUNT)) * 100,
                               PcOntime_Adjust = (double)g.Sum(x => x.ONTIME_COUNT) == 0 ? 0 : (((double)g.Sum(x => x.ONTIME_ADJUST) + (double)g.Sum(x => x.ONTIME_ONTIME))
                                        / (double)g.Sum(x => x.ONTIME_COUNT)) * 100,

                           }).OrderBy(x => x.ActualGiDate).ToList();

            foreach (var item in results)
            {
                ReportSalesViewModels model = new ReportSalesViewModels();
                model.Segment = item.Segment;
                model.Customer = item.SoldToName;
                model.MatName = item.MatFreight;
                model.ActualGiDate = item.ActualGiDate.ToString("dd/MM/yyyy");
                model.Plan = item.Plan;
                model.OnTimeTender = Math.Round(item.PcTender_Ontime, 2);
                model.AdjustTender = Math.Round(item.PcTender_Adjust, 2);
                model.SumOfTender = item.SumOfTender;
                model.OnTimeAccept = Math.Round(item.PcAccept_Ontime, 2);
                model.AdjustAccept = Math.Round(item.PcAccept_Adjust, 2);
                model.SumOfAccept = item.SumOfAccept;
                model.OnTimeInbound = Math.Round(item.PcInbound_Ontime, 2);
                model.AdjustInbound = Math.Round(item.PcInbound_Adjust, 2);
                model.SumOfInbound = item.SumOfInbound;
                model.OnTimeOutbound = Math.Round(item.PcOutbound_Ontime, 2);
                model.AdjustOutbound = Math.Round(item.PcOutbound_Adjust, 2);
                model.SumOfOutbound = item.SumOfOutbound;
                model.OnTimeOntime = Math.Round(item.PcOntime_Ontime, 2);
                model.AdjustOntime = Math.Round(item.PcOntime_Adjust, 2);
                model.SumOfOntime = item.SumOfOntime;
                model.OnTimeDocReturn = Math.Round(item.PcDocReturn_Ontime, 2);
                model.AdjustDocReturn = Math.Round(item.PcDocReturn_Adjust, 2);
                model.SumOfDocReturn = item.SumOfDocReturn;
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DetailData(string SegmentId, string CustomerId, string FromDateSearch, string ToDateSearch, string MatNameId)
        {

            //add summary data
            List<ReportSalesViewModels> viewSummaryModel = new List<ReportSalesViewModels>();

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
            if (!String.IsNullOrEmpty(CustomerId))
                criteria.Add(new Tuple<string, string>("SOLDTO", CustomerId));
            if (!String.IsNullOrEmpty(SegmentId))
                criteria.Add(new Tuple<string, string>("DATA_SUBGRP", SegmentId));
            if (!String.IsNullOrEmpty(MatNameId))
                criteria.Add(new Tuple<string, string>("MATFRIGRP", MatNameId));

            IEnumerable<dynamic> DN = objBs.dWH_ONTIME_DNBs.GetByFilter(DateFrom, DateTo, criteria).ToList();

            IEnumerable<dynamic> SH = objBs.dWH_ONTIME_SHIPMENTBs.GetByFilter(DateFrom, DateTo, criteria).ToList();

            //IEnumerable<dynamic> DN = objBs.dWH_ONTIME_DNBs.GetByDate(DateFrom, DateTo).ToList();

            //IEnumerable<dynamic> SH = objBs.dWH_ONTIME_SHIPMENTBs.GetByDate(DateFrom, DateTo).ToList();

            DN = DN.Where(x => x.DATA_SUBGRP == SegmentId && x.SOLDTO == CustomerId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                DN = DN.Where(x => x.MATFRIGRP == MatNameId);

            SH = SH.Where(x => x.DATA_SUBGRP == SegmentId && x.SOLDTO == CustomerId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                SH = SH.Where(x => x.MATFRIGRP == MatNameId);

            IEnumerable<dynamic> q = (from dn in DN
                                      join sh in SH
                                      //on new { dn.DELVNO, dn.SHPMNTNO } equals new { sh.DELVNO, sh.SHPMNTNO }
                                      on dn.DELVNO equals sh.DELVNO
                                      select new
                                      {
                                          DeliveryNo = dn.DELVNO,
                                          ShipmentNo = sh.SHPMNTNO,
                                          Segment = sh.DATA_SUBGRP,
                                          MatFreight = dn.MATFRIGRP,
                                          CustomerCode = dn.SOLDTO,
                                          CustomerName = dn.SOLDTO_NAME,

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