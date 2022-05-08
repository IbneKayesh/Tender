using System;
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
            List<RFQ_TENDER> objList = new List<RFQ_TENDER>() {
            new RFQ_TENDER(){
                RFQ_NUMBER="B0125258524452",START_DATE=DateTime.Now,END_DATE=DateTime.Now,LAST_DELIVERY_DATE=DateTime.Now,
                PAY_A="CASh", PAY_AP=50,PAY_B="LC", PAY_BP=50,PRODUCTS_QUANTITY=1000,SHIPMENT_MODE=1,PORT_ID=1,RECEIVER_NAME="Chittagonj Port",
                RECEIVER_DETAILS="Pran Rfl Group",CURRENCY_RATE=85,CURRENCY_NAME="$",PRODUCTS_DESC="1000 Ton Camical"
                },
            new RFQ_TENDER(){
                RFQ_NUMBER="B0125258524452",START_DATE=DateTime.Now,END_DATE=DateTime.Now,LAST_DELIVERY_DATE=DateTime.Now,
                PAY_A="CASh", PAY_AP=50,PAY_B="LC", PAY_BP=50,PRODUCTS_QUANTITY=1000,SHIPMENT_MODE=1,PORT_ID=1,RECEIVER_NAME="Chittagonj Port",
                RECEIVER_DETAILS="Pran Rfl Group",CURRENCY_RATE=85,CURRENCY_NAME="$",PRODUCTS_DESC="1000 Ton Camical"
                }
            };
            return View(objList);
        }
        [HttpGet]
        public ActionResult SubmitTender()
        {
            DropDownFor_Tender();
            return View();
        }
        [HttpPost]
        public ActionResult SubmitTender(RFQ_TENDER obj)
        {

            DropDownFor_Tender();
            return View(obj);
        }
        public ActionResult CompareTender()
        {
            List<RFQ_BIDDING> objList = new List<RFQ_BIDDING>() {
            new RFQ_BIDDING(){
                RFQ_NUMBER="B0125258524452",RFQ_SL=10,VENDOR_ID="Arok Traders",SUBMIT_DATE=DateTime.Now,
                PRODUCTS_RATE=500,PRODUCTS_QUANTITY=1000,SHIPMENT_MODE=1,PORT_ID=1,LOADING_ADDRESS="Chittagonj Port",
                SENDER_NAME="Pran Rfl Group",SENDER_DETAILS="must be receive in 30 days",PRODUCTS_DESC="1000 Ton Camical"
                },
             new RFQ_BIDDING(){
                RFQ_NUMBER="B0125258524452",RFQ_SL=10,VENDOR_ID="Arok Traders",SUBMIT_DATE=DateTime.Now,
                PRODUCTS_RATE=500,PRODUCTS_QUANTITY=1000,SHIPMENT_MODE=1,PORT_ID=1,LOADING_ADDRESS="Chittagonj Port",
                SENDER_NAME="Pran Rfl Group",SENDER_DETAILS="must be receive in 30 days",PRODUCTS_DESC="1000 Ton Camical"
                },
              new RFQ_BIDDING(){
                RFQ_NUMBER="B0125258524452",RFQ_SL=10,VENDOR_ID="Arok Traders",SUBMIT_DATE=DateTime.Now,
                PRODUCTS_RATE=500,PRODUCTS_QUANTITY=1000,SHIPMENT_MODE=1,PORT_ID=1,LOADING_ADDRESS="Chittagonj Port",
                SENDER_NAME="Pran Rfl Group",SENDER_DETAILS="must be receive in 30 days",PRODUCTS_DESC="1000 Ton Camical"
                }
            };
            return View(objList);
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
       

       
        //public ActionResult QuotationCompare(string id)
        //{
        //    RFQ_BIDDING obj = new RFQ_BIDDING();
        //    obj.RFQ_NUMBER = "B0125258524452";
        //    obj.RFQ_SL = 10;
        //    obj.VENDOR_ID = "Arok Traders";
        //    obj.SUBMIT_DATE = DateTime.Now;
        //    obj.PRODUCTS_RATE = 500;
        //    obj.PRODUCTS_QUANTITY = 1000;
        //    obj.SHIPMENT_MODE = 1;
        //    obj.PORT_ID = 1;
        //    obj.LOADING_ADDRESS = "Chittagonj Port";
        //    obj.SENDER_NAME = "Pran Rfl Group";
        //    obj.SENDER_DETAILS = "must be receive in 30 days";
        //    obj.PRODUCTS_DESC = "1000 Ton Camical";
        //    return View(obj);
        //}
    }
}