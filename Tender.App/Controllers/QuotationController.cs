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
            List<RFQ_TenderView> obj = QuotationService.getAllTenderForSupplier((string)Session["vendorId"]).Item1.Where(c=>c.APPROVAL_ID =="0").ToList();
            return View(obj.ToPagedList(page ??1, pageSize:8));
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
            if (ModelState.IsValid)
            {
               var _tpl= QuotationService.SaveQuotationData(obj);
                if (_tpl.SUCCESS == true)
                {
                    TempData["msg"] = AlertService.SaveSuccessOK();
                }
            }
            else
            {
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
      
        public void DropDownFor_Tender()
        {
            ViewBag.SHIPMENT_MODE = new SelectList(TenderService.getShipmentMode().Item1.ToList(), "SHIPMENT_MODE_ID", "SHIPMENT_MODE_NAME");
            ViewBag.PORT_ID = new SelectList(TenderService.getPort().Item1.ToList(), "PORT_ID", "PORT_NAME");

        }
    }
}