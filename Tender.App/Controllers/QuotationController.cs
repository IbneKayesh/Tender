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
            RFQ_BIDDING obj = new RFQ_BIDDING();

            obj.RFQ_NUMBER = "B0125258524452"; obj.RFQ_SL = 10; obj.VENDOR_ID = "Arok Traders"; obj.SUBMIT_DATE = DateTime.Now; obj.
                PRODUCTS_RATE = 500; obj.PRODUCTS_QUANTITY = 1000; obj.SHIPMENT_MODE = 1; obj.PORT_ID = 1; obj.LOADING_ADDRESS = "Chittagonj Port"; obj.
                SENDER_NAME = "Pran Rfl Group"; obj.SENDER_DETAILS = "must be receive in 30 days"; obj.PRODUCTS_DESC = "1000 Ton Camical"; obj.
                PRODUCTS_ID = "Cemical"; obj.TENDER_PRODUCTS_DESC = "must be single packet"; obj.START_DATE = DateTime.Now; obj.END_DATE = DateTime.Now; obj.
                LAST_DELIVERY_DATE = DateTime.Now; obj.PARTIAL_SHIPMENT = true; obj.TENDER_SHIPMENT_MODE = 1; obj.TENDER_PORT_ID = 1; obj.
                DELIVERY_ADDRESS = "Mongla Bondor"; obj.RECEIVER_NAME = "Pran Group"; obj.RECEIVER_DETAILS = "Badda Dhaka Bangladesh";
            return View(obj);

            //return View(new RFQ_BIDDING());
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