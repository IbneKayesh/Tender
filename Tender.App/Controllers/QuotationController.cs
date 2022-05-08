using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.App.Service;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class QuotationController : Controller
    {
        // GET: Bid, For Bidder
        public ActionResult Index()
        {
            DropDownFor_Tender();
            return View();
        }
        
        [HttpGet]
        public ActionResult SubmitQuote(string id)
        {
            DropDownFor_Tender();
            return View(new RFQ_BIDDING());
        }
        [HttpPost]
        public ActionResult SubmitQuote(RFQ_BIDDING obj)
        {
            return View();
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

        }
    }
}