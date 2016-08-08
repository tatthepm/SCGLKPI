using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;

namespace SCGLKPIUI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeModels model = new HomeModels();
            int last_month = DateTime.Now.AddMonths(-1).Month;
            model.daysDIff = Convert.ToInt32((DateTime.Now - Convert.ToDateTime("01/01/2016")).TotalDays).ToString();
            model.LastMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(last_month);
            model.Year = DateTime.Now.Year.ToString();
            model.DNLastMonthCount = objBs.HomeKPIBs.GetLastMonth().Select(x => x.DNLastMonthCount).Sum().ToString();
            model.shipmentLastMonthCount = objBs.HomeKPIBs.GetLastMonth().Select(x => x.shipmentLastMonthCount).Sum().ToString();

            model.AcceptLastMonthCount = objBs.HomeKPIBs.GetLastMonth().Select(x => x.AcceptLastMonthCount).Sum().ToString();
            model.AcceptLastMonthPending = objBs.HomeKPIBs.GetLastMonth().Select(x => x.AcceptPendingCount).Sum().ToString();
            model.AcceptLastMonthOntime = objBs.HomeKPIBs.GetLastMonth().Select(x => x.AcceptOntimeCount).Sum().ToString();
            model.AcceptLastMonthDelay = objBs.HomeKPIBs.GetLastMonth().Select(x => x.AcceptDelayCount).Sum().ToString();
            model.AcceptLastMonthPercent = ((Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().Select(x => x.AcceptOntimeCount).Sum()) / Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().Select(x => x.AcceptLastMonthCount).Sum())) * 100).ToString();

            model.TenderLastMonthCount = objBs.HomeKPIBs.GetLastMonth().Select(x => x.TenderLastMonthCount).Sum().ToString();
            model.TenderLastMonthPending = objBs.HomeKPIBs.GetLastMonth().Select(x => x.TenderPendingCount).Sum().ToString();
            model.TenderLastMonthOntime = objBs.HomeKPIBs.GetLastMonth().Select(x => x.TenderOntimeCount).Sum().ToString();
            model.TenderLastMonthDelay = objBs.HomeKPIBs.GetLastMonth().Select(x => x.TenderDelayCount).Sum().ToString();
            model.TenderLastMonthPercent = ((Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().Select(x => x.TenderOntimeCount).Sum()) / Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().Select(x => x.TenderLastMonthCount).Sum())) * 100).ToString();

            model.DeliveryLastMonthCount = objBs.HomeKPIBs.GetLastMonth().Select(x => x.DeliveryLastMonthCount).Sum().ToString();
            model.DeliveryLastMonthPending = objBs.HomeKPIBs.GetLastMonth().Select(x => x.DeliveryPendingCount).Sum().ToString();
            model.DeliveryLastMonthOntime = objBs.HomeKPIBs.GetLastMonth().Select(x => x.DeliveryOntimeCount).Sum().ToString();
            model.DeliveryLastMonthDelay = objBs.HomeKPIBs.GetLastMonth().Select(x => x.DeliveryDelayCount).Sum().ToString();
            model.DeliveryLastMonthPercent = ((Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().Select(x => x.DeliveryOntimeCount).Sum()) / Convert.ToDouble(objBs.HomeKPIBs.GetLastMonth().Select(x => x.DeliveryLastMonthCount).Sum())) * 100).ToString();
          
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult JsonRole()
        {
            BaseBs objBs = new BaseBs();
            var q = from r in objBs.tuserBs.GetAll()
                    select new
                    {
                        Id = r.UserId,
                        Name = r.UserEmail
                    };
            return Json(q, JsonRequestBehavior.AllowGet);
        }
    }
}