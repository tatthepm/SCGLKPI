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
    public class ViewerDNController : BaseController
    {
        // GET: DataViewer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OverviewDnTableSummary(string DN)
        {
            DN = DN.Trim().PadLeft(10, '0');
            //get by ID
            var q =  objBs.dWH_ONTIME_DNBs.GetByID(DN);

            return PartialView("pv_DNViewer", q);
        }
    }
}