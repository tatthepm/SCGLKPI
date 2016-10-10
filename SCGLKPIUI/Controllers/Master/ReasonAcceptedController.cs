using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers.Master {
    public class ReasonAcceptedController : BaseController {
        // GET: ReasonAccepted
        public ActionResult Index(string sms) {
            try {
                TempData["Msg"] = sms;
                var q = objBs.reasonAcceptedBs.GetAll().Where(x => x.IsDeleted == false);
                //test
                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion reason accepted failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string reasonName, string isAdjust) {
            try {
                if (!string.IsNullOrEmpty(reasonName)) {

                    ReasonAccepted reasonAccepted = new ReasonAccepted();
                    reasonAccepted.Name = reasonName;
                    reasonAccepted.IsDeleted = false;
                    if (isAdjust == "True") reasonAccepted.IsAdjust = true;
                    if (ModelState.IsValid) {
                        objBs.reasonAcceptedBs.Insert(reasonAccepted);
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
                objBs.reasonAcceptedBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}