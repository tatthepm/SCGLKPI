using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using SCGLKPIUI.Models;


namespace SCGLKPIUI.Controllers {
    public class MenuTableAcceptedController : BaseController {
        // GET: MenuTable
        public ActionResult Index() {
            var q = objBs.menuTableBs.GetAll().Where(x => x.KPIId == "2"
                                               && x.KPIFrequencyId == "1");
            return View(q);
        }
    }
}