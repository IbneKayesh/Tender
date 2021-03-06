using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;
using Tender.App.Service;
using Aio.Db.MSSqlEF;
using PagedList;


namespace Tender.App.Controllers
{
    public class TenderController : Controller
    {
        [UserSessionCheck]
        public ActionResult Index(int? page)
        {
            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];
            List<RFQ_TenderView> obj = QuotationService.getAllTender((string)Session["vendorId"], snPurcheserComObj.COMPANY_ID).Item1.Where(c => c.SUBMITED_BIDS != 0).ToList();

            return View(obj.ToPagedList(page ?? 1, pageSize: 8));
        }
        [UserSessionCheck]
        public ActionResult ViewAllTender(int? page)
        {
            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];

            ViewBag.SEARCH_TYPE = TenderService.DropDown_SearchType();
            List<RFQ_TenderView> obj = QuotationService.getAllTender((string)Session["vendorId"],snPurcheserComObj.COMPANY_ID).Item1;
            return View(obj.ToPagedList(page ?? 1, pageSize: 8));
        }
        [UserSessionCheck]
        public ActionResult ViewApproveTender(int? page)
        {
            ViewBag.SEARCH_TYPE = TenderService.DropDown_SearchType();
            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];
            List<RFQ_TenderView> obj = QuotationService.getAllTender((string)Session["vendorId"],snPurcheserComObj.COMPANY_ID).Item1.Where(c => c.APPROVAL_ID != "0").ToList();
            return View(obj.ToPagedList(page ?? 1, pageSize: 8));
        }
        [UserSessionCheck]
        public ActionResult ViewPendingApprovalTnd(int? page)
        {
            ViewBag.SEARCH_TYPE = TenderService.DropDown_SearchType();
            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];
            List<RFQ_TenderView> obj = QuotationService.getAllTender((string)Session["vendorId"], snPurcheserComObj.COMPANY_ID).Item1.Where(c => c.APPROVAL_ID == "0").ToList();
            return View(obj.ToPagedList(page ?? 1, pageSize: 8));
        }
        [UserSessionCheck]
        public ActionResult ViewExpireTnd(int? page)
        {
            ViewBag.SEARCH_TYPE = TenderService.DropDown_SearchType();
            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];
            List<RFQ_TenderView> obj = QuotationService.getAllTender((string)Session["vendorId"], snPurcheserComObj.COMPANY_ID).Item1.Where(c => (c.END_DATE <= System.DateTime.Now.Date) && (c.SUBMITED_BIDS == 0)).ToList();
            return View(obj.ToPagedList(page ?? 1, pageSize: 8));
        }
        [HttpGet]
        [UserSessionCheck]
        public ActionResult TenderDetails(string id)
        {
            DropDownFor_Tender();
            RFQ_BIDDING obj = new RFQ_BIDDING();

            Tuple<RFQ_BIDDING, EQResult> _tpl = QuotationService.getTenderById(id);
            if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
            {
                List<RFQ_TNDR_DOCUMENTS> docList = QuotationService.getTenderDocumentList(id).Item1;
                _tpl.Item1.RFQ_TNDR_DOCUMENTS = docList;
                return View(_tpl.Item1);
            }
            return View(obj);
        }
        [HttpGet]
        [UserSessionCheck]
        public ActionResult SubmitTender()
        {
            if (Session["ssUser"] == null)
            {
                RedirectToAction("Login", "Accounts");
            }
            RFQ_TENDER obj = new RFQ_TENDER();
            obj.VENDOR_ID = (string)Session["vendorId"];
            DropDownFor_Tender();
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitTender(RFQ_TENDER obj)
        {
            obj.RFQ_NUMBER = CommonService.getTenderNumber("RFQ_TENDER",obj.COMPANY_ID);
            //obj.PORT_ID = obj.PORT_ID == 0 ? 0 : obj.PORT_ID;
            //ModelState.Clear();
            DropDownFor_Tender();
            if (ModelState.IsValid)
            {
                var _tpl = TenderService.SaveData(obj);
                if (_tpl.SUCCESS == true)
                {
                    TempData["msg"] = AlertService.SaveSuccess();
                }
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                ModelState.AddModelError("Error", "Invalid Data");
                return View(obj);
            }
            return RedirectToAction("ViewAllTender");
        }
        [UserSessionCheck]
        public ActionResult CompareTender(string id)
        {
            List<RFQ_BIDDING> obj = QuotationService.getTenderListForCompare(id).Item1;
            return View(obj);
        }
        [UserSessionCheck]
        public ActionResult ApproveTender(string rfqNumber, string quotNumber, string vendorId)
        {
            RFQ_TENDER_APPROVAL obj = new RFQ_TENDER_APPROVAL();
            obj.APPROVAL_ID = Guid.NewGuid().ToString();
            obj.RFQ_NUMBER = rfqNumber;
            obj.QUOTE_NUMBER = quotNumber;
            obj.VENDOR_ID = vendorId;
            var _tpl = QuotationService.ApproveQuotation(obj);
            if (_tpl.SUCCESS == true)
            {
                TempData["msg"] = AlertService.SaveSuccessOK("Approve Successfully");
            }
            return RedirectToAction("Index", "Tender");
        }
        public ActionResult ApprovalProcess() {
            RFQ_TENDER_APPROVAL_VIEW obj = new RFQ_TENDER_APPROVAL_VIEW();

            List<RFQ_TENDER_APPROVAL_VIEW> list = QuotationService.ApproveTenderList().Item1;
            if (list.Count()>1)
            {
                return View(list);
            }
            return View(obj);
        }
        public ActionResult AprvRFQ(string rfqNumber, string quotNumber, string userId,string approvalNumber) {
            RFQ_TENDER_APPROVAL obj = new RFQ_TENDER_APPROVAL();
            obj.RFQ_NUMBER = rfqNumber;
            obj.QUOTE_NUMBER = quotNumber;
            if (approvalNumber == "1") {
                obj.FIRST_APRV_BY = userId;
                obj.FIRST_APRV_DATE = DateTime.Now;
                obj.FIRST_APRV_STATUS = "Y";
                obj.FIRST_APRV_NOTE = "1st approve done";
            }
            else if (approvalNumber == "2") {
                obj.SEC_APRV_BY = userId;
                obj.SEC_APRV_DATE = DateTime.Now;
                obj.SEC_APRV_STATUS = "Y";
                obj.SEC_APRV_NOTE = "2nd approve done";
            }
            else if (approvalNumber == "3") {
                obj.THIRD_APRV_BY = userId;
                obj.THIRD_APRV_DATE = DateTime.Now;
                obj.THIRD_APRV_STATUS = "Y";
                obj.THIRD_APRV_NOTE = "3rd approve done";
            }
            //APPROVE FOR RFQ_TENDER_APPROVAL UPDATE 
            var _tpl = QuotationService.ApproveQuotationUpdate(obj);
            if (_tpl.SUCCESS == true)
            {
                TempData["msg"] = AlertService.SaveSuccessOK("Approve Successfully");
            }
            else {
                TempData["msg"] = AlertService.SaveWarningOK(_tpl.MESSAGES);
            }
            return RedirectToAction("ApprovalProcess");
        }
        public void DropDownFor_Tender()
        {
            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];

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

            ViewBag.VENDOR_ID = new SelectList(CommonService.GetVendor((string)Session["vendorId"]).Item1.ToList(), "VENDOR_ID", "ORGANIZATION_NAME");
            ViewBag.PRODUCTS_ID = new SelectList(TenderService.GetVendorWiseItem((string)Session["vendorId"]).Item1.ToList(), "PRODUCTS_ID", "PRODUCTS_NAME");
            //ViewBag.SHIPMENT_MODE = new SelectList(TenderService.getShipmentMode().Item1.ToList(), "SHIPMENT_MODE_ID", "SHIPMENT_MODE_NAME");
            ViewBag.SHIPMENT_MODE = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.PORT_ID = new SelectList(Enumerable.Empty<SelectListItem>());
            ViewBag.INCO_TERMS = new SelectList(TenderService.getIncoterms().Item1.ToList(), "INCO_TERMS_ID", "INCO_TERMS_NAME");
            ViewBag.PAY_A = new SelectList(TenderService.getPaymentMode().Item1.ToList(), "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            ViewBag.PAY_B = new SelectList(TenderService.getPaymentMode().Item1.ToList(), "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            ViewBag.CURRENCY_NAME = new SelectList(TenderService.getCurrency().Item1.ToList(), "CURRENCY_NAME", "CURRENCY_NAME");
            ViewBag.TENDER_DOC = new SelectList(TenderService.getTnderDoc().Item1.ToList(), "DOCUMENTS_ID", "DOCUMENTS_NAME");
            ViewBag.COMPANY_ID = new SelectList(CommonService.GetCompany(snPurcheserComObj.COMPANY_ID).Item1.ToList(), "COMPANY_ID", "COMPANY_NAME");


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

        public ActionResult DropDownFor_PaymentModeA(string paymentModeID)
        {
            var obj = new SelectList(TenderService.getPaymentModeByID(paymentModeID).Item1.ToList(), "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CompanyProductCatWiseSupllierList(string COMPANY_ID, string PRODUCTS_ID) {
            HttpContext.Session.Remove("snVendorList");
            var objList = TenderService.companyProductCatWiseSupllierList(COMPANY_ID, PRODUCTS_ID).Item1.ToList();
            Session["snVendorList"] = objList;

           var  snVendorList=(List<VENDOR_DETAILS>)Session["snVendorList"];

            return Json(objList, JsonRequestBehavior.AllowGet);
        }
    }
}