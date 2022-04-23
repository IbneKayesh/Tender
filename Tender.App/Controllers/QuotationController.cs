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
            return View(new RFQ_BIDDING());
        }
        public ActionResult Bid(RFQ_BIDDING obj)
        {
            return View();
        }
    }
}