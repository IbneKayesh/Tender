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
        string userId = "c0919d47-94d1-49a7-b351-1fa448081ed0";
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
            RFQ_TENDER obj = new RFQ_TENDER();
            obj.VENDOR_ID = userId;
            DropDownFor_Tender();
            return View(obj);
        }
        [HttpPost]
        public ActionResult SubmitTender(RFQ_TENDER obj)
        {
            obj.RFQ_NUMBER = CommonService.getTenderNumber("RFQ_TENDER");
            DropDownFor_Tender();
            if (ModelState.IsValid)
            {
                TenderService.SaveData(obj);
            }
            else
            {
                return View(obj);
            }
            return View(obj);
        }
        public ActionResult CompareTender()
        {
            List<RFQ_BIDDING> objList = new List<RFQ_BIDDING>() {
            new RFQ_BIDDING(){
                RFQ_NUMBER="B0125258524452",RFQ_SL=10,VENDOR_ID="Arok Traders",SUBMIT_DATE=DateTime.Now,
                PRODUCTS_RATE=500,PRODUCTS_QUANTITY=1000,SHIPMENT_MODE=1,PORT_ID=1,LOADING_ADDRESS="Chittagonj Port",
                SENDER_NAME="Pran Rfl Group",SENDER_DETAILS="must be receive in 30 days",PRODUCTS_DESC="1000 Ton Camical",
                PRODUCTS_ID="Cemical",TND_PRODUCTS_DESC="must be single packet",START_DATE=DateTime.Now,END_DATE=DateTime.Now,
                LAST_DELIVERY_DATE=DateTime.Now,PARTIAL_SHIPMENT="allow",TENDER_SHIPMENT_MODE="sea",TENDER_PORT_ID="cp",
                DELIVERY_ADDRESS="Mongla Bondor",RECEIVER_NAME="Pran Group",RECEIVER_DETAILS="Badd,Dhaka,Bangladesh"
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
            ViewBag.PARTIAL_SHIPMENT = TenderService.DropDown_partialshipment();
           // ViewBag.PORT_ID = TenderService.DropDown_Port();
            //ViewBag.INCO_TERMS = TenderService.DropDown_Incoterms();
            ViewBag.COST_EX_INC = TenderService.DropDown_CostType();
            //ViewBag.CURRENCY_NAME = TenderService.DropDown_currencyList();
            //ViewBag.PAY_A = TenderService.DropDown_payment();
            //ViewBag.PAY_B = TenderService.DropDown_payment();
            ViewBag.LOCAL_IMPORT = TenderService.DropDown_importer();

            ViewBag.VENDOR_ID = new SelectList(CommonService.GetVendor(userId).Item1.ToList(), "VENDOR_ID", "ORGANIZATION_NAME");
            ViewBag.PRODUCTS_ID = new SelectList(TenderService.GetVendorWiseItem(userId).Item1.ToList(), "PRODUCTS_ID", "PRODUCTS_NAME");
            ViewBag.SHIPMENT_MODE = new SelectList(TenderService.getShipmentMode().Item1.ToList(), "SHIPMENT_MODE_ID", "SHIPMENT_MODE_NAME");
            ViewBag.PORT_ID = new SelectList(TenderService.getPort().Item1.ToList(), "PORT_ID", "PORT_NAME");
            ViewBag.INCO_TERMS = new SelectList(TenderService.getIncoterms().Item1.ToList(), "INCO_TERMS_ID", "INCO_TERMS_NAME");
            ViewBag.PAY_A = new SelectList(TenderService.getPaymentMode().Item1.ToList(), "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            ViewBag.PAY_B = new SelectList(TenderService.getPaymentMode().Item1.ToList(), "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            ViewBag.CURRENCY_NAME = new SelectList(TenderService.getCurrency().Item1.ToList(), "CURRENCY_NAME", "CURRENCY_NAME");
            ViewBag.TENDER_DOC = new SelectList(TenderService.getTnderDoc().Item1.ToList(), "DOCUMENTS_ID", "DOCUMENTS_NAME");

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