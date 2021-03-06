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
    public class ApproveDeliveredController : BaseController
    {
        // GET: AdjustOntimed
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

                var ddlReason = (from m in objBs.reasonOntimeBs.GetAll()
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
            return Json(objBs.ontimeAdjustedBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.ontimeAdjustedBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonOntimeBs.GetAll()
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonApproveOntimeTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            // add IEnumerable<AdjustDeliverededViewModels>
            List<ApproveDeliveredViewModels> viewModel = new List<ApproveDeliveredViewModels>();

            //filter department
            var q = from d in objBs.ontimeAdjustedBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            foreach (var item in q)
            {
                ApproveDeliveredViewModels model = new ApproveDeliveredViewModels();
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
                if (item.REQUESTED_DATE != null)
                {
                    model.RequestedDate = item.REQUESTED_DATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }
                else
                {
                    model.RequestedDate = "";
                }
                if (item.ORDCMPDATE != null)
                {
                    model.OrderCompDate = item.ORDCMPDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }
                else
                {
                    model.OrderCompDate = "";
                }
                if (item.SHCRDATE != null)
                {
                    model.ShcrDate = item.SHCRDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }
                else
                {
                    model.ShcrDate = "";
                }
                model.PlanDelivery = item.PLNONTIMEDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.ActualDelivery = item.ACDLVDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.ActualGI = item.ACTGIDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.Approve = Convert.ToBoolean(item.ON_TIME_ADJUST);
                model.AdjustBy = item.ON_TIME_ADJUST_BY;
                model.Remark = item.ON_TIME_REMARK;
                model.Reason = item.ON_TIME_REASON;
                model.thisReasonId = Convert.ToString(item.ON_TIME_REASON_ID);
                try
                {
                    model.FilePath = objBs.ontimeFilesBs.GetByShipment(item.DELVNO).FirstOrDefault().FILEPATH;
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
        public ActionResult UpdateOntimeApprove(List<string> thisReasonId, List<string> txtDN, List<string> txtApprove, List<string> txtRemark, string yearId, string monthId)
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
                        var reasonId = objBs.ontimeAdjustedBs.GetByID(dn).ON_TIME_REASON_ID;
                        bool isadjust = objBs.reasonOntimeBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                        DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                        ontimeDn.ON_TIME_ADJUST = isadjust ? 1 : 0;

                        objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                        //delete OntimedDelays
                        objBs.ontimeAdjustedBs.Delete(dn);

                        //update sum of adjust daily
                        DateTime ONTIMEDate = Convert.ToDateTime(ontimeDn.ACTGIDATE_D);
                        string matNameId = Convert.ToString(ontimeDn.MATFRIGRP);
                        string sectionId = Convert.ToString(ontimeDn.SECTION_ID);
                        string departmentId = Convert.ToString(ontimeDn.DEPARTMENT_ID);

                        if (isadjust)
                        {

                            int id = objBs.ontimeDeliveryBs.GetAll()
                                .Where(x => x.ActualGiDate == ONTIMEDate
                                       && x.DepartmentId == departmentId
                                       && x.SectionId == sectionId
                                       && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeDelivery ontimeDelivery = objBs.ontimeDeliveryBs.GetByID(id);

                            int adjOntime = ontimeDelivery.AdjustDelivery + 1;
                            ontimeDelivery.AdjustDelivery = adjOntime;
                            ontimeDelivery.SumOfAdjustDelivery = ontimeDelivery.OnTime + adjOntime;
                            objBs.ontimeDeliveryBs.Update(ontimeDelivery);

                            // update sum of adjust monthly
                            int idM = objBs.ontimeDeliveryMonthBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.Month == monthId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeDeliveryMonth ontimeDeliveryMonth = objBs.ontimeDeliveryMonthBs.GetByID(idM);

                            int adjOntimeMonth = ontimeDeliveryMonth.AdjustDelivery + 1;
                            ontimeDeliveryMonth.AdjustDelivery = adjOntimeMonth;
                            ontimeDeliveryMonth.SumOfAdjustDelivery = ontimeDeliveryMonth.OnTime + adjOntimeMonth;
                            objBs.ontimeDeliveryMonthBs.Update(ontimeDeliveryMonth);

                            // update sum of adjust yearly
                            int idY = objBs.ontimeDeliveryYearBs.GetAll()
                                      .Where(x => x.Year == yearId
                                      && x.DepartmentId == departmentId
                                      && x.SectionId == sectionId
                                      && x.MatFriGrp == matNameId).FirstOrDefault().Id;

                            OntimeDeliveryYear ontimeDeliveryYear = objBs.ontimeDeliveryYearBs.GetByID(idY);

                            int adjOntimeYear = ontimeDeliveryYear.AdjustDelivery + 1;
                            ontimeDeliveryYear.AdjustDelivery = adjOntimeYear;
                            ontimeDeliveryYear.SumOfAdjustDelivery = ontimeDeliveryYear.OnTime + adjOntimeYear;
                            objBs.ontimeDeliveryYearBs.Update(ontimeDeliveryYear);

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
