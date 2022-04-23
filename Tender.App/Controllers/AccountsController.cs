using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View(new VENDOR());
        }
        [HttpPost]
        public ActionResult Login(string id, VENDOR_LOGIN obj)
        {
            return View(new VENDOR());
        }
        public ActionResult Logout()
        {
            return View(new VENDOR());
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View(new VENDOR());
        }
        [HttpPost]
        public ActionResult Registration(VENDOR obj)
        {

            return View();
        }
        [HttpGet]
        public ActionResult UpdateProfile(string id)
        {
     
            return View(new VENDOR_DETAILS().getAll());
        }
        [HttpPost]
        public ActionResult UpdateProfile(VENDOR_DETAILS obj)
        {
            return View();
        }

        public ActionResult ViewProfile(string id)
        {
            //var obj = db.VENDOR_DETAILS.Find(id);
            return View(new VENDOR_DETAILS());
        }
    }
}