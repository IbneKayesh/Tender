using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class SetupController : Controller
    {
        #region Category
        // GET: Setup
        public ActionResult Categories()
        {
            return View(new TNDR_CATEGORY().getAll());
        }

        public ActionResult CreateCategory()
        {
            return View();
        }
        public ActionResult CreateCategory(string obj)
        {
            return View();
        }
        #endregion
    }
}