using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class TenderController : Controller
    {
        // GET: Tender
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Request()
        {
            return View(new RFQ_MASTER());
        }

        public ActionResult Request(RFQ_MASTER obj)
        {
            return View(obj);
        }
    }
}