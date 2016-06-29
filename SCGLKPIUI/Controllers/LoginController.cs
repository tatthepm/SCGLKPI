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

namespace SCGLKPIUI.Controllers {
    [AllowAnonymous]
    public class LoginController : BaseController {
        private SHA512Managed sha512 = new SHA512Managed();
        // GET: Login
        public ActionResult Index(string sms) {
            TempData["Msg"] = sms;
            return View();
            }
        [HttpPost]
        public ActionResult SignIn(TUser user) {
            try {
                byte[] hashedValueOfPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                string hexOfValuePassword = BitConverter.ToString(hashedValueOfPassword);
                user.Password = hexOfValuePassword;

               
                if(Membership.ValidateUser(user.UserEmail,user.Password)) {
                    int id = objBs.tuserBs.GetAll().Where(x => x.UserEmail == user.UserEmail).FirstOrDefault().UserId;
                    var tuser = objBs.tuserBs.GetByID(id);
                    tuser.LastLogin = DateTime.Now;
                    objBs.tuserBs.Update(tuser);
                    FormsAuthentication.SetAuthCookie(user.UserEmail,false);
                    return RedirectToAction("Index","Home",new { sms = "Login Successfully" });
                    }
                else {
                    return RedirectToAction("Index","Login",new { sms = "user or password is incorrected" });
                    }
                }
            catch(Exception ex) {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index","Login",new { sms = "Login Failed!" + ex.Message });
                }
            }
        public ActionResult SignOut() {
            FormsAuthentication.SignOut();
            Session.Clear();  // This may not be needed -- but can't hurt
            Session.Abandon();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName,"");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId","");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rSessionCookie);
            return RedirectToAction("Index","Home");
            }
        public ActionResult ChangePassword(string sms) {
            TempData["Msg"] = sms;
            int userId = objBs.tuserBs.GetAll().Where(x => x.UserEmail == User.Identity.Name).FirstOrDefault().UserId;
            TUser tuser = objBs.tuserBs.GetByID(userId);
            ViewBag.OldPassword = tuser.Password;
            return View(tuser);
            }
        [HttpPost]
        public ActionResult ChangePassword(TUser tuser,string oldPassword,string keyPassword) {
            try {
                byte[] hashedValueOfKeyPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(keyPassword));
                byte[] hashedValueOfPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(tuser.Password));
                byte[] hashedValueOfConfirmPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(tuser.ConfirmPassword));
                string hexOfValuePassword = BitConverter.ToString(hashedValueOfPassword);
                string hexOfValueConfirmPassword = BitConverter.ToString(hashedValueOfConfirmPassword);
                string hexOfValueKeyPassword = BitConverter.ToString(hashedValueOfKeyPassword);
                //keyPassword
                if(oldPassword == hexOfValueKeyPassword) {
                    if(ModelState.IsValid) {
                        tuser.Password = hexOfValuePassword;
                        tuser.ConfirmPassword = hexOfValueConfirmPassword;
                        objBs.tuserBs.Update(tuser);
                        return RedirectToAction("ChangePassword","Login",new { sms = "Change password Successfully ,  Please Sign out and then login again !" });
                        }
                    else {
                        return RedirectToAction("ChangePassword","Login",new { sms = "ModelState not valid !" });
                        }
                    }
                else {
                    return RedirectToAction("ChangePassword","Login",new { sms = "Old password incorrected !" });
                    }
                }
            catch(Exception ex) {
                return RedirectToAction("ChangePassword","Login",new { sms = "Change password operation failed " + ex.Message });
                }
            }
        }
    }