using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;


namespace SCGLKPIUI.Controllers.Master {
    public class ReasonDeliveredController : BaseController {
        // GET: ReasonDelivered
        public ActionResult Index(string sms) {
            try {
                TempData["Msg"] = sms;
                var q = objBs.reasonOntimeBs.GetAll();
                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion reason ontime failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string reasonName, string isAdjust) {
            try {
                if (!string.IsNullOrEmpty(reasonName)) {
                    ReasonOntime reasonOntime = new ReasonOntime();
                    reasonOntime.Name = reasonName;
                    reasonOntime.IsDeleted = false;
                    if (isAdjust == "True") reasonOntime.IsAdjust = true;
                    if (ModelState.IsValid) {
                        objBs.reasonOntimeBs.Insert(reasonOntime);
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
                objBs.reasonOntimeBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}