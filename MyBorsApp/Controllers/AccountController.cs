using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using Models;
using Models.Models.ViewModels;
using MyBorsApp.Tools;
using Tools;

namespace MyBorsApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MyContext db = new MyContext();
        // GET: Account
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {

            if (ModelState.IsValid)
            {
                if (!db.Userses.Any(i => i.UserName == register.UserName.Trim().ToLower()))
                {
                    if (!db.Userses.Any(u => u.Email == register.Email.Trim().ToLower()))
                    {
                        Users user = new Users()
                        {
                            Email = register.Email.Trim().ToLower(),
                            Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                            ActiveCode = Guid.NewGuid().ToString(),
                            IsActive = false,
                            RegisterDate = DateTime.Now,
                            RoleID = 1,
                            UserName = register.UserName.Trim().ToLower(),
                            FullName = register.FullName
                        };
                        db.Userses.Add(user);
                        db.SaveChanges();
                        //Send Active Email
                        //string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActiviationEmail", user);
                        //SendEmail.Send(user.Email, "ایمیل فعالسازی", body);
                        //End Send Active Email
                        return View("SuccessRegister", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "ایمیل  وارد شده تکراری است");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری میباشد ");

                }

            }



            return View(register);

        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {

            if (ModelState.IsValid)
            {
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.Userses.SingleOrDefault(u => u.Email == login.Email && u.Password == hashPassword);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);

                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده یافت نشد");
                }
            }



            return View(login);
        }
        //public ActionResult ActiveUser(string id)
        //{
        //    var user = db.Userses.SingleOrDefault(u => u.ActiveCode == id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    user.IsActive = true;
        //    user.ActiveCode = Guid.NewGuid().ToString();
        //    db.SaveChanges();
        //    ViewBag.UserName = user.UserName;
        //    return View();
        //}

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [Route("ForgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            
                if (ModelState.IsValid)
                {
                    var user = db.Userses.SingleOrDefault(u => u.Email == forgot.Email);
                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            string bodyEmail =
                                PartialToStringClass.RenderPartialView("ManageEmails", "RecoveryPassword", user);
                            SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
                            return View("SuccesForgotPassword", user);
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربری یافت نشد");
                    }
                }
            

            return View();
        }

        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoveryPassword(string id, RecoveryPasswordViewModel recovery)
        {
            
                if (ModelState.IsValid)
                {
                    var user = db.Userses.SingleOrDefault(u => u.ActiveCode == id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }

                    user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                    user.ActiveCode = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    return Redirect("/Login?recovery=true");
                }
            

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}