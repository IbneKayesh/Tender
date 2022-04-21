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
        public ActionResult Registration()
        {
            return View(new VENDOR());
        }
        public ActionResult Registration(VENDOR obj)
        {
            return View();
        }

        public ActionResult UpdateProfile(string id)
        {
            //var obj = db.VENDOR_DETAILS.Find(id);
            return View(new VENDOR_DETAILS());
        }
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