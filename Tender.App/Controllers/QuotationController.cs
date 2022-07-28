using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tender.App.Service;
using Tender.Models.Models;
using PagedList;

namespace Tender.App.Controllers
{
    public class QuotationController : Controller
    {
        // GET: Bid, For Bidder
        [UserSessionCheck]
        public ActionResult Index(int? page)
        {
            List<RFQ_TenderView> obj = QuotationService.getAllTenderForSupplier((string)Session["vendorId"]).Item1.Where(c => c.APPROVAL_ID == "0").ToList();
            if (obj.Count == 0)
            {
                TempData["msg"] = AlertService.SaveWarningOK("No RFQ found");
            }
            return View(obj.ToPagedList(page ?? 1, pageSize: 8));
        }

        [HttpGet]
        [UserSessionCheck]
        public ActionResult SubmitQuote(string id)
        {
            DropDownFor_Tender();
            RFQ_BIDDING obj = new RFQ_BIDDING();

            Tuple<RFQ_BIDDING, EQResult> _tpl = QuotationService.getTenderById(id);
            if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
            {
                List<RFQ_TNDR_DOCUMENTS> docList = QuotationService.getTenderDocumentList(id).Item1;
                _tpl.Item1.RFQ_TNDR_DOCUMENTS = docList;
                if (_tpl.Item1.LOWER_RATE == "Lower Rate Only")
                {
                    decimal minimumBid = CommonService.getQuotMinimumBidRate(_tpl.Item1.RFQ_NUMBER, (string)Session["vendorId"]);
                    ViewBag.MINIMUM_BID_RATE = minimumBid;
                }
                return View(_tpl.Item1);
            }
            return View(obj);

            //return View(new RFQ_BIDDING());
        }
        [HttpPost]
        public ActionResult SubmitQuote(RFQ_BIDDING obj)
        {
            obj.SUBMIT_DATE = DateTime.Now;
            obj.VENDOR_ID = (string)Session["vendorId"];

            //submit once check
            if (obj.RE_BID == "Submit Once")
            {
                var _tplSubmitType = QuotationService.submitOnceCheck(obj.RFQ_NUMBER,obj.VENDOR_ID);
                if (_tplSubmitType.Item1.Count() >0)
                {
                    TempData["msg"] = AlertService.SaveWarningOK("Alreay apply quotation for this RFQ");
                    return RedirectToAction("Index");
                }
            }

            //lower rate condition check
            if (obj.LOWER_RATE == "Lower Rate Only")
            {
                decimal minimumBid = CommonService.getQuotMinimumBidRate(obj.RFQ_NUMBER, obj.VENDOR_ID);
                if (obj.PRODUCTS_RATE >= minimumBid && minimumBid != 0)
                {
                    TempData["msg"] = AlertService.SaveWarningOK("Submit Rate must be less then your prvious rate. Please apply again");
                    return RedirectToAction("SubmitQuote", new { id = obj.RFQ_NUMBER });
                }
            }
            if (ModelState.IsValid)
            {
                var _tpl = QuotationService.SaveQuotationData(obj);
                if (_tpl.SUCCESS == true)
                {
                    TempData["msg"] = AlertService.SaveSuccessOK();
                }
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                ModelState.AddModelError("Error", "Invalid Data");

                DropDownFor_Tender();
                return View(obj);
            }
            DropDownFor_Tender();
            return RedirectToAction("Index");
        }
        [UserSessionCheck]
        public ActionResult WiningsBid()
        {
            List<RFQ_BIDDING> obj = QuotationService.winingsBid((string)Session["vendorId"]).Item1;
            return View(obj);
        }
        [UserSessionCheck]
        public ActionResult TotalApplyBids()
        {
            List<RFQ_BIDDING> obj = QuotationService.totalApplyBids((string)Session["vendorId"]).Item1;
            return View(obj);
        }

        [UserSessionCheck]
        public ActionResult QuotDetails(string quotId)
        {
            DropDownFor_Tender();
            RFQ_BIDDING obj = new RFQ_BIDDING();

            Tuple<RFQ_BIDDING, EQResult> _tpl = QuotationService.getQuotById(quotId);
            if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
            {
                List<RFQ_TNDR_DOCUMENTS> docList = QuotationService.getTenderDocumentList(_tpl.Item1.RFQ_NUMBER).Item1;
                _tpl.Item1.RFQ_TNDR_DOCUMENTS = docList;
                return View(_tpl.Item1);
            }
            return View(obj);

        }

        public void DropDownFor_Tender()
        {
            ViewBag.SHIPMENT_MODE = new SelectList(TenderService.getShipmentMode().Item1.ToList(), "SHIPMENT_MODE_ID", "SHIPMENT_MODE_NAME");
            ViewBag.PORT_ID = new SelectList(TenderService.getPort().Item1.ToList(), "PORT_ID", "PORT_NAME");

        }

    }
}