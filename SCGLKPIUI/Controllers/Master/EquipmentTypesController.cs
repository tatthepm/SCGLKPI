using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace SCGLKPIUI.Controllers.Master
{
    public class EquipmentTypesController : BaseController
    {
        // GET: EquipmentTypes
        public ActionResult Index(string sms)
        {
            try {
                TempData["Msg"] = sms;
                var q = objBs.EquipmentTypesBs.GetAll().Where(x => x.IsDeleted == false);

                return View(q);

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Opeartion Equipment Type failed " + ex.InnerException.InnerException.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult Create(string Eqmt_Description, string IsActive, string Eqmt_Code) {
            try {
                if (!string.IsNullOrEmpty(Eqmt_Description)) {
                    EquipmentTypes EquipmentTypes = new EquipmentTypes();
                    EquipmentTypes.Eqmt_Code = Eqmt_Code;
                    EquipmentTypes.Eqmt_Description = Eqmt_Description;
                    EquipmentTypes.IsDeleted = false;
                    if (IsActive == "True") EquipmentTypes.IsActive = true;
                    if (ModelState.IsValid) {
                        objBs.EquipmentTypesBs.Insert(EquipmentTypes);
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
                objBs.EquipmentTypesBs.Delete(Id);
                return RedirectToAction("Index", new { sms = "Deleted Successfully !" });

            }
            catch (Exception ex) {
                return RedirectToAction("Index", new { sms = "Operation delete failed ! " + ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}