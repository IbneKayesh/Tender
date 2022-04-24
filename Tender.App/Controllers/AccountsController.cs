using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.App.Service;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View(new VENDOR_LOGIN());
        }
        [HttpPost]
        public ActionResult Login(VENDOR_LOGIN obj)
        {
            if (ModelState.IsValid)
            {
                Tuple<VENDOR_LOGIN, EQResult> _tpl = AccountsService.UserLogin(obj);
                if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", _tpl.Item2.MESSAGES);
            }
            return View(obj);
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
            if (ModelState.IsValid)
            {
                EQResult _tpl = AccountsService.registration(obj);
                if (_tpl.SUCCESS && _tpl.ROWS == 1)
                {
                    return RedirectToAction("SendConfirmEmail", _tpl);
                }
                ModelState.AddModelError("", _tpl.MESSAGES);
            }
            return View(obj);
        }

        public ActionResult RegistrationSuccessfull(VENDOR obj)
        {
            if (obj == null)
                obj = new VENDOR();
            obj.ORGANIZATION_NAME = "xxxx";
            obj.VENDOR_EMAIL = "a....1@gmail.com";
            return View(obj);
        }
        public ActionResult SendConfirmEmail(VENDOR obj)
        {
            return RedirectToAction("RegistrationSuccessfull", obj);
        }


        [HttpGet]
        public ActionResult UserProfile(string id)
        {

            return View(new VENDOR_DETAILS().getAll());
        }
        [HttpPost]
        public ActionResult UserProfile(VENDOR_DETAILS obj)
        {
            return View();
        }
    }
}