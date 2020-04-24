using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models;
using DataLayer;
using Models.Models.ViewModels;

namespace MyBorsApp.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        
        // GET: UserPanel/Account
        MyContext db = new MyContext();
        // GET: UserPanel/Account
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (ModelState.IsValid)
            {
                var user = db.Userses.Single(u => u.UserName == User.Identity.Name);
                string oldPasswordHash =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(change.OldPassword, "MD5");
                if (user.Password == oldPasswordHash)
                {
                    string hashNewPasword =
                        FormsAuthentication.HashPasswordForStoringInConfigFile(change.Password, "MD5");
                    user.Password = hashNewPasword;
                    db.SaveChanges();
                    ViewBag.Success = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی درست نمی باشد");
                }
            }
            return View();
        }

        public ActionResult ChangeInfo()
        {
            Users user = db.Userses.Single(u => u.UserName == User.Identity.Name);
            ChangeAccountInfoViewModel model = new ChangeAccountInfoViewModel()
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumer
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeInfo(ChangeAccountInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = db.Userses.Single(u => u.UserName == User.Identity.Name);
                user.FullName = model.FullName;
                user.PhoneNumer = model.PhoneNumber;
                db.SaveChanges();
                ViewBag.Success = true;
            }
            return View(model);
        }

        public ActionResult SetToken()
        {
            Users user = db.Userses.Single(u => u.UserName == User.Identity.Name);
            SetTokenViewModel item = new SetTokenViewModel()
            {
                Token = user.Token,
                TokenUserName = user.UserSetToken
            };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetToken(SetTokenViewModel setToken)
        {
            if (ModelState.IsValid)
            {
                Users user = db.Userses.Single(u => u.UserName == User.Identity.Name);
                user.Token = setToken.Token;
                user.UserSetToken = setToken.TokenUserName;
                db.Userses.AddOrUpdate(user);
                db.SaveChanges();
            }

            return View(setToken);
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