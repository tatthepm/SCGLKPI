using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;

namespace SCGLKPIUI.Controllers {
    public class HomeController : BaseController {
        public ActionResult Index() {
            HomeModels model = new HomeModels();
            int last_month = DateTime.Now.AddMonths(-1).Month;
            model.daysDIff = Convert.ToInt32((DateTime.Now - Convert.ToDateTime("01/01/2016")).TotalDays).ToString();
            model.LastMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(last_month);
            model.Year = DateTime.Now.Year.ToString();
            model.shipmentLastMonthCount = "92267";
            model.DNLastMonthCount = "112242";
            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult JsonRole() {
            BaseBs objBs = new BaseBs();
            var q = from r in objBs.tuserBs.GetAll()
                    select new {
                        Id = r.UserId,
                        Name = r.UserEmail
                    };
            return Json(q, JsonRequestBehavior.AllowGet);
        }
    }
}