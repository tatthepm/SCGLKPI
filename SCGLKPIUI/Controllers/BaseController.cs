using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace SCGLKPIUI.Controllers {
    public class BaseController : Controller {
        protected BaseBs objBs;
        public BaseController() {
            objBs = new BaseBs();
            }
        }
    }