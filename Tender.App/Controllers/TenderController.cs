using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;
using Tender.App.Service;
using Aio.Db.MSSqlEF;

namespace Tender.App.Controllers
{
    public class TenderController : Controller
    {
        // GET: Tender, For Supplier
        string userId = "c0919d47-94d1-49a7-b351-1fa448081ed0";
        public ActionResult Index()
        {List<RFQ_TenderView> obj = QuotationService.getAllTender().Item1;

            return View(obj);
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
        [ValidateAntiForgeryToken]
        public ActionResult SubmitTender(RFQ_TENDER obj)
        {
            obj.RFQ_NUMBER = CommonService.getTenderNumber("RFQ_TENDER");
            //obj.PORT_ID = obj.PORT_ID == 0 ? 0 : obj.PORT_ID;
            //ModelState.Clear();
            DropDownFor_Tender();
            if (ModelState.IsValid)
            {
                TenderService.SaveData(obj);
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                ModelState.AddModelError("Err", "Invalid Data");
                return View(obj);
            }
            return View(obj);
        }
        public ActionResult CompareTender(string id)
        {
            List<RFQ_BIDDING> obj = QuotationService.getTenderListForCompare(id).Item1;           
            return View(obj);
        }
        public ActionResult ApproveTender(string rfqNumber,string quotNumber,string vendorId)
        {
            RFQ_TENDER_APPROVAL obj = new RFQ_TENDER_APPROVAL();
            obj.APPROVAL_ID= Guid.NewGuid().ToString();
            obj.RFQ_NUMBER = rfqNumber;
            obj.QUOTE_NUMBER = quotNumber;
            obj.VENDOR_ID = vendorId;
            QuotationService.ApproveQuotation(obj);
            return RedirectToAction("Index", "Tender");
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
            //ViewBag.SHIPMENT_MODE = new SelectList(TenderService.getShipmentMode().Item1.ToList(), "SHIPMENT_MODE_ID", "SHIPMENT_MODE_NAME");
            ViewBag.SHIPMENT_MODE = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.PORT_ID = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.INCO_TERMS = new SelectList(TenderService.getIncoterms().Item1.ToList(), "INCO_TERMS_ID", "INCO_TERMS_NAME");
            ViewBag.PAY_A = new SelectList(TenderService.getPaymentMode().Item1.ToList(), "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            ViewBag.PAY_B = new SelectList(TenderService.getPaymentMode().Item1.ToList(), "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            ViewBag.CURRENCY_NAME = new SelectList(TenderService.getCurrency().Item1.ToList(), "CURRENCY_NAME", "CURRENCY_NAME");
            ViewBag.TENDER_DOC = new SelectList(TenderService.getTnderDoc().Item1.ToList(), "DOCUMENTS_ID", "DOCUMENTS_NAME");

        }
        [AllowAnonymous]
        public ActionResult DropDownFor_Port(string portno)
        {
            if (portno == "2")
            {
                var obj = new SelectList(TenderService.getPort().Item1.ToList(), "PORT_ID", "PORT_NAME");
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SelectList(new List<SelectListItem>
                  {
                      new SelectListItem{ Text="No Port", Value = "0" }
                  }, "Value", "Text", 0), JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        public ActionResult DropDownFor_shipmentMode(string portno)
        {
            if (portno == "2")
            {
                var obj = new SelectList(TenderService.getShipmentMode().Item1.ToList(), "SHIPMENT_MODE_ID", "SHIPMENT_MODE_NAME");
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SelectList(new List<SelectListItem>
                  {
                      new SelectListItem{ Text="No shipment Mode", Value = "0" }
                  }, "Value", "Text", 0), JsonRequestBehavior.AllowGet);
            }
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