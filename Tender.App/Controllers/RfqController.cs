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
    public class RfqController : Controller
    {
        // GET: Rfq
        
        public ActionResult Index()
        {
            return View();
        }
        [UserSessionCheck]
        public ActionResult PublishRfq()
        {
            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];
            List<RFQ_TenderView> obj = RfqService.getAllRfqList(snPurcheserComObj.COMPANY_ID).Item1.ToList();
            return View(obj);
        }
        [UserSessionCheck]
        public ActionResult RFQLockUnlock(string rfqNumber, int StatusFlag)
        {
            var _tpl = RfqService.ApproveTender(StatusFlag,rfqNumber);
            if (_tpl.SUCCESS == true && _tpl.ROWS == 1 && StatusFlag == 1)
            {
                TempData["msg"] = AlertService.SaveSuccessOK("Lock Successfully");
            }
            else if (_tpl.SUCCESS == true && _tpl.ROWS == 1 && StatusFlag == 0) {
                TempData["msg"] = AlertService.SaveSuccessOK("Unlock Successfully");
            }
            else
            {
                TempData["msg"] = AlertService.SaveWarningOK("Something is wrong ! Please try again");
            }
            return RedirectToAction("PublishRfq", "Rfq");
        }

    }
}