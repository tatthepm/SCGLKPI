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
                var q = objBs.reasonTenderedBs.GetAll();

                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion reason tendered failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string reasonName) {
            try {
                ReasonTendered reasonTendered = new ReasonTendered();
                reasonTendered.Name = reasonName;
                if (ModelState.IsValid) {
                    objBs.reasonTenderedBs.Insert(reasonTendered);
                }
                return RedirectToAction("Index", new { sms = "Created Successfully !" });
            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Create failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public ActionResult Delete(int Id) {
            try {
                objBs.reasonTenderedBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}