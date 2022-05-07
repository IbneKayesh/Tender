﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;
using Tender.App.Service;

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
            DropDownFor_Tender();
            return View();
        }
        [HttpPost]
        public ActionResult BidRequest(RFQ_TENDER obj)
        {

            DropDownFor_Tender();
            return View(obj);
        }

        public void DropDownFor_Tender()
        {
            ViewBag.SELL_BUY = TenderService.DropDownList_Sel_Buy();
            ViewBag.RE_BID = TenderService.getReBidding();
            ViewBag.LOWER_RATE = TenderService.getAnyRate();
            ViewBag.PRODUCTS_ID = TenderService.DropDownList_item();
            ViewBag.PARTIAL_SHIPMENT = TenderService.DropDown_partialshipment();
            ViewBag.SHIPMENT_MODE = TenderService.DropDown_shipmentMode();
            ViewBag.PORT_ID = TenderService.DropDown_Port();
            ViewBag.INCO_TERMS = TenderService.DropDown_Incoterms();
            ViewBag.COST_EX_INC = TenderService.DropDown_CostType();
            ViewBag.CURRENCY_NAME = TenderService.DropDown_currencyList();
            ViewBag.PAY_A = TenderService.DropDown_payment();
            ViewBag.PAY_B = TenderService.DropDown_payment();

        }

        public ActionResult QuotationList()
        {
            return View();
        }
        public ActionResult QuotationCompare(string id)
        {
            List<RFQ_BIDDING> objList = new List<RFQ_BIDDING>();
            return View(objList);
        }
    }
}