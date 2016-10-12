using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using SCGLKPIUI.Models;
using System.Transactions;

namespace SCGLKPIUI.Controllers
{
    public class ViewerShipmentController : BaseController
    {
        // GET: DataViewer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OverviewShipmentTableSummary(string SHIPMENT)
        {
            SHIPMENT = SHIPMENT.Trim().PadLeft(10,'0');
            //get by ID
            var q =  objBs.dWH_ONTIME_SHIPMENTBs.GetByID(SHIPMENT);

            return PartialView("pv_ShipmentViewer", q);
        }
    }
}