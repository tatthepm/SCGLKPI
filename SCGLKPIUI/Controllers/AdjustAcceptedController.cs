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
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace SCGLKPIUI.Controllers
{
    public class AdjustAcceptedController : BaseController
    {
        // GET: AdjustAccepted
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
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public JsonResult SectionFilter(string departmentId)
        {
            return Json(objBs.acceptedDelayBs.GetBySection(departmentId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionId)
        {
            return Json(objBs.acceptedDelayBs.GetByMatName(departmentId, sectionId).OrderBy(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonAcceptedBs.GetAll().Where(x => x.IsDeleted == false)
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAdjustAcceptTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            ViewBag.DepartmentId = DepartmentId;
            ViewBag.SectionId = SectionId;
            ViewBag.MatNameId = MatNameId;
            ViewBag.YearId = YearId;
            ViewBag.MonthId = MonthId;
            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustAcceptedViewModels> viewModel = new List<AdjustAcceptedViewModels>();

            //filter department
            var q = from d in objBs.acceptedDelayBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

            //filter matname
            if (!String.IsNullOrEmpty(MatNameId))
            {
                q = q.Where(x => x.MATFRIGRP == MatNameId);
            }

            //int c = q.Count();

            foreach (var item in q)
            {
                AdjustAcceptedViewModels model = new AdjustAcceptedViewModels();
                model.Shipment = item.SHPMNTNO;
                model.CarrierId = item.CARRIER_ID;
                model.RegionId = item.REGION_ID;
                model.RegionName = item.REGION_NAME_TH;
                model.Soldto = item.SOLDTO;
                model.SoldtoName = item.SOLDTO_NAME;
                model.Shipto = item.SHIPTO;
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.ShippingPoint = item.SHPPOINT;
                model.Segment = item.SEGMENT;
                model.SubSegment = item.SUBSEGMENT;
                model.TruckType = item.TRUCK_TYPE;
                model.LastTender = item.LTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.PlanAccept = item.PLNACPDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                model.LastAccept = item.LACPDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                viewModel.Add(model);
            }

            var ddlReason = (from r in objBs.reasonAcceptedBs.GetAll()
                             select new
                             {
                                 Id = r.Id,
                                 Name = r.Name
                             }).Distinct().OrderBy(x => x.Name);
            ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAcceptReason(List<String> dynamic_select, List<string> txtSM, List<string> txtRemark)
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
                            string reasonName = objBs.reasonAcceptedBs.GetByID(Convert.ToInt32(reasonId)).Name;
                            bool isadjust = objBs.reasonAcceptedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                            DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                            //Change adjustable here
                            ontimeShipment.ACPD_ADJUST = 0;
                            ontimeShipment.ACPD_ADJUST_BY = User.Identity.Name;
                            ontimeShipment.ACPD_ADJUST_DATE = DateTime.Now;
                            ontimeShipment.ACPD_REASON = reasonName;
                            ontimeShipment.ACPD_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeShipment.ACPD_REMARK = remark;
                            objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                            AcceptedDelay tmp_adjusted = objBs.acceptedDelayBs.GetByID(sm);
                            if (tmp_adjusted == null)
                            {
                                return Json("shipment " + sm + " ได้ทำการ adjust ไปแล้ว");
                            }
                            AcceptedAdjusted tmp_toInsert = new AcceptedAdjusted
                            {
                                CARRIER_ID = tmp_adjusted.CARRIER_ID,
                                DEPARTMENT_ID = tmp_adjusted.DEPARTMENT_ID,
                                DEPARTMENT_Name = tmp_adjusted.DEPARTMENT_Name,
                                SECTION_ID = tmp_adjusted.SECTION_ID,
                                SECTION_NAME = tmp_adjusted.SECTION_NAME,
                                SEGMENT = tmp_adjusted.SEGMENT,
                                SUBSEGMENT = tmp_adjusted.SUBSEGMENT,
                                MATFRIGRP = tmp_adjusted.MATFRIGRP,
                                MATNAME = tmp_adjusted.MATNAME,
                                REGION_ID = tmp_adjusted.REGION_ID,
                                REGION_NAME_EN = tmp_adjusted.REGION_NAME_EN,
                                REGION_NAME_TH = tmp_adjusted.REGION_NAME_TH,
                                SOLDTO = tmp_adjusted.SOLDTO,
                                SOLDTO_NAME = tmp_adjusted.SOLDTO_NAME,
                                SHIPTO = tmp_adjusted.SHIPTO,
                                LAST_SHPG_LOC_NAME = tmp_adjusted.LAST_SHPG_LOC_NAME,
                                VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                LTNRDDATE = tmp_adjusted.LTNRDDATE,
                                LTNRDDATE_D = tmp_adjusted.LTNRDDATE_D,
                                PLNACPDDATE = tmp_adjusted.PLNACPDDATE,
                                PLNACPDDATE_D = tmp_adjusted.PLNACPDDATE_D,
                                LACPDDATE = tmp_adjusted.LACPDDATE,
                                LACPDDATE_D = tmp_adjusted.LACPDDATE_D,
                                SHPPOINT = tmp_adjusted.SHPPOINT,
                                TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                SHPMNTNO = tmp_adjusted.SHPMNTNO,
                                LOADED_DATE = DateTime.Now,
                                ACPD_ADJUST = isadjust ? 1 : 0,
                                ACPD_ADJUST_BY = User.Identity.Name,
                                ACPD_ADJUST_DATE = DateTime.Now,
                                ACPD_REASON = reasonName,
                                ACPD_REASON_ID = Convert.ToInt32(reasonId),
                                ACPD_REMARK = remark
                            };
                            //insert waiting ofr approval
                            objBs.acceptedAdjustedBs.Insert(tmp_toInsert);
                            //delete AcceptedDelays
                            objBs.acceptedDelayBs.Delete(sm);

                            countSM++;
                        }
                    }

                    Trans.Complete();
                    return RedirectToAction("Index", new { sms = countSM + "-Shipment is adjusted Successfully!" });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { sms = "Operation update reason accepted failed !" + ex.InnerException.InnerException.Message.ToString() });
                }
                //  return View();
            }
        }

        public JsonResult Upload()
        {
            using (TransactionScope Trans = new TransactionScope())
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
                    if (System.IO.File.Exists(Server.MapPath("~/Content/Docs/acpd/") + reference + "_" + fileName))
                    {
                        return Json("อัพโหลดไม่สำเร็จ - มีไฟล์นี้อยู่แล้ว");
                    }
                    AcceptedFiles acpdFiles = new AcceptedFiles();
                    acpdFiles.SHPMNTNO = reference;
                    acpdFiles.FILEPATH = "Content/Docs/acpd" + reference + "_" + fileName;
                    acpdFiles.LOADED_DATE = DateTime.Now;
                    acpdFiles.LOADED_BY = User.Identity.Name;
                    objBs.acceptedFilesBs.Insert(acpdFiles);
                    file.SaveAs(Server.MapPath("~/Content/Docs/acpd/") + reference + "_" + fileName); //File will be saved in application root
                }
                Trans.Complete();
                return Json("อัพโหลดสำเร็จ " + Request.Files.Count + " ไฟล์");
            }
        }

        public JsonResult UploadReason()
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    string reference = Request.Files.AllKeys[i];
                    HttpPostedFileBase FileUpload = Request.Files[i]; //Uploaded file
                                                                      //Use the following properties to get file's name, size and MIMEType
                    string fileName = reference;
                    string targetpath = Server.MapPath("~/Content/Docs/acpd/");
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
                                    string reasonName = objBs.reasonAcceptedBs.GetByID(reasonId).Name;
                                    bool isadjust = objBs.reasonAcceptedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
                                    DWH_ONTIME_SHIPMENT ontimeShipment = objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm);
                                    //Change adjustable here
                                    ontimeShipment.ACPD_ADJUST = 0;
                                    ontimeShipment.ACPD_ADJUST_BY = User.Identity.Name;
                                    ontimeShipment.ACPD_ADJUST_DATE = DateTime.Now;
                                    ontimeShipment.ACPD_REASON = reasonName;
                                    ontimeShipment.ACPD_REASON_ID = Convert.ToInt32(reasonId);
                                    ontimeShipment.ACPD_REMARK = remark;
                                    objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                                    AcceptedDelay tmp_adjusted = objBs.acceptedDelayBs.GetByID(sm);
                                    if(tmp_adjusted==null)
                                    {
                                        return Json("shipment " + sm + " ได้ทำการ adjust ไปแล้ว");
                                    }
                                    AcceptedAdjusted tmp_toInsert = new AcceptedAdjusted
                                    {
                                        CARRIER_ID = tmp_adjusted.CARRIER_ID,
                                        DEPARTMENT_ID = tmp_adjusted.DEPARTMENT_ID,
                                        DEPARTMENT_Name = tmp_adjusted.DEPARTMENT_Name,
                                        SECTION_ID = tmp_adjusted.SECTION_ID,
                                        SECTION_NAME = tmp_adjusted.SECTION_NAME,
                                        SEGMENT = tmp_adjusted.SEGMENT,
                                        SUBSEGMENT = tmp_adjusted.SUBSEGMENT,
                                        MATFRIGRP = tmp_adjusted.MATFRIGRP,
                                        MATNAME = tmp_adjusted.MATNAME,
                                        REGION_ID = tmp_adjusted.REGION_ID,
                                        REGION_NAME_EN = tmp_adjusted.REGION_NAME_EN,
                                        REGION_NAME_TH = tmp_adjusted.REGION_NAME_TH,
                                        SOLDTO = tmp_adjusted.SOLDTO,
                                        SOLDTO_NAME = tmp_adjusted.SOLDTO_NAME,
                                        SHIPTO = tmp_adjusted.SHIPTO,
                                        LAST_SHPG_LOC_NAME = tmp_adjusted.LAST_SHPG_LOC_NAME,
                                        VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                        VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                        LTNRDDATE = tmp_adjusted.LTNRDDATE,
                                        LTNRDDATE_D = tmp_adjusted.LTNRDDATE_D,
                                        PLNACPDDATE = tmp_adjusted.PLNACPDDATE,
                                        PLNACPDDATE_D = tmp_adjusted.PLNACPDDATE_D,
                                        LACPDDATE = tmp_adjusted.LACPDDATE,
                                        LACPDDATE_D = tmp_adjusted.LACPDDATE_D,
                                        SHPPOINT = tmp_adjusted.SHPPOINT,
                                        TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                        SHPMNTNO = tmp_adjusted.SHPMNTNO,
                                        LOADED_DATE = DateTime.Now,
                                        ACPD_ADJUST = isadjust ? 1 : 0,
                                        ACPD_ADJUST_BY = User.Identity.Name,
                                        ACPD_ADJUST_DATE = DateTime.Now,
                                        ACPD_REASON = reasonName,
                                        ACPD_REASON_ID = Convert.ToInt32(reasonId),
                                        ACPD_REMARK = remark
                                    };
                                    //insert waiting ofr approval
                                    objBs.acceptedAdjustedBs.Insert(tmp_toInsert);
                                    //delete AcceptedDelays
                                    objBs.acceptedDelayBs.Delete(sm);

                                    countSM++;
                                }
                            }
                            Trans.Complete();
                            return Json("อัพโหลดสำเร็จ " + countSM + " Shipment");
                        }
                        catch (Exception e)
                        {
                            return Json("อัพโหลดไม่สำเร็จ กรอกข้อมูลไม่ถูกต้อง");
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                }
            }
            return Json("อัพโหลดไม่สำเร็จ ประเภทไฟล์ไม่ถูกต้อง");
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
                List<AdjustAcceptedViewModels> viewModel = new List<AdjustAcceptedViewModels>();

                //filter department
                var q = from d in objBs.acceptedDelayBs.GetByFilter(DepartmentId, SectionId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                        select d;

                //filter matname
                if (!String.IsNullOrEmpty(MatNameId))
                {
                    q = q.Where(x => x.MATFRIGRP == MatNameId);
                }

                //int c = q.Count();

                foreach (var item in q)
                {
                    AdjustAcceptedViewModels model = new AdjustAcceptedViewModels();
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
                    model.LastTender = item.LTNRDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    model.PlanAccept = item.PLNACPDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    model.LastAccept = item.LACPDDATE.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    viewModel.Add(model);
                }
                templateData.Merge(ExcelModels.ToDataTable(viewModel));
                templateData.Columns.Add("Remark", typeof(string));

                ExcelModels ex = new ExcelModels();
                ex.DumpExcel(templateData, "ExportedAdjustAccept_" + DateTime.Now.ToString("yyyyMMddHHmm", new CultureInfo("th-TH")));//dump

                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}

