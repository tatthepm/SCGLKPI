using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCGLKPIUI.Controllers
{
    public class ChartsController : Controller
    {
        // GET: Charts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChartJs() {
            return View();
        }
    }
}