using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;


namespace SCGLKPIUI.Controllers.Master
{
    public class ReasonTenderedController : BaseController
    {
        // GET: ReasonTendered
        public ActionResult Index(string sms)
        {
            try {
                TempData["Msg"] = sms;
                var q = objBs.ReasonTenderedBs.GetAll().Where(x => x.IsDeleted == false);

                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion reason tendered failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string reasonName, string isAdjust) {
            try {
                if (!string.IsNullOrEmpty(reasonName)) {
                    ReasonTendered ReasonTendered = new ReasonTendered();
                    ReasonTendered.Name = reasonName;
                    ReasonTendered.IsDeleted = false;
                    if (isAdjust == "True") ReasonTendered.IsAdjust = true;
                    if (ModelState.IsValid) {
                        objBs.ReasonTenderedBs.Insert(ReasonTendered);
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
                objBs.ReasonTenderedBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}