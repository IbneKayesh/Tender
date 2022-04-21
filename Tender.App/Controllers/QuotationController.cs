using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class QuotationController : Controller
    {
        // GET: Bid, For Bidder
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Bid(string id)
        {
            return View(new RFQ_VENDOR());
        }
        public ActionResult Bid(RFQ_VENDOR obj)
        {
            return View();
        }
    }
}