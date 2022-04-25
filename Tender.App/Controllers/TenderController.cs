﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class TenderController : Controller
    {
        // GET: Tender, For Supplier

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BidRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BidRequest(RFQ_TENDER obj)
        {
            return View(obj);
        }
    }
}