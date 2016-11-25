using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers.Master
{
    public class HubMastersController : BaseController
    {
        // GET: HubMasters
        public ActionResult Index(string sms)
        {
            try {
                TempData["Msg"] = sms;
                var q = objBs.HubMastersBs.GetAll().Where(x => x.IsDeleted == false);

                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion Hub Master failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string Hub_Name, string IsActive, string Hub_Id) {
            try {
                if (!string.IsNullOrEmpty(Hub_Name)) {
                    HubMasters HubMasters = new HubMasters();
                    HubMasters.Hub_Id = Hub_Id;
                    HubMasters.Hub_Name = Hub_Name;
                    HubMasters.IsDeleted = false;
                    if (IsActive == "True") HubMasters.IsActive = true;
                    if (ModelState.IsValid) {
                        objBs.HubMastersBs.Insert(HubMasters);
                    }
                    return RedirectToAction("Index", new { sms = "Created Successfully !" });
                }
                else {
                    return RedirectToAction("Index", new { sms = "Hub Name is null or empty !" });
                }
            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation Create failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        public ActionResult Delete(int Id) {
            try {
                objBs.HubMastersBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}