﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using System.Transactions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace SCGLKPIUI.Controllers
{
    public class ApproveOutboundedController : BaseController
    {
        // GET: AdjustOutbounded
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
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

                var ddlReason = (from m in objBs.reasonOutboundBs.GetAll()
                                 select new
                                 {
                                     Id = m.Id,
                                     Name = m.Name,
                                 }).Distinct();

                ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Delivery failed " + ex.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.outboundAdjustedBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.outboundAdjustedBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonOutboundBs.GetAll()
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonApproveOutboundTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AdjustOutboundedViewModels>
            List<ApproveOutboundedViewModels> viewModel = new List<ApproveOutboundedViewModels>();

            //filter department
            var q = from d in objBs.outboundAdjustedBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            foreach (var item in q)
            {
                ApproveOutboundedViewModels model = new ApproveOutboundedViewModels();
                model.Shipment = item.SHPMNTNO;
                model.DeliveryNote = item.DELVNO;
                model.CarrierId = item.CARRIER_ID;
                model.RegionId = item.REGION_ID;
                model.RegionName = item.REGION_NAME_TH;
                model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                model.Shipto = item.SHIPTO;
                model.ShiptoName = item.TO_SHPG_LOC_NAME;
                model.ShippingPoint = item.SHPPOINT;
                model.TruckType = item.TRUCK_TYPE;
                model.PlanOutbound = item.PLNOUTBDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.ActualOutbound = item.ACDLVDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.ActualGI = item.ACTGIDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.Approve = Convert.ToBoolean(item.OUTB_ADJUST);
                model.AdjustBy = item.OUTB_ADJUST_BY;
                model.Remark = item.OUTB_REMARK;
                model.Reason = item.OUTB_REASON;
                model.thisReasonId = Convert.ToString(item.OUTB_REASON_ID);
                try
                {
                    model.FilePath = objBs.outboundFilesBs.GetByShipment(item.DELVNO).FirstOrDefault().FILEPATH;
                }
                catch(Exception)
                {
                    model.FilePath = "#";
                }
                viewModel.Add(model);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateOutboundApprove(List<string> thisReasonId, List<string> txtDN, List<string> txtApprove, List<string> txtRemark, string yearId, string monthId)
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    // List<string> listSM = new List<string>();
                    int countDN = 0;
                    List<string> DNs = new List<string>(txtApprove.Distinct());
                    foreach (string dn in DNs)
                    {
                        var reasonId = objBs.outboundAdjustedBs.GetByID(dn).OUTB_REASON_ID;
                        bool isadjust = objBs.reasonOutboundBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                        DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                        ontimeDn.OUTB_ADJUST = isadjust ? 1 : 0;

                        objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                        //delete OutboundedDelays
                        objBs.outboundAdjustedBs.Delete(dn);

                        //update sum of adjust daily
                        DateTime ONTIMEDate = Convert.ToDateTime(ontimeDn.ACTGIDATE_D);
                        string matNameId = Convert.ToString(ontimeDn.MATFRIGRP);
                        string sectionId = Convert.ToString(ontimeDn.SECTION_ID);
                        string departmentId = Convert.ToString(ontimeDn.DEPARTMENT_ID);

                        if (isadjust)
                        {

                            int id = objBs.ontimeOutboundBs.GetAll()
                                .Where(x => x.ActualGiDate == ONTIMEDate
                                       && x.DepartmentId == departmentId
                                       && x.SectionId == sectionId
                                       && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeOutbound ontimeOutbound = objBs.ontimeOutboundBs.GetByID(id);

                            int adjOntime = ontimeOutbound.AdjustOutbound + 1;
                            ontimeOutbound.AdjustOutbound = adjOntime;
                            ontimeOutbound.SumOfAdjustOutbound = ontimeOutbound.OnTime + adjOntime;
                            objBs.ontimeOutboundBs.Update(ontimeOutbound);

                            // update sum of adjust monthly
                            int idM = objBs.ontimeOutboundMonthBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.Month == monthId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeOutboundMonth ontimeOutboundMonth = objBs.ontimeOutboundMonthBs.GetByID(idM);

                            int adjOutboundMonth = ontimeOutboundMonth.AdjustOutbound + 1;
                            ontimeOutboundMonth.AdjustOutbound = adjOutboundMonth;
                            ontimeOutboundMonth.SumOfAdjustOutbound = ontimeOutboundMonth.OnTime + adjOutboundMonth;
                            objBs.ontimeOutboundMonthBs.Update(ontimeOutboundMonth);

                            // update sum of adjust yearly
                            int idY = objBs.ontimeOutboundYearBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeOutboundYear ontimeOutboundYear = objBs.ontimeOutboundYearBs.GetByID(idY);

                            int adjOutboundYear = ontimeOutboundYear.AdjustOutbound + 1;
                            ontimeOutboundYear.AdjustOutbound = adjOutboundYear;
                            ontimeOutboundYear.SumOfAdjustOutbound = ontimeOutboundYear.OnTime + adjOutboundYear;
                            objBs.ontimeOutboundYearBs.Update(ontimeOutboundYear);

                            countDN++;
                        }
                    }

                    Trans.Complete();
                    return Content(countDN + " - Delivery note is adjusted Successfully!");
                }
                catch (Exception ex)
                {
                    return Content("Operation update reason ontimed failed !" + ex.ToString());
                }
                //  return View();

            }
        }

    }
}
