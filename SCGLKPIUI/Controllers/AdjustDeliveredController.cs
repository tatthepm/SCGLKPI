﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Delivered;
using System.Transactions;
using BLL;
using BOL;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace SCGLKPIUI.Controllers
{
    public class AdjustDeliveredController : BaseController
    {
        // GET: AdjustDelivered
        public ActionResult Index(string sms)
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

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation doc return adjust failed " + ex.InnerException.InnerException.Message.ToString() });
            }


        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.ontimeDelayBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.ontimeDelayBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonOntimeBs.GetAll().Where(x => x.IsDeleted == false)
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAdjustOntimeTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            ViewBag.DepartmentId = DepartmentId;
            ViewBag.SectionId = SectionId;
            ViewBag.MatNameId = MatNameId;
            ViewBag.YearId = YearId;
            ViewBag.MonthId = MonthId;
            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustDeliveredViewModels> viewModel = new List<AdjustDeliveredViewModels>();

            //filter department
            var q = from d in objBs.ontimeDelayBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            //int c = q.Count();

            foreach (var item in q)
            {
                AdjustDeliveredViewModels model = new AdjustDeliveredViewModels();
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
                viewModel.Add(model);
            }

            var ddlReason = (from r in objBs.reasonOntimeBs.GetAll()
                             select new
                             {
                                 Id = r.Id,
                                 Name = r.Name
                             }).Distinct().OrderBy(x => x.Name);
            ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

            var jsonResult = Json(viewModel, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public ActionResult UpdateOntimeReason(List<String> dynamic_select, List<string> txtDN, List<string> txtRemark)
        {
            using (TransactionScope Trans = new TransactionScope())
            {

                try
                {
                    // List<string> listSM = new List<string>();
                    int countDN = 0;
                    for (int i = 0; i < dynamic_select.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(dynamic_select[i]))
                        {
                            string dn = txtDN[i];
                            string reasonId = dynamic_select[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.reasonOntimeBs.GetByID(Convert.ToInt32(reasonId)).Name;
                            bool isadjust = objBs.reasonOntimeBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;

                            DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                            ontimeDn.ON_TIME_ADJUST = isadjust ? 0 : 0;
                            ontimeDn.ON_TIME_ADJUST_BY = User.Identity.Name;
                            ontimeDn.ON_TIME_ADJUST_DATE = DateTime.Now;
                            ontimeDn.ON_TIME_REASON = reasonName;
                            ontimeDn.ON_TIME_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeDn.ON_TIME_REMARK = remark;

                            objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                            OntimeDelay tmp_adjusted = objBs.ontimeDelayBs.GetByID(dn);
                            if (tmp_adjusted == null)
                            {
                                return Json("DN " + dn + " ได้ทำการ adjust ไปแล้ว");
                            }
                            OntimeAdjusted tmp_toInsert = new OntimeAdjusted
                            {
                                CARRIER_ID = tmp_adjusted.CARRIER_ID,
                                DEPARTMENT_ID = tmp_adjusted.DEPARTMENT_ID,
                                DEPARTMENT_NAME = tmp_adjusted.DEPARTMENT_NAME,
                                SECTION_ID = tmp_adjusted.SECTION_ID,
                                SECTION_NAME = tmp_adjusted.SECTION_NAME,
                                MATFRIGRP = tmp_adjusted.MATFRIGRP,
                                MATNAME = tmp_adjusted.MATNAME,
                                REGION_ID = tmp_adjusted.REGION_ID,
                                REGION_NAME_EN = tmp_adjusted.REGION_NAME_EN,
                                REGION_NAME_TH = tmp_adjusted.REGION_NAME_TH,
                                SOLDTO = tmp_adjusted.SOLDTO,
                                SOLDTO_NAME = tmp_adjusted.SOLDTO_NAME,
                                SHIPTO = tmp_adjusted.SHIPTO,
                                SEGMENT = tmp_adjusted.SEGMENT,
                                SUBSEGMENT = tmp_adjusted.SUBSEGMENT,
                                TO_SHPG_LOC_NAME = tmp_adjusted.TO_SHPG_LOC_NAME,
                                VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                ORDCMPDATE = tmp_adjusted.ORDCMPDATE,
                                REQUESTED_DATE = tmp_adjusted.REQUESTED_DATE,
                                SHCRDATE = tmp_adjusted.SHCRDATE,
                                PLNONTIMEDATE = tmp_adjusted.PLNONTIMEDATE,
                                PLNONTIMEDATE_D = tmp_adjusted.PLNONTIMEDATE_D,
                                ACDLVDATE = tmp_adjusted.ACDLVDATE,
                                ACDLVDATE_D = tmp_adjusted.ACDLVDATE_D,
                                ACTGIDATE = tmp_adjusted.ACTGIDATE,
                                ACTGIDATE_D = tmp_adjusted.ACTGIDATE_D,
                                SHPPOINT = tmp_adjusted.SHPPOINT,
                                TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                DELVNO = tmp_adjusted.DELVNO,
                                SHPMNTNO = tmp_adjusted.SHPMNTNO,
                                LOADED_DATE = DateTime.Now,
                                ON_TIME_ADJUST = isadjust ? 1 : 0,
                                ON_TIME_ADJUST_BY = User.Identity.Name,
                                ON_TIME_ADJUST_DATE = DateTime.Now,
                                ON_TIME_REASON = reasonName,
                                ON_TIME_REASON_ID = Convert.ToInt32(reasonId),
                                ON_TIME_REMARK = remark
                            };
                            //insert waiting for approval
                            objBs.ontimeAdjustedBs.Insert(tmp_toInsert);
                            //delete AcceptedDelays
                            objBs.ontimeDelayBs.Delete(dn);

                            countDN++;
                        }
                    }

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countDN + "-DN is adjusted Successfully!" });

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { sms = "Operation update reason delivery failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
            }
        }
        public JsonResult Upload()
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        string reference = Request.Files.AllKeys[i];
                        HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                                    //Use the following properties to get file's name, size and MIMEType
                        int fileSize = file.ContentLength;
                        string fileName = file.FileName;
                        string mimeType = file.ContentType;
                        System.IO.Stream fileContent = file.InputStream;
                        //To save file, use SaveAs method
                        if (System.IO.File.Exists(Server.MapPath("~/Content/Docs/ontime/") + reference + "_" + fileName))
                        {
                            return Json("อัพโหลดไม่สำเร็จ - มีไฟล์นี้อยู่แล้ว");
                        }
                        OntimeFiles delvFiles = new OntimeFiles();
                        delvFiles.DELVNO = reference;
                        delvFiles.FILEPATH = "Content/Docs/ontime/" + reference + "_" + fileName;
                        delvFiles.LOADED_DATE = DateTime.Now;
                        delvFiles.LOADED_BY = User.Identity.Name;
                        objBs.ontimeFilesBs.Insert(delvFiles);
                        file.SaveAs(Server.MapPath("~/Content/Docs/ontime/") + reference + "_" + fileName); //File will be saved in application root
                    }
                    Trans.Complete();
                    return Json("อัพโหลดสำเร็จ " + Request.Files.Count + " ไฟล์");
                }
                catch (Exception e)
                {
                    return Json("อัพโหลดไม่สำเร็จ :: Code " + e.ToString());
                }
            }
        }

        public ContentResult UploadReason()
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    string errorRef = "";

                    string reference = Request.Files.AllKeys[i];
                    HttpPostedFileBase FileUpload = Request.Files[i]; //Uploaded file
                                                                      //Use the following properties to get file's name, size and MIMEType
                    string fileName = reference;
                    string targetpath = Server.MapPath("~/Content/Docs/ontime/");
                    FileUpload.SaveAs(targetpath + DateTime.Now.ToString("yyyyMMddHHmm", new CultureInfo("th-TH")) + "_adjust.xlsx");
                    string pathToExcelFile = targetpath + DateTime.Now.ToString("yyyyMMddHHmm", new CultureInfo("th-TH")) + "_adjust.xlsx";
                    var ext = Path.GetExtension(pathToExcelFile);

                    int countDN = 0;

                    //if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    if (ext == ".xlsx")
                    {
                        DataTable dT = ExcelModels.openExcel(pathToExcelFile, 1);
                        try
                        {
                            foreach (DataRow dr in dT.Rows)
                            {

                                //Do record adjust data - send to approval
                                if (!String.IsNullOrEmpty(dr[0].ToString()))
                                {
                                    string dn = dr[0].ToString();
                                    int reasonId = Convert.ToInt32(dr[17].ToString());
                                    string remark = dr[18].ToString();
                                    string reasonName = objBs.reasonOntimeBs.GetByID(reasonId).Name;
                                    bool isadjust = objBs.reasonOntimeBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;

                                    DWH_ONTIME_DN ontimeDn = objBs.dWH_ONTIME_DNBs.GetByID(dn);
                                    ontimeDn.ON_TIME_ADJUST = isadjust ? 0 : 0;
                                    ontimeDn.ON_TIME_ADJUST_BY = User.Identity.Name;
                                    ontimeDn.ON_TIME_ADJUST_DATE = DateTime.Now;
                                    ontimeDn.ON_TIME_REASON = reasonName;
                                    ontimeDn.ON_TIME_REASON_ID = Convert.ToInt32(reasonId);
                                    ontimeDn.ON_TIME_REMARK = remark;

                                    objBs.dWH_ONTIME_DNBs.Update(ontimeDn);

                                    OntimeDelay tmp_adjusted = objBs.ontimeDelayBs.GetByID(dn);
                                    if (tmp_adjusted == null)
                                    {
                                        errorRef = errorRef + dn + " , ";
                                    }
                                    else
                                    {
                                        OntimeAdjusted tmp_toInsert = new OntimeAdjusted
                                        {
                                            CARRIER_ID = tmp_adjusted.CARRIER_ID,
                                            DEPARTMENT_ID = tmp_adjusted.DEPARTMENT_ID,
                                            DEPARTMENT_NAME = tmp_adjusted.DEPARTMENT_NAME,
                                            SECTION_ID = tmp_adjusted.SECTION_ID,
                                            SECTION_NAME = tmp_adjusted.SECTION_NAME,
                                            MATFRIGRP = tmp_adjusted.MATFRIGRP,
                                            MATNAME = tmp_adjusted.MATNAME,
                                            REGION_ID = tmp_adjusted.REGION_ID,
                                            REGION_NAME_EN = tmp_adjusted.REGION_NAME_EN,
                                            REGION_NAME_TH = tmp_adjusted.REGION_NAME_TH,
                                            SOLDTO = tmp_adjusted.SOLDTO,
                                            SOLDTO_NAME = tmp_adjusted.SOLDTO_NAME,
                                            SHIPTO = tmp_adjusted.SHIPTO,
                                            SEGMENT = tmp_adjusted.SEGMENT,
                                            SUBSEGMENT = tmp_adjusted.SUBSEGMENT,
                                            TO_SHPG_LOC_NAME = tmp_adjusted.TO_SHPG_LOC_NAME,
                                            VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                            VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                            ORDCMPDATE = tmp_adjusted.ORDCMPDATE,
                                            REQUESTED_DATE = tmp_adjusted.REQUESTED_DATE,
                                            SHCRDATE = tmp_adjusted.SHCRDATE,
                                            PLNONTIMEDATE = tmp_adjusted.PLNONTIMEDATE,
                                            PLNONTIMEDATE_D = tmp_adjusted.PLNONTIMEDATE_D,
                                            ACDLVDATE = tmp_adjusted.ACDLVDATE,
                                            ACDLVDATE_D = tmp_adjusted.ACDLVDATE_D,
                                            ACTGIDATE = tmp_adjusted.ACTGIDATE,
                                            ACTGIDATE_D = tmp_adjusted.ACTGIDATE_D,
                                            SHPPOINT = tmp_adjusted.SHPPOINT,
                                            TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                            DELVNO = tmp_adjusted.DELVNO,
                                            SHPMNTNO = tmp_adjusted.SHPMNTNO,
                                            LOADED_DATE = DateTime.Now,
                                            ON_TIME_ADJUST = isadjust ? 1 : 0,
                                            ON_TIME_ADJUST_BY = User.Identity.Name,
                                            ON_TIME_ADJUST_DATE = DateTime.Now,
                                            ON_TIME_REASON = reasonName,
                                            ON_TIME_REASON_ID = Convert.ToInt32(reasonId),
                                            ON_TIME_REMARK = remark
                                        };
                                        //insert waiting for approval
                                        objBs.ontimeAdjustedBs.Insert(tmp_toInsert);
                                        //delete AcceptedDelays
                                        objBs.ontimeDelayBs.Delete(dn);

                                        countDN++;
                                    }
                                }
                            }
                            Trans.Complete();
                            if (errorRef != "")
                            {
                                errorRef = "<div style='overflow:auto'> DN หมายเลข " + errorRef + "ได้ทำการ adjust ไปแล้ว </div>";
                            }
                            return Content("อัพโหลดสำเร็จ " + countDN + " DN" + "<br>" + errorRef);
                        }
                        catch (Exception e)
                        {
                            return Content("อัพโหลดไม่สำเร็จ กรอกข้อมูลไม่ถูกต้อง");
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                }
            }
            return Content("อัพโหลดไม่สำเร็จ ประเภทไฟล์ไม่ถูกต้อง");
        }
        /// <summary>
        /// Dump Accepts Excels
        /// </summary>
        /// <param name="DepartmentId">String</param>
        /// <param name="SectionId">String</param>
        /// <param name="YearId">String</param>
        /// <param name="MonthId">String</param>
        /// <param name="MatNameId">String</param>
        /// <returns>ActionResult </returns>
        [HttpPost]
        public ActionResult ExportExcel(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            try
            {
                DataTable templateData = new DataTable();
                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustDeliveredViewModels> viewModel = new List<AdjustDeliveredViewModels>();

                //filter department
                var q = from d in objBs.ontimeDelayBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                        select d;

                //filter matname
                if (!String.IsNullOrEmpty(MatNameId))
                {
                    q = q.Where(x => x.MATFRIGRP == MatNameId);
                }

                //int c = q.Count();

                foreach (var item in q)
                {
                    AdjustDeliveredViewModels model = new AdjustDeliveredViewModels();
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
                    viewModel.Add(model);
                }
                templateData.Merge(ExcelModels.ToDataTable(viewModel));
                templateData.Columns.Add("Remark", typeof(string));

                ExcelModels ex = new ExcelModels();
                ex.DumpExcel(templateData, "ExportedAdjustDelivery_" + DateTime.Now.ToString("yyyyMMddHHmm", new CultureInfo("th-TH")));//dump

                return View();
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error" + e.ToString();
                return View();
            }
        }
    }
}