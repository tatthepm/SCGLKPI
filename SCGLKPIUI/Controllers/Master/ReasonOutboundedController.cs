using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers.Master {
    public class ReasonOutboundedController : BaseController {
        // GET: ReasonOutbounded
        public ActionResult Index(string sms) {
            try {
                TempData["Msg"] = sms;
                var q = objBs.reasonOutboundBs.GetAll().Where(x => x.IsDeleted == false);
                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion reason outbound failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string reasonName, string isAdjust) {
            try {
                if (!string.IsNullOrEmpty(reasonName)) {
                    ReasonOutbound reasonOutbound = new ReasonOutbound();
                    reasonOutbound.Name = reasonName;
                    reasonOutbound.IsDeleted = false;
                    if (isAdjust == "True") reasonOutbound.IsAdjust = true;
                    if (ModelState.IsValid) {
                        objBs.reasonOutboundBs.Insert(reasonOutbound);
                    }
                    return RedirectToAction("Index", new { sms = "Created Successfully !" });
                }
                else {
                    return RedirectToAction("Index", new { sms = "reason is null or empty !" });
                }
            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Create failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public ActionResult Delete(int Id) {
            try {
                objBs.reasonOutboundBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}