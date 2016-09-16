using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SCGLKPIUI.Models;

namespace SCGLKPIUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private SHA512Managed sha512 = new SHA512Managed();
        // GET: Login
        public ActionResult Index(string sms)
        {
            TempData["Msg"] = sms;
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(TUser user)
        {
            try
            {
                byte[] hashedValueOfPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                string hexOfValuePassword = BitConverter.ToString(hashedValueOfPassword);
                user.Password = hexOfValuePassword;


                if (Membership.ValidateUser(user.UserEmail, user.Password))
                {
                    int id = objBs.tuserBs.GetAll().Where(x => x.UserEmail == user.UserEmail).FirstOrDefault().UserId;
                    var tuser = objBs.tuserBs.GetByID(id);
                    tuser.LastLogin = DateTime.Now;
                    objBs.tuserBs.Update(tuser);
                    FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                    return RedirectToAction("Index", "Home", new { sms = "Login Successfully" });
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { sms = "Failed! " + "user or password is incorrected" });
                }
            }
            catch (Exception ex)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Login", new { sms = "Failed! " + ex.Message });
            }
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();  // This may not be needed -- but can't hurt
            Session.Abandon();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rSessionCookie);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult ChangePassword(string sms)
        {
            TempData["Msg"] = sms;
            int userId = objBs.tuserBs.GetAll().Where(x => x.UserEmail == User.Identity.Name).FirstOrDefault().UserId;
            TUser tuser = objBs.tuserBs.GetByID(userId);
            ViewBag.OldPassword = tuser.Password;
            return View(tuser);
        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(TUser tuser, string oldPassword, string keyPassword)
        {
            try
            {
                byte[] hashedValueOfKeyPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(keyPassword));
                byte[] hashedValueOfPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(tuser.Password));
                byte[] hashedValueOfConfirmPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(tuser.ConfirmPassword));
                string hexOfValuePassword = BitConverter.ToString(hashedValueOfPassword);
                string hexOfValueConfirmPassword = BitConverter.ToString(hashedValueOfConfirmPassword);
                string hexOfValueKeyPassword = BitConverter.ToString(hashedValueOfKeyPassword);
                //keyPassword
                if (oldPassword == hexOfValueKeyPassword)
                {
                    if (ModelState.IsValid)
                    {
                        tuser.Password = hexOfValuePassword;
                        tuser.ConfirmPassword = hexOfValueConfirmPassword;
                        objBs.tuserBs.Update(tuser);
                        return RedirectToAction("ChangePassword", "Login", new { sms = "Change password Successfully ,  Please Sign out and then login again !" });
                    }
                    else
                    {
                        return RedirectToAction("ChangePassword", "Login", new { sms = "Failed! " + "ModelState not valid !" });
                    }
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Login", new { sms = "Failed! " + "Old password incorrected !" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ChangePassword", "Login", new { sms = "Failed! " + "Change password operation failed " + ex.Message });
            }
        }
        [Authorize]
        public ActionResult Register(string sms)
        {
            TempData["Msg"] = sms;
            var Roles = from d in objBs.roleBs.GetAll()
                        select d;
            var ddlRoles = new SelectList(Roles, "RoleId", "RoleDesc");
            ViewBag.ddlRoles = ddlRoles.ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Register(TUser user)
        {
            try
            {
                byte[] hashedConfirmPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.ConfirmPassword));
                string hexConfirmPassword = BitConverter.ToString(hashedConfirmPassword);
                byte[] hashedValueOfPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                string hexOfValuePassword = BitConverter.ToString(hashedValueOfPassword);
                user.ConfirmPassword = hexConfirmPassword;
                user.Password = hexOfValuePassword;
                if(user.Password != user.ConfirmPassword)
                {
                    return RedirectToAction("Register", "Login", new { sms = "Failed! " + "password not matched." });
                }
                //check if existed.
                var tuser = objBs.tuserBs.GetByEmail(user.UserEmail);
                if (tuser == null )
                {
                    objBs.tuserBs.Insert(user);
                    //FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                    return RedirectToAction("Register", "Login", new { sms = "User added successfully" });
                }
                else
                {
                    return RedirectToAction("Register", "Login", new { sms = "Failed! " + "User existed" });
                }
            }
            catch (Exception ex)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Register", "Login", new { sms = "Failed! " + ex.Message });
            }
        }
        [Authorize]
        public ActionResult ResetPassword(string sms)
        {
            TempData["Msg"] = sms;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ResetPassword(TUser user, string oldPassword, string keyPassword)
        {
            try
            {
                byte[] hashedConfirmPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.ConfirmPassword));
                string hexConfirmPassword = BitConverter.ToString(hashedConfirmPassword);
                byte[] hashedValueOfPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                string hexOfValuePassword = BitConverter.ToString(hashedValueOfPassword);
                user.ConfirmPassword = hexConfirmPassword;
                user.Password = hexOfValuePassword;
                if (user.ConfirmPassword != user.ConfirmPassword)
                {
                    return RedirectToAction("ResetPassword", "Login", new { sms = "Failed! " + "Confirm password incorrect." });
                }
                //check if existed.
                var tuser = objBs.tuserBs.GetByEmail(user.UserEmail);
                if (tuser != null)
                {
                    objBs.tuserBs.Update(user);
                    //FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                    return RedirectToAction("ResetPassword", "Login", new { sms = "Password changed successfully" });
                }
                else
                {
                    return RedirectToAction("ResetPassword", "Login", new { sms = "Failed! " + "User not existed" });
                }
            }
            catch (Exception ex)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("ResetPassword", "Login", new { sms = "Failed! " + ex.Message });
            }
        }
        [Authorize]
        public ActionResult DeleteUser(string sms)
        {
            TempData["Msg"] = sms;
            var Roles = from d in objBs.tuserBs.GetAll()
                        select d;
            var ddlUsers = new SelectList(Roles, "UserEmail", "UserEmail");
            ViewBag.ddlUsers = ddlUsers.ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteUser(TUser user)
        {
            try
            {
                if (user.UserEmail != user.Password)
                {
                    return RedirectToAction("DeleteUser", "Login", new { sms = "Failed! " + "E-mail not matched." });
                }
                //check if existed.
                var tuser = objBs.tuserBs.GetByEmail(user.UserEmail);
                if (tuser != null)
                {
                    objBs.tuserBs.Delete(tuser.UserId);
                    //FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                    return RedirectToAction("DeleteUser", "Login", new { sms = "User deleted successfully" });
                }
                else
                {
                    return RedirectToAction("DeleteUser", "Login", new { sms = "Failed! " + "User not existed" });
                }
            }
            catch (Exception ex)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("DeleteUser", "Login", new { sms = "Failed! " + ex.Message });
            }
        }
    }
}