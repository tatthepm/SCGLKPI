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
    public class PendingTenderedController : BaseController
    {
        // GET: PendingTendered
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

                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { sms = "Operation Accept failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public JsonResult PendingTenderedTableSummary(string SegmentId, string YearId, string MonthId)
        {
            // add IEnumerable<AcceptOntimeSummaryViewModels>
            List<PendingTenderedViewModels> viewSummaryModel = new List<PendingTenderedViewModels>();

            // filter by department
            var q = objBs.tenderedPendingBs.GetAll().Where(x => !String.IsNullOrEmpty(x.DEPARTMENT_Name)
                                               && !String.IsNullOrEmpty(x.SECTION_NAME)
                                               && !String.IsNullOrEmpty(x.MATNAME)
                                               && DateTime.Parse(x.PLNTNRDDATE.ToString(), new CultureInfo("en-US")).Year.ToString() == YearId);
            //filter Segment
            if (!String.IsNullOrEmpty(SegmentId))
                q = q.Where(x => x.SEGMENT == SegmentId);

            //filter month
            if (!String.IsNullOrEmpty(MonthId))
                q = q.Where(x => Convert.ToDateTime(x.PLNTNRDDATE.Value.Date.ToShortDateString()).Month.ToString() == MonthId);

            foreach (var item in q)
            {
                PendingTenderedViewModels model = new PendingTenderedViewModels();
                model.Shipment = item.SHPMNTNO;
                model.RegionName = item.REGION_NAME_TH;
                model.SoldtoName = item.SOLDTO_NAME;
                model.ShiptoName = item.SHIPTO;
                model.PlanTender = item.PLNTNRDDATE_D.Value.ToString();
                viewSummaryModel.Add(model);
            }

            return Json(viewSummaryModel, JsonRequestBehavior.AllowGet);
        }
    }
}