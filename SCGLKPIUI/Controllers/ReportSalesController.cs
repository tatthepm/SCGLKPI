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
        public JsonResult jsonData(string SegmentId, string CustomerId, DateTime? FromDateSearch, DateTime? ToDateSearch, string MatNameId)
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

            var DN = objBs.dWH_ONTIME_DNBs.GetByDate(FromDateSearch, ToDateSearch);

            var SH = objBs.dWH_ONTIME_SHIPMENTBs.GetByDate(FromDateSearch, ToDateSearch);

            DN = DN.Where(x => x.DATA_SUBGRP == SegmentId && x.SOLDTO == CustomerId);

            SH = SH.Where(x => x.DATA_SUBGRP == SegmentId && x.SOLDTO == CustomerId);

            var q = (from dn in DN
                     join sh in SH on dn.DELVNO equals sh.DELVNO
                     select new
                     {
                         DELVNO = dn.DELVNO,
                         SHPMNTNO = sh.SHPMNTNO,
                         SUB_SEGMENT = sh.DATA_SUBGRP,
                         MATFRIGRP = dn.MATFRIGRP,
                         SOLDTO = dn.SOLDTO,
                         SOLDTO_NAME = dn.SOLDTO_NAME,
                         ACPD_ONTIME = sh.ACPD_ONTIME,
                         ACPD_ADJUST = sh.ACPD_ADJUST,
                         ACPD_COUNT = sh.ACPD_COUNT,
                         TNRD_ONTIME = sh.TNRD_ONTIME,
                         TNRD_ADJUST = sh.TNRD_ADJUST,
                         TNRD_COUNT = sh.TNRD_COUNT,
                         INB_ONTIME = dn.INB_ONTIME_FLAG,
                         INB_ADJUST = dn.INB_ADJUST,
                         INB_COUNT = dn.INB_COUNT,
                         OUTB_ONTIME = dn.OUTB_ONTIME_FLAG,
                         OUTB_ADJUST = dn.OUTB_ADJUST,
                         OUTB_COUNT = dn.OUTB_COUNT,
                         ONTIME_ONTIME = dn.ON_TIME_FLAG,
                         ONTIME_ADJUST = dn.ON_TIME_ADJUST,
                         ONTIME_COUNT = dn.ON_TIME_COUNT,
                         SCGL_DOCRTN_ONTIME = dn.SCGL_DOCRET_ONTIME_FLAG,
                         SCGL_DOCRTN_ADJUST = dn.SCGL_DOCRET_ADJUST,
                         SCGL_DOCRTN_COUNT = dn.SCGL_DOCRET_COUNT,
                         ACTGIDATE = dn.ACTGIDATE_D
                     }).Take(50000);

            //filter customer
            if (!string.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SUB_SEGMENT == SegmentId);

            //filter customer
            if (!string.IsNullOrEmpty(CustomerId))
                q = q.Where(x => x.SOLDTO == CustomerId);

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
                q = q.Where(x => x.MATFRIGRP == MatNameId);

            var results = (from c in q
                           group c by new { c.ACTGIDATE,c.SOLDTO_NAME,c.MATFRIGRP,c.SUB_SEGMENT } into g
                           select new
                           {
                               ActualGiDate = g.Key.ACTGIDATE,
                               Segment = g.Key.SUB_SEGMENT,
                               SoldToName = g.Key.SOLDTO_NAME,
                               MatFreight = g.Key.MATFRIGRP,
                               Plan = 98.0,
                               PcAccept_Ontime = ((double)g.Sum(x => x.ACPD_ONTIME) / (double)g.Sum(x => x.ACPD_COUNT)) * 100,
                               PcAccept_Adjust = (((double)g.Sum(x => x.ACPD_ADJUST) + (double)g.Sum(x => x.ACPD_ONTIME))
                                        / (double)g.Sum(x => x.ACPD_COUNT)) * 100,
                               SumOfAccept = (int)g.Sum(x => x.ACPD_COUNT == null ? 0 : x.ACPD_COUNT),
                               PcTender_Ontime = ((double)g.Sum(x => x.TNRD_ONTIME) / (double)g.Sum(x => x.TNRD_COUNT)) * 100,
                               PcTender_Adjust = (((double)g.Sum(x => x.TNRD_ADJUST) + (double)g.Sum(x => x.TNRD_ONTIME))
                                        / (double)g.Sum(x => x.TNRD_COUNT)) * 100,
                               SumOfTender = (int)g.Sum(x => x.TNRD_COUNT == null ? 0 : x.TNRD_COUNT ),
                               PcInbound_Ontime = ((double)g.Sum(x => x.INB_ONTIME) / (double)g.Sum(x => x.INB_COUNT)) * 100,
                               PcInbound_Adjust = (((double)g.Sum(x => x.INB_ADJUST) + (double)g.Sum(x => x.INB_ONTIME))
                                        / (double)g.Sum(x => x.INB_COUNT)) * 100,
                               SumOfInbound = (int)g.Sum(x => x.INB_COUNT == null ? 0 : x.INB_COUNT),
                               PcOutbound_Ontime = ((double)g.Sum(x => x.OUTB_ONTIME) / (double)g.Sum(x => x.OUTB_COUNT)) * 100,
                               PcOutbound_Adjust = (((double)g.Sum(x => x.OUTB_ADJUST) + (double)g.Sum(x => x.OUTB_ONTIME))
                                        / (double)g.Sum(x => x.OUTB_COUNT)) * 100,
                               SumOfOutbound = (int)g.Sum(x => x.OUTB_COUNT == null ? 0 : x.OUTB_COUNT),
                               PcDocReturn_Ontime = ((double)g.Sum(x => x.SCGL_DOCRTN_ONTIME) / (double)g.Sum(x => x.SCGL_DOCRTN_COUNT)) * 100,
                               PcDocReturn_Adjust = (((double)g.Sum(x => x.SCGL_DOCRTN_ADJUST) + (double)g.Sum(x => x.SCGL_DOCRTN_ONTIME))
                                        / (double)g.Sum(x => x.SCGL_DOCRTN_COUNT)) * 100,
                               SumOfDocReturn = (int)g.Sum(x => x.SCGL_DOCRTN_COUNT == null ? 0 : x.SCGL_DOCRTN_COUNT),
                               PcOntime_Ontime = ((double)g.Sum(x => x.ONTIME_ONTIME) / (double)g.Sum(x => x.ONTIME_COUNT)) * 100,
                               PcOntime_Adjust = (((double)g.Sum(x => x.ONTIME_ADJUST) + (double)g.Sum(x => x.ONTIME_ONTIME))
                                        / (double)g.Sum(x => x.ONTIME_COUNT)) * 100,
                               SumOfOntime = (int)g.Sum(x => x.ONTIME_COUNT == null ? 0 : x.ONTIME_COUNT),
                           }).OrderBy(x => x.ActualGiDate);

            foreach (var item in results)
            {
                ReportSalesViewModels model = new ReportSalesViewModels();
                string dd = item.ActualGiDate.Value.Day.ToString();
                string mm = item.ActualGiDate.Value.Month.ToString();
                string yyyy = item.ActualGiDate.Value.Year.ToString();
                model.Segment = item.Segment;
                model.Customer = item.SoldToName;
                model.MatName = item.MatFreight;
                model.ActualGiDate = item.ActualGiDate.Value.ToString("dd/MM/yyyy");
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
    }
}