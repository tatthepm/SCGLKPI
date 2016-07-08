using System;
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
    public class AdjustAcceptedController : BaseController
    {
        // GET: AdjustAccepted
        public ActionResult Index(string sms, string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            try
            {

                TempData["Msg"] = sms;

                DropDownList ddl = new DropDownList();
                var ddlDept = ddl.GetDropDownList("Department");
                var ddlSec = ddl.GetDropDownList("Section");
                var ddlYear = ddl.GetDropDownListAcceptedMonth("Year");
                var ddlMonth = ddl.GetDropDownListAcceptedMonth("Month");
                ViewBag.DepartmentId = new SelectList(ddlDept.ToList(), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddlSec.ToList(), "Id", "Name");
                ViewBag.YearId = new SelectList(ddlYear.ToList(), "Id", "Name");
                ViewBag.MonthId = new SelectList(ddlMonth.ToList(), "Id", "Name");

                //1 DropdownList 
                var ddlMatName = (from m in objBs.acceptedDelayBs.GetAll()
                                  where !String.IsNullOrEmpty(m.MATNAME)
                                  select new
                                  {
                                      Id = m.MATFRIGRP,
                                      Name = m.MATNAME,
                                  }).Distinct();

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
            var result = (from m in objBs.acceptedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          select new
                          {
                              Id = m.SECTION_ID,
                              Name = m.SECTION_NAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatNameFilter(string departmentId, string sectionid)
        {
            var result = (from m in objBs.acceptedDelayBs.GetAll()
                          where m.DEPARTMENT_ID == departmentId
                          && m.SECTION_ID == sectionid
                          select new
                          {
                              Id = m.MATFRIGRP,
                              Name = m.MATNAME
                          }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReasonFilter()
        {
            var result = (from r in objBs.reasonAcceptedBs.GetAll()
                            select new
                            {
                                Id = r.Id,
                                Name = r.Name
                            }).Distinct().OrderBy(x => x.Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDelayAcceptedData(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            try
            {
                ViewBag.DepartmentId = DepartmentId;
                ViewBag.SectionId = SectionId;
                ViewBag.MatNameId = MatNameId;
                ViewBag.YearId = YearId;
                ViewBag.MonthId = MonthId;

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustAcceptedViewModels> viewModel = new List<AdjustAcceptedViewModels>();

                //filter department
                var q = from d in objBs.acceptedDelayBs.GetAll()
                        where d.DEPARTMENT_ID == DepartmentId
                        && d.SECTION_ID == SectionId
                        && d.LACPDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.LACPDDATE_D.Value.Year == Convert.ToInt32(YearId)
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
                    model.PlanAccept = Convert.ToDateTime(item.PLNACPDDATE);
                    model.LastAccept = Convert.ToDateTime(item.LACPDDATE);
                    viewModel.Add(model);
                }

                var ddlReason = (from r in objBs.reasonAcceptedBs.GetAll()
                                 select new
                                 {
                                     Id = r.Id,
                                     Name = r.Name
                                 }).Distinct().OrderBy(x => x.Name);

                //ViewBag.ReasonId = new SelectList(ddlReason.ToList(), "Id", "Name");
                return PartialView("pv_AdjustAccepted", viewModel);
                //return View("Index", viewModel);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation getDelayAccepteData failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public JsonResult JsonAcceptTable(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            ViewBag.DepartmentId = DepartmentId;
            ViewBag.SectionId = SectionId;
            ViewBag.MatNameId = MatNameId;
            ViewBag.YearId = YearId;
            ViewBag.MonthId = MonthId;
            // add IEnumerable<AdjustAcceptedViewModels>
            List<AdjustAcceptedViewModels> viewModel = new List<AdjustAcceptedViewModels>();

            //filter department
            var q = from d in objBs.acceptedDelayBs.GetAll()
                    where d.DEPARTMENT_ID == DepartmentId
                    && d.SECTION_ID == SectionId
                    && d.LACPDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                    && d.LACPDDATE_D.Value.Year == Convert.ToInt32(YearId)
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
                model.PlanAccept = Convert.ToDateTime(item.PLNACPDDATE);
                model.LastAccept = Convert.ToDateTime(item.LACPDDATE);
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
        public ActionResult UpdateAcceptReason(List<String> dynamic_select, List<string> txtSM, List<string> txtRemark, string departmentId, string sectionId, string matNameId, string yearId, string monthId)
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
                            ontimeShipment.ACPD_ADJUST = isadjust ? 0 : 0;
                            ontimeShipment.ACPD_ADJUST_BY = User.Identity.Name;
                            ontimeShipment.ACPD_ADJUST_DATE = DateTime.Now;
                            ontimeShipment.ACPD_REASON = reasonName;
                            ontimeShipment.ACPD_REASON_ID = Convert.ToInt32(reasonId);
                            ontimeShipment.ACPD_REMARK = remark;
                            objBs.dWH_ONTIME_SHIPMENTBs.Update(ontimeShipment);

                            //delete AcceptedDelays
                            objBs.acceptedDelayBs.Delete(sm);

                            //update sum of adjust daily
                            DateTime LACPDDate = Convert.ToDateTime(objBs.dWH_ONTIME_SHIPMENTBs.GetByID(sm).LACPDDATE_D);

                            int id = objBs.ontimeAcceptBs.GetAll()
                                .Where(x => x.AcceptDate == LACPDDate
                                       && x.DepartmentId == departmentId
                                       && x.SectionId == sectionId
                                       && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                            OntimeAccept ontimeAccept = objBs.ontimeAcceptBs.GetByID(id);
                            int adjACPD = ontimeAccept.AdjustAccept + 1;
                            ontimeAccept.AdjustAccept = adjACPD;
                            ontimeAccept.SumOfAdjustAccept = ontimeAccept.OnTime + adjACPD;
                            objBs.ontimeAcceptBs.Update(ontimeAccept);
                            countSM++;
                        }
                    }

                    // update sum of adjust monthly
                    int idM = objBs.ontimeAcceptMonthBs.GetAll()
                              .Where(x => x.Year == yearId
                              && x.Month == monthId
                              && x.DepartmentId == departmentId
                              && x.SectionId == sectionId
                              && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                    OntimeAcceptMonth ontimeAcceptMonth = objBs.ontimeAcceptMonthBs.GetByID(idM);
                    int adjACPDMonth = ontimeAcceptMonth.AdjustAccept + countSM;
                    ontimeAcceptMonth.AdjustAccept = adjACPDMonth;
                    ontimeAcceptMonth.SumOfAdjustAccept = ontimeAcceptMonth.OnTime + adjACPDMonth;
                    objBs.ontimeAcceptMonthBs.Update(ontimeAcceptMonth);

                    // update sum of adjust yearly
                    int idY = objBs.ontimeAcceptYearBs.GetAll()
                              .Where(x => x.Year == yearId
                              && x.DepartmentId == departmentId
                              && x.SectionId == sectionId
                              && x.MatFriGrp == matNameId).FirstOrDefault().Id;
                    OntimeAcceptYear ontimeAcceptYear = objBs.ontimeAcceptYearBs.GetByID(idY);
                    int adjACPDYear = ontimeAcceptYear.AdjustAccept + countSM;
                    ontimeAcceptYear.AdjustAccept = adjACPDYear;
                    ontimeAcceptYear.SumOfAdjustAccept = ontimeAcceptYear.OnTime + adjACPDYear;
                    objBs.ontimeAcceptYearBs.Update(ontimeAcceptYear);

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

        [HttpPost]
        public ActionResult ExportExcel(string DepartmentId, string SectionId, string YearId, string MonthId, string MatNameId)
        {
            try
            {
                //delete file
                var pathDelete = Path.Combine(Server.MapPath("~/App_Data/UploadFiles"), "AcceptDealy.xlsx");
                if (System.IO.File.Exists(pathDelete))
                {
                    System.IO.File.Delete(pathDelete);
                }
                // set path new file
                var path = Server.MapPath(@"~/App_Data/UploadFiles/AcceptDealy.xlsx");
                FileInfo newFile = new FileInfo(path);

                // add IEnumerable<AdjustAcceptedViewModels>
                List<AdjustAcceptedViewModels> viewModel = new List<AdjustAcceptedViewModels>();

                //filter department
                var q = from d in objBs.acceptedDelayBs.GetAll()
                        where d.DEPARTMENT_ID == DepartmentId
                        && d.SECTION_ID == SectionId
                        && d.LACPDDATE_D.Value.Month == Convert.ToInt32(MonthId)
                        && d.LACPDDATE_D.Value.Year == Convert.ToInt32(YearId)
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
                    model.PlanAccept = Convert.ToDateTime(item.PLNACPDDATE);
                    model.LastAccept = Convert.ToDateTime(item.LACPDDATE);
                    viewModel.Add(model);
                }
                // export to excel using EPPLus
                using (ExcelPackage excelPackage = new ExcelPackage(newFile))
                {
                    excelPackage.Workbook.Properties.Author = "Tatthep";
                    excelPackage.Workbook.Properties.Title = "EPPLus Export";
                    excelPackage.Workbook.Properties.Comments = "This is my generagted Excel File";
                    excelPackage.Workbook.Worksheets.Add("Driver");
                    var workSheet = excelPackage.Workbook.Worksheets[1];

                    // You must fetch data then set format
                    workSheet.Cells[2, 1].LoadFromCollection(viewModel, true);

                    //set Format
                    workSheet.DefaultColWidth = 15;
                    workSheet.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                    workSheet.Cells[1, 1].Style.Font.SetFromFont(new System.Drawing.Font("tahoma", 15));
                    workSheet.Cells[1, 1].Value = "Accepted delay";
                    workSheet.Cells[1, 1].Value = "Shipment";
                    workSheet.Cells[2, 2].Value = "CarrierId";
                    workSheet.Cells[2, 3].Value = "RegionId";
                    workSheet.Cells[2, 4].Value = "RegionName";
                    workSheet.Cells[2, 5].Value = "Soldto";
                    workSheet.Cells[2, 6].Value = "SoldtoName";
                    workSheet.Cells[2, 7].Value = "Shipto";
                    workSheet.Cells[2, 8].Value = "ShiptoName";
                    workSheet.Cells[2, 9].Value = "PlanAccept";
                    workSheet.Cells[2, 10].Value = "LastAccept";

                    var rang = workSheet.Cells["A1:J2"];
                    rang.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rang.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rang.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rang.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rang.Style.Font.SetFromFont(new System.Drawing.Font("tahoma", 10));
                    rang.Style.Font.Bold = true;

                    int i = 0;
                    foreach (var item in viewModel)
                    {
                        //  workSheet.Cells[i + 3,1].Value = i + 1;
                        //workSheet.Column(1).Width = 16;
                        //workSheet.Column(4).Width = 25;
                        //workSheet.Column(5).Width = 25;
                        //workSheet.Column(6).Width = 25;
                        //workSheet.Column(7).Width = 25;
                        //workSheet.Column(8).Width = 35;
                        //workSheet.Column(9).Width = 35;
                        //workSheet.Column(12).Width = 20;


                        //Draw Border
                        workSheet.Cells[i + 3, 1, i + 3, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[i + 3, 1, i + 3, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[i + 3, 1, i + 3, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[i + 3, 1, i + 3, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[i + 3, 1, i + 3, 10].Style.Font.SetFromFont(new System.Drawing.Font("tahoma", 9));
                        i++;
                    }
                    excelPackage.Save();
                }


            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation export failed !" + ex.InnerException.InnerException.Message.ToString() });
            }
            string newpath = Server.MapPath(@"~/App_Data/UploadFiles/AcceptDealy.xlsx");
            return File(newpath, "application/vnd.ms-excel", "AcceptDealy.xlsx");

        }
    }
}
