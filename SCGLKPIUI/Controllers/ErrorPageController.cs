using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCGLKPIUI.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult ErrorMessage(string sms)
        {
            TempData["Msg"] = sms;
            return View();
        }
    }
}