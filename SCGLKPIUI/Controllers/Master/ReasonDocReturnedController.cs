using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers.Master {
    public class ReasonDocReturnedController : BaseController {
        // GET: ReasonDocReturned
        public ActionResult Index(string sms) {
            try {
                TempData["Msg"] = sms;
                var q = objBs.reasonDocReturnBs.GetAll();
                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion reason doc return failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string reasonName, string isAdjust) {
            try {
                if (!string.IsNullOrEmpty(reasonName)) {
                    ReasonDocReturn reasonDocReturn = new ReasonDocReturn();
                    reasonDocReturn.Name = reasonName;
                    reasonDocReturn.IsDeleted = false;
                    if (isAdjust == "True") reasonDocReturn.IsAdjust = true;
                    if (ModelState.IsValid) {
                        objBs.reasonDocReturnBs.Insert(reasonDocReturn);
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
                objBs.reasonDocReturnBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

    }
}