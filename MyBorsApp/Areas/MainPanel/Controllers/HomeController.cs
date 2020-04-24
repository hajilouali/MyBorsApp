using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Models;
using Models.Api.Request;
using Models.Api.Response;
using MyBorsApp.ApiControler;
using MyBorsApp.Convertor;
using Newtonsoft.Json;
using PhoenixFutureApiSdk;
using PhoenixFutureApiSdk.Model;

namespace MyBorsApp.Areas.MainPanel.Controllers
{

    public class HomeController : Controller
    {
        MyContext db= new MyContext();
        const string subdomain = "bmi";
        private static FutureSdk future = new FutureSdk(subdomain);
        // GET: MainPanel/Home
        public ActionResult Index()
        {
            var user = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault();
            ViewBag.user = user;
            if (!DataUtility.HolidayTest(DateTime.Now))
            {
                ViewBag.startTime = "10:00 AM";
                if (DataUtility.IsEndWeakDay())
                {
                    ViewBag.endTime = "04:00 PM";
                }
                else
                {
                    ViewBag.endTime = "05:00 PM";
                }
            }
            else
            {
                ViewBag.startTime = "0";
                ViewBag.endTime = "0";
            }
            
            return View(db.UserContractses.Where(p => p.UserID == user.UserID).ToList());
        }

        public ActionResult DashbordResult()
        {
            var user = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault();
            return PartialView(db.UserContractses.Where(p => p.UserID == user.UserID).ToList());
        }

        public ActionResult AddContractRow()
        {
            var token = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault().Token;
            var response = Apiservice.GetMessage("/api/contracts/all", token);

            var item = response.Content.ReadAsAsync<List<ContractsViewModel>>().Result;
            ViewBag.ContractID1 = new SelectList(item, "id", "name");
            ViewBag.ContractID2 = new SelectList(item, "id", "name");
            
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddContractRow(UserContracts userContracts)
        {
            try
            {
                userContracts.DateTime = DateTime.Now;
                userContracts.UserID = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault().UserID;
                db.UserContractses.Add(userContracts);
                db.SaveChanges();
                return Redirect("/");
            }
            catch 
            {
                return PartialView(userContracts);
            }
           
            
        }
        public ActionResult EditeRowContract(int id)
        {
            var token = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault().Token;
            var usercontract = db.UserContractses.Find(id);
            var response = Apiservice.GetMessage("/api/contracts/all", token);

            var item = response.Content.ReadAsAsync<List<ContractsViewModel>>().Result;
            ViewBag.ContractID1 = new SelectList(item, "id", "name", usercontract.ContractID1);
            ViewBag.ContractID2 = new SelectList(item, "id", "name", usercontract.ContractID2);

            return PartialView(usercontract);
        }

        [HttpPost]
        public ActionResult EditeRowContract(UserContracts userContracts)
        {
            try
            {
                db.Entry(userContracts).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/");
            }
            catch
            {
                return PartialView(userContracts);
            }
        }
        public void changestatus(int id ,bool statos)
        {
            var UserContracts = db.UserContractses.Find(id);
            if (UserContracts!=null)
            {
                UserContracts.IsActive = statos;
                db.UserContractses.AddOrUpdate(UserContracts);
                db.SaveChanges();
            }
        }

        public void deletedrow(int id)
        {
            UserContracts item = db.UserContractses.Find(id);
            if (item !=null)
            {
                db.UserContractses.Remove(item);
                db.SaveChanges();
            }
          
        }
        public JsonResult GetContractName(int id)
        {
            try
            {
                var token = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault().Token;
                var response = Apiservice.GetMessage("/api/contracts/all", token);

                var item = response.Content.ReadAsAsync<List<ContractsViewModel>>().Result;
                var respons = item.Where(p => p.id == id).SingleOrDefault();

                return Json(respons, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }
        public string GetContractTitel(int id)
        {
            try
            {
                var token = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault().Token;
                var response = Apiservice.GetMessage("/api/contracts/all", token);

                var item = response.Content.ReadAsAsync<List<ContractsViewModel>>().Result;
                var respons = item.Where(p => p.id == id).SingleOrDefault();

                return respons.name;
            }
            catch
            {
                return null;
            }
        }
        public JsonResult GetContractDitails(int id)
        {
            try
            {
                var token = db.Userses.Where(p => p.UserName == User.Identity.Name).SingleOrDefault().Token;
                var response = Apiservice.GetMessage(string.Format("/api/contract/") + id, token);

                var item = response.Content.ReadAsAsync<ContractDetailViewModel>().Result;

                return Json(item, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }

        }


    }
}