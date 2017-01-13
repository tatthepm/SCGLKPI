using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using SCGLKPIUI.Models.Tendered;
using System.Transactions;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace SCGLKPIUI.Controllers
{
    public class AdjustTenderedController : BaseController
    {
        // GET: AdjustTendered
        public ActionResult Index(string sms)
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


                var ddlShipPoint = ddl.GetDropDownListTenderedMonth("ShippingPoint");
                var ddlShipTo = ddl.GetDropDownListTenderedMonth("ShipTo");
                var ddlTruckType = ddl.GetDropDownListTenderedMonth("TruckType");

                ViewBag.ShipPoint = new SelectList(ddlShipPoint.ToList(), "Id", "Name");
                ViewBag.ShipTo = new SelectList(ddlShipTo.ToList(), "Id", "Name");
                ViewBag.TruckType = new SelectList(ddlTruckType.ToList(), "Id", "Name");

                var ddlReason = (from r in objBs.ReasonTenderedBs.GetAll().Where(x => x.IsDeleted == false)
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
        public JsonResult ShiptoFilter(string SegmentId)
        {
            return Json(objBs.tenderedDelayBs.GetByShipto(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShippingPointFilter(string SegmentId)
        {
            return Json(objBs.tenderedDelayBs.GetByShipPoint(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult truckTypeFilter(string SegmentId)
        {
            return Json(objBs.tenderedDelayBs.GetByTruckType(SegmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.ReasonTenderedBs.GetAll().Where(x => x.IsDeleted == false)
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAdjustTenderTable(string SegmentId, string YearId, string MonthId, string ShipPoint, string ShipTo, string TruckType)
        {
            ViewBag.SegmentId = SegmentId;
            ViewBag.YearId = YearId;
            ViewBag.MonthId = MonthId;
            ViewBag.ShipPoint = ShipPoint;
            ViewBag.ShipTo = ShipTo;
            ViewBag.TruckType = TruckType;

            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustTenderedViewModels> viewModel = new List<AdjustTenderedViewModels>();

            //filter department
            var q = from d in objBs.tenderedDelayBs.GetByFilter(SegmentId, Convert.ToInt32(YearId), Convert.ToInt32(MonthId))
                    select d;

            //filter Shipping Point
            if (!String.IsNullOrEmpty(ShipPoint))
                q = q.Where(x => x.SHPPOINT == ShipPoint);

            //filter Shipping To
            if (!String.IsNullOrEmpty(ShipTo))
                q = q.Where(x => x.SHIPTO == ShipTo);

            //filter Truck Type
            if (!String.IsNullOrEmpty(TruckType))
                q = q.Where(x => x.TRUCK_TYPE == TruckType);

            //int c = q.Count();
            foreach (var item in q)
            {
                AdjustTenderedViewModels model = new AdjustTenderedViewModels();
                model.Shipment = item.SHPMNTNO;
                model.CarrierId = item.CARRIER_ID;
                model.RegionId = item.REGION_ID;
                model.RegionName = item.REGION_NAME_TH;
                model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                model.Shipto = item.SHIPTO;
                model.Segment = item.SEGMENT;
                model.SubSegment = item.SUBSEGMENT;
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.ShippingPoint = item.SHPPOINT;
                model.TruckType = item.TRUCK_TYPE;
                model.ShcrDate = item.SHCRDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.PlanTender = item.PLNTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.FirstTender = item.FTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
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
        public ActionResult UpdateTenderReason(List<String> dynamic_select, List<string> txtSM, List<string> txtRemark)
        {
            using (TransactionScope Trans = new TransactionScope())
            {

                try
                {
                    // List<string> listSM = new List<string>();
                    int countSM = 0;
                    for (int i = 0; i < dynamic_select.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(dynamic_select[i]))
                        {
                            string sm = txtSM[i];
                            string reasonId = dynamic_select[i];
                            string remark = txtRemark[i];
                            string reasonName = objBs.ReasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).Name;
                            bool isadjust = objBs.ReasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                            DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                            //Change adjustable here
                            ontimeShipment.TNRD_ADJUST = 0;
                            ontimeShipment.TNRD_ADJUST_BY = User.Identity.Name;
                            ontimeShipment.TNRD_ADJUST_DATE = DateTime.Now;
                            ontimeShipment.TNRD_ONTIME_REASON = reasonName;
                            ontimeShipment.TNRD_ONTIME_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeShipment.TNRD_ONTIME_REMARK = remark;
                            objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                            TenderedDelay tmp_adjusted = objBs.tenderedDelayBs.GetByID(sm);
                            if (tmp_adjusted == null)
                            {
                                return Json("shipment " + sm + " ได้ทำการ adjust ไปแล้ว");
                            }
                            TenderedAdjusted tmp_toInsert = new TenderedAdjusted
                            {
                                CARRIER_ID = tmp_adjusted.CARRIER_ID,
                                DEPARTMENT_ID = tmp_adjusted.DEPARTMENT_ID,
                                DEPARTMENT_Name = tmp_adjusted.DEPARTMENT_Name,
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
                                LAST_SHPG_LOC_NAME = tmp_adjusted.LAST_SHPG_LOC_NAME,
                                VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                SHCRDATE = tmp_adjusted.SHCRDATE,
                                SHCRDATE_D = tmp_adjusted.SHCRDATE_D,
                                PLNTNRDDATE = tmp_adjusted.PLNTNRDDATE,
                                PLNTNRDDATE_D = tmp_adjusted.PLNTNRDDATE_D,
                                FTNRDDATE = tmp_adjusted.FTNRDDATE,
                                FTNRDDATE_D = tmp_adjusted.FTNRDDATE_D,
                                SHPPOINT = tmp_adjusted.SHPPOINT,
                                TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                SHPMNTNO = tmp_adjusted.SHPMNTNO,
                                DELVNO = tmp_adjusted.DELVNO,
                                LOADED_DATE = DateTime.Now,
                                TNRD_ADJUST = isadjust ? 1 : 0,
                                TNRD_ADJUST_BY = User.Identity.Name,
                                TNRD_ADJUST_DATE = DateTime.Now,
                                TNRD_ONTIME_REASON = reasonName,
                                TNRD_ONTIME_REASON_ID = Convert.ToInt32(reasonId),
                                TNRD_ONTIME_REMARK = remark
                            };
                            //insert waiting ofr approval
                            objBs.tenderedAdjustedBs.Insert(tmp_toInsert);
                            //delete AcceptedDelays
                            objBs.tenderedDelayBs.Delete(sm);

                            countSM++;
                        }
                    }

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countSM + "-Shipment is adjusted Successfully!" });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { sms = "Operation update reason accepted failed !" + ex.ToString() });
                }
                //  return View();
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
                        if (System.IO.File.Exists(Server.MapPath("~/Content/Docs/tnrd/") + reference + fileName))
                        {
                            return Json("อัพโหลดไม่สำเร็จ - มีไฟล์นี้อยู่แล้ว");
                        }
                        TenderedFiles tnrdFiles = new TenderedFiles();
                        tnrdFiles.SHPMNTNO = reference;
                        tnrdFiles.FILEPATH = "Content/Docs/tnrd/" + reference + "_" + fileName;
                        tnrdFiles.LOADED_DATE = DateTime.Now;
                        tnrdFiles.LOADED_BY = User.Identity.Name;
                        objBs.tenderedFilesBs.Insert(tnrdFiles);
                        file.SaveAs(Server.MapPath("~/Content/Docs/tnrd/") + reference + "_" + fileName); //File will be saved in application root
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
                    string targetpath = Server.MapPath("~/Content/Docs/tnrd/");
                    FileUpload.SaveAs(targetpath + DateTime.Now.ToString("yyyyMMddHHmm", new CultureInfo("th-TH")) + "_adjust.xlsx");
                    string pathToExcelFile = targetpath + DateTime.Now.ToString("yyyyMMddHHmm", new CultureInfo("th-TH")) + "_adjust.xlsx";
                    var ext = Path.GetExtension(pathToExcelFile);

                    int countSM = 0;

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
                                    string sm = dr[0].ToString();
                                    int reasonId = Convert.ToInt32(dr[15].ToString());
                                    string remark = dr[16].ToString();
                                    string reasonName = objBs.ReasonTenderedBs.GetByID(reasonId).Name;
                                    bool isadjust = objBs.ReasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                                    DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                                    //Change adjustable here
                                    ontimeShipment.TNRD_ADJUST = 0;
                                    ontimeShipment.TNRD_ADJUST_BY = User.Identity.Name;
                                    ontimeShipment.TNRD_ADJUST_DATE = DateTime.Now;
                                    ontimeShipment.TNRD_ONTIME_REASON = reasonName;
                                    ontimeShipment.TNRD_ONTIME_REASON_ID = Convert.ToInt32(reasonId);
                                    ontimeShipment.TNRD_ONTIME_REMARK = remark;
                                    objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                                    TenderedDelay tmp_adjusted = objBs.tenderedDelayBs.GetByID(sm);
                                    if (tmp_adjusted == null)
                                    {
                                        errorRef = errorRef + sm + " , ";
                                    }
                                    else
                                    {
                                        TenderedAdjusted tmp_toInsert = new TenderedAdjusted
                                        {
                                            CARRIER_ID = tmp_adjusted.CARRIER_ID,
                                            DEPARTMENT_ID = tmp_adjusted.DEPARTMENT_ID,
                                            DEPARTMENT_Name = tmp_adjusted.DEPARTMENT_Name,
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
                                            LAST_SHPG_LOC_NAME = tmp_adjusted.LAST_SHPG_LOC_NAME,
                                            VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                            VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                            SHCRDATE = tmp_adjusted.SHCRDATE,
                                            SHCRDATE_D = tmp_adjusted.SHCRDATE_D,
                                            PLNTNRDDATE = tmp_adjusted.PLNTNRDDATE,
                                            PLNTNRDDATE_D = tmp_adjusted.PLNTNRDDATE_D,
                                            FTNRDDATE = tmp_adjusted.FTNRDDATE,
                                            FTNRDDATE_D = tmp_adjusted.FTNRDDATE_D,
                                            SHPPOINT = tmp_adjusted.SHPPOINT,
                                            TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                            SHPMNTNO = tmp_adjusted.SHPMNTNO,
                                            DELVNO = tmp_adjusted.DELVNO,
                                            LOADED_DATE = DateTime.Now,
                                            TNRD_ADJUST = isadjust ? 1 : 0,
                                            TNRD_ADJUST_BY = User.Identity.Name,
                                            TNRD_ADJUST_DATE = DateTime.Now,
                                            TNRD_ONTIME_REASON = reasonName,
                                            TNRD_ONTIME_REASON_ID = Convert.ToInt32(reasonId),
                                            TNRD_ONTIME_REMARK = remark
                                        };
                                        //insert waiting ofr approval
                                        objBs.tenderedAdjustedBs.Insert(tmp_toInsert);
                                        //delete AcceptedDelays
                                        objBs.tenderedDelayBs.Delete(sm);

                                        countSM++;
                                    }
                                }
                            }
                            Trans.Complete();
                            if (errorRef != "")
                            {
                                errorRef = "<div style='overflow:auto'> Shipment หมายเลข " + errorRef + "ได้ทำการ adjust ไปแล้ว </div>";
                            }
                            return Content("อัพโหลดสำเร็จ " + countSM + " Shipment" + "<br>" + errorRef);
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
        public ActionResult ExportExcel(string SegmentId, string YearId, string MonthId, string ShipPoint, string ShipTo, string TruckType)
        {
            try
            {
                DataTable templateData = new DataTable();
                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustTenderedViewModels> viewModel = new List<AdjustTenderedViewModels>();

                //filter department
                var q = from d in objBs.tenderedDelayBs.GetByFilter(SegmentId, Convert.ToInt32(YearId), Convert.ToInt32(MonthId))
                        select d;

                //filter Shipping Point
                if (!String.IsNullOrEmpty(ShipPoint))
                    q = q.Where(x => x.SHPPOINT == ShipPoint);

                //filter Shipping To
                if (!String.IsNullOrEmpty(ShipTo))
                    q = q.Where(x => x.SHIPTO == ShipTo);

                //filter Truck Type
                if (!String.IsNullOrEmpty(TruckType))
                    q = q.Where(x => x.TRUCK_TYPE == TruckType);

                //int c = q.Count();
                foreach (var item in q)
                {
                    AdjustTenderedViewModels model = new AdjustTenderedViewModels();
                    model.Shipment = item.SHPMNTNO;
                    model.CarrierId = item.CARRIER_ID;
                    model.RegionId = item.REGION_ID;
                    model.RegionName = item.REGION_NAME_TH;
                    model.Soldto = item.SOLDTO;
                    model.SoldtoName = item.SOLDTO_NAME;
                    model.Shipto = item.SHIPTO;
                    model.Segment = item.SEGMENT;
                    model.SubSegment = item.SUBSEGMENT;
                    model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                    model.ShippingPoint = item.SHPPOINT;
                    model.TruckType = item.TRUCK_TYPE;
                    model.ShcrDate = item.SHCRDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    model.PlanTender = item.PLNTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    model.FirstTender = item.FTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    viewModel.Add(model);
                }
                templateData.Merge(ExcelModels.ToDataTable(viewModel));
                templateData.Columns.Add("Remark", typeof(string));

                ExcelModels ex = new ExcelModels();
                ex.DumpExcel(templateData, "ExportedAdjustOutbound_" + DateTime.Now.ToString("yyyyMMddHHmm", new CultureInfo("th-TH")));//dump

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