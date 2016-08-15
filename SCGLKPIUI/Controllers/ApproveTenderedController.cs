﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using System.Transactions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace SCGLKPIUI.Controllers
{
    public class ApproveTenderedController : BaseController
    {
        // GET: AdjustAccepted
        public ActionResult Index(string sms, string SegmentId, string YearId, string MonthId)
        {
            try
            {
                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlSeg = ddl.GetDropDownListSegment();
                var ddlYear = ddl.GetDropDownListTenderedMonth("Year");
                var ddlMonth = ddl.GetDropDownListTenderedMonth("Month");
                ViewBag.SegmentId = new SelectList(ddlSeg.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                var ddlReason = (from r in objBs.reasonTenderedBs.GetAll()
                                 select new
                                 {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);
                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonTenderedBs.GetAll()
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonApproveTenderTable(string SegmentId, string YearId, string MonthId)
        {
            // add IEnumerable<AdjustAcceptedViewModels>
            List<ApproveTenderedViewModels> viewModel = new List<ApproveTenderedViewModels>();

            //filter department
            var q = from d in objBs.tenderedAdjustedBs.GetByFilter(SegmentId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            foreach (var item in q)
            {
                ApproveTenderedViewModels model = new ApproveTenderedViewModels();
                model.Shipment = item.SHPMNTNO;
                model.CarrierId = item.CARRIER_ID;
                model.RegionId = item.REGION_ID;
                model.RegionName = item.REGION_NAME_TH;
                model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                model.Shipto = item.SHIPTO;
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.PlanTender = Convert.ToDateTime(item.PLNTNRDDATE);
                model.FirstTender = Convert.ToDateTime(item.FTNRDDATE);
                model.Approve = Convert.ToBoolean(item.TNRD_ADJUST);
                model.AdjustBy = item.TNRD_ADJUST_BY;
                model.Remark = item.TNRD_ONTIME_REMARK;
                model.Reason = item.TNRD_ONTIME_REASON;
                model.thisReasonId = Convert.ToString(item.TNRD_ONTIME_REASON_ID);

                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTenderApprove(List<string> thisReasonId, List<string> txtSM, List<string> txtApprove, List<string> txtRemark, string segmentId, string yearId, string monthId)
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    int countSM = 0;
                    foreach (string index in txtApprove)
                    {
                        int i = Convert.ToInt16(index);

                        string sm = txtSM[i];
                        string reasonId = thisReasonId[i];
                        string remark = txtRemark[i];
                        string reasonName = objBs.reasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).Name;
                        bool isadjust = objBs.reasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                        DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                        //Change adjustable here
                        ontimeShipment.TNRD_ADJUST = isadjust ? 1 : 0;
                        ontimeShipment.TNRD_ADJUST_BY = User.Identity.Name;
                        ontimeShipment.TNRD_ADJUST_DATE = DateTime.Now;
                        ontimeShipment.TNRD_ONTIME_REASON = reasonName;
                        ontimeShipment.TNRD_ONTIME_REASON_ID = Convert.ToInt32(reasonId);
                        ontimeShipment.TNRD_ONTIME_REMARK = remark;
                        objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                        //delete TenderedDelays
                        objBs.tenderedAdjustedBs.Delete(sm);

                        //update sum of adjust daily
                        DateTime FTNRDDate = Convert.ToDateTime(objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm).FTNRDDATE_D);

                        if (isadjust)
                        {
                            int id = objBs.ontimeTenderBs.GetAll()
                                .Where(x => x.FirstTenderDate == FTNRDDate
                                       && x.Segment == segmentId).FirstOrDefault().Id;
                            OntimeTender ontimeTender = objBs.ontimeTenderBs.GetByID(id);
                            int adjACPD = ontimeTender.AdjustTender + 1;
                            ontimeTender.AdjustTender = adjACPD;
                            ontimeTender.SumOfAdjustTender = ontimeTender.OnTime + adjACPD;
                            objBs.ontimeTenderBs.Update(ontimeTender);

                            // update sum of adjust monthly
                            int idM = objBs.ontimeTenderMonthBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.Month == monthId
                                      && x.Segment == segmentId).FirstOrDefault().Id;
                            OntimeTenderMonth ontimeTenderMonth = objBs.ontimeTenderMonthBs.GetByID(idM);
                            int adjTNRDMonth = ontimeTenderMonth.AdjustTender + 1;
                            ontimeTenderMonth.AdjustTender = adjTNRDMonth;
                            ontimeTenderMonth.SumOfAdjustTender = ontimeTenderMonth.OnTime + adjTNRDMonth;
                            objBs.ontimeTenderMonthBs.Update(ontimeTenderMonth);

                            // update sum of adjust yearly
                            int idY = objBs.ontimeTenderYearBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.Segment == segmentId).FirstOrDefault().Id;
                            OntimeTenderYear ontimeTenderYear = objBs.ontimeTenderYearBs.GetByID(idY);
                            int adjTNRDYear = ontimeTenderYear.AdjustTender + 1;
                            ontimeTenderYear.AdjustTender = adjTNRDYear;
                            ontimeTenderYear.SumOfAdjustTender = ontimeTenderYear.OnTime + adjTNRDYear;
                            objBs.ontimeTenderYearBs.Update(ontimeTenderYear);
                        }
                        countSM++;
                    }

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countSM + "-Shipment is adjusted Successfully!" });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { sms = "Operation update reason tendered failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
                //  return View();
            }
        }

    }
}
