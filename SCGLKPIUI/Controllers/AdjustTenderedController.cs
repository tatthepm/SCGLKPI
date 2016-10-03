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

namespace SCGLKPIUI.Controllers
{
    public class AdjustTenderedController : BaseController
    {
        // GET: AdjustTendered
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

                var ddlReason = (from r in objBs.reasonTenderedBs.GetAll().Where(x => x.IsDeleted == false)
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
            var result = (from r in objBs.reasonTenderedBs.GetAll().Where(x => x.IsDeleted == false)
                          select new
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonAdjustTenderTable(string SegmentId, string YearId, string MonthId)
        {
            ViewBag.SegmentId = SegmentId;
            ViewBag.YearId = YearId;
            ViewBag.MonthId = MonthId;
            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustTenderedViewModels> viewModel = new List<AdjustTenderedViewModels>();

            //filter department
            var q = from d in objBs.tenderedDelayBs.GetByFilter(SegmentId, Convert.ToInt32(MonthId), Convert.ToInt32(YearId))
                    select d;

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
                model.ShiptoName = item.LAST_SHPG_LOC_NAME;
                model.ShippingPoint = item.SHPPOINT;
                model.TruckType = item.TRUCK_TYPE;
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

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTenderReason(List<String> dynamic_select, List<string> txtSM, List<string> txtRemark, string segmentId, string yearId, string monthId)
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
                            string reasonName = objBs.reasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).Name;
                            bool isadjust = objBs.reasonTenderedBs.GetByID(Convert.ToInt32(reasonId)).IsAdjust;
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
                                LAST_SHPG_LOC_NAME = tmp_adjusted.LAST_SHPG_LOC_NAME,
                                VENDOR_CODE = tmp_adjusted.VENDOR_CODE,
                                VENDOR_NAME = tmp_adjusted.VENDOR_NAME,
                                PLNTNRDDATE = tmp_adjusted.PLNTNRDDATE,
                                PLNTNRDDATE_D = tmp_adjusted.PLNTNRDDATE_D,
                                FTNRDDATE = tmp_adjusted.FTNRDDATE,
                                FTNRDDATE_D = tmp_adjusted.FTNRDDATE_D,
                                SHPPOINT = tmp_adjusted.SHPPOINT,
                                TRUCK_TYPE = tmp_adjusted.TRUCK_TYPE,
                                SHPMNTNO = tmp_adjusted.SHPMNTNO,
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
                    if (System.IO.File.Exists(Server.MapPath("~/Icons/TNRD/") + reference + fileName))
                    {
                        return Json("อัพโหลดไม่สำเร็จ - มีไฟล์นี้อยู่แล้ว");
                    }
                    TenderedFiles tnrdFiles = new TenderedFiles();
                    tnrdFiles.SHPMNTNO = reference;
                    tnrdFiles.FILEPATH = "Icons/TNRD/" + reference + "_" + fileName;
                    tnrdFiles.LOADED_DATE = DateTime.Now;
                    tnrdFiles.LOADED_BY = User.Identity.Name;
                    objBs.tenderedFilesBs.Insert(tnrdFiles);
                    file.SaveAs(Server.MapPath("~/Icons/TNRD/") + reference + "_" + fileName); //File will be saved in application root
                }
                Trans.Complete();
                return Json("อัพโหลดสำเร็จ " + Request.Files.Count + " ไฟล์");
            }
        }
    }
}