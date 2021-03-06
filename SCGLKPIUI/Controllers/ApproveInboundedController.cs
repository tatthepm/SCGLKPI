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
    public class ApproveInboundedController : BaseController
    {
        // GET: AdjustInbounded
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

                var ddlReason = (from m in objBs.reasonInboundBs.GetAll()
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
                return RedirectToAction("Index", new { sms = "Operation Inbound failed " + ex.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.inboundAdjustedBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.inboundAdjustedBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonInboundBs.GetAll()
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonApproveInboundTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AdjustInboundedViewModels>
            List<ApproveInboundedViewModels> viewModel = new List<ApproveInboundedViewModels>();

            //filter department
            var q = from d in objBs.inboundAdjustedBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            foreach (var item in q)
            {
                ApproveInboundedViewModels model = new ApproveInboundedViewModels();
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
                model.PlanInbound = item.PLNINBDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                if (item.LTNRDDATE != null)
                {
                    model.LastTender = item.LTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }
                model.ActualGI = item.ACTGIDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.Approve = Convert.ToBoolean(item.INB_ADJUST);
                model.AdjustBy = item.INB_ADJUST_BY;
                model.Remark = item.INB_REMARK;
                model.Reason = item.INB_REASON;
                model.thisReasonId = Convert.ToString(item.INB_REASON_ID);
                try
                {
                    model.FilePath = objBs.inboundFilesBs.GetByShipment(item.DELVNO).FirstOrDefault().FILEPATH;
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
        public ActionResult UpdateInboundApprove(List<string> thisReasonId, List<string> txtDN, List<string> txtApprove, List<string> txtRemark, string yearId, string monthId)
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
                        var reasonId = objBs.inboundAdjustedBs.GetByID(dn).INB_REASON_ID;
                        bool isadjust = objBs.reasonInboundBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                        DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                        ontimeDn.INB_ADJUST = isadjust ? 1 : 0;

                        objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                        //delete InboundedDelays
                        objBs.inboundAdjustedBs.Delete(dn);

                        //update sum of adjust daily
                        DateTime ONTIMEDate = Convert.ToDateTime(ontimeDn.ACTGIDATE_D);
                        string matNameId = Convert.ToString(ontimeDn.MATFRIGRP);
                        string sectionId = Convert.ToString(ontimeDn.SECTION_ID);
                        string departmentId = Convert.ToString(ontimeDn.DEPARTMENT_ID);

                        if (isadjust)
                        {

                            int id = objBs.ontimeInboundBs.GetAll()
                                .Where(x => x.ActualGiDate == ONTIMEDate
                                       && x.DepartmentId == departmentId
                                       && x.SectionId == sectionId
                                       && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeInbound ontimeInbound = objBs.ontimeInboundBs.GetByID(id);

                            int adjOntime = ontimeInbound.AdjustInbound + 1;
                            ontimeInbound.AdjustInbound = adjOntime;
                            ontimeInbound.SumOfAdjustInbound = ontimeInbound.OnTime + adjOntime;
                            objBs.ontimeInboundBs.Update(ontimeInbound);

                            // update sum of adjust monthly
                            int idM = objBs.ontimeInboundMonthBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.Month == monthId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeInboundMonth ontimeInboundMonth = objBs.ontimeInboundMonthBs.GetByID(idM);

                            int adjInboundMonth = ontimeInboundMonth.AdjustInbound + 1;
                            ontimeInboundMonth.AdjustInbound = adjInboundMonth;
                            ontimeInboundMonth.SumOfAdjustInbound = ontimeInboundMonth.OnTime + adjInboundMonth;
                            objBs.ontimeInboundMonthBs.Update(ontimeInboundMonth);

                            // update sum of adjust yearly
                            int idY = objBs.ontimeInboundYearBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeInboundYear ontimeInboundYear = objBs.ontimeInboundYearBs.GetByID(idY);

                            int adjInboundYear = ontimeInboundYear.AdjustInbound + 1;
                            ontimeInboundYear.AdjustInbound = adjInboundYear;
                            ontimeInboundYear.SumOfAdjustInbound = ontimeInboundYear.OnTime + adjInboundYear;
                            objBs.ontimeInboundYearBs.Update(ontimeInboundYear);

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
