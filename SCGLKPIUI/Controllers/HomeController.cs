using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace SCGLKPIUI.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
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