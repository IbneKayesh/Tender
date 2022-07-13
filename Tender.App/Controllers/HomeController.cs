using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.App.Service;
using Tender.Models.Models;
using Aio.Db.MSSqlEF;
using Newtonsoft.Json;

namespace Tender.App.Controllers
{
    public class HomeController : Controller
    {
        [UserSessionCheck]
        public ActionResult Index()
        {

            VENDOR_COMPANY snPurcheserComObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];

            if (snPurcheserComObj == null) {
                snPurcheserComObj = new VENDOR_COMPANY();
            }

            List<RFQ_BIDDING> obj = QuotationService.winingsBid((string)Session["vendorId"]).Item1;
            ViewBag.WingingsBids = obj.Count();
            List<RFQ_BIDDING> ttbid = HomeService.applyBids((string)Session["vendorId"]).Item1;
            ViewBag.ApplyBids = ttbid.Count();
            List<RFQ_TENDER> newBid = HomeService.newBids((string)Session["vendorId"]).Item1;
            
            List<RFQ_TENDER_APPROVAL> NotWinBid = HomeService.NotWinBids((string)Session["vendorId"]).Item1;
            ViewBag.NotWinBid = NotWinBid.Count();

            List<RFQ_TenderView> supplierRFQ = QuotationService.getAllTenderForSupplier((string)Session["vendorId"]).Item1.Where(c => c.APPROVAL_ID == "0").ToList();
            ViewBag.NewBids = supplierRFQ.Count();


            List<RFQ_TENDER> totalRFQ = HomeService.totalRFQ((string)Session["vendorId"],snPurcheserComObj.COMPANY_ID).Item1;
            ViewBag.totalRFQ = totalRFQ.Count();
            List<RFQ_BIDDING> totalQuto = HomeService.totalQuto((string)Session["vendorId"], snPurcheserComObj.COMPANY_ID).Item1;
            ViewBag.totalQuto = totalQuto.Count();
            List<RFQ_TENDER> currentRFQ = HomeService.currentRFQ((string)Session["vendorId"],snPurcheserComObj.COMPANY_ID).Item1;
            ViewBag.currentRFQ = currentRFQ.Count();
            List<VENDOR> totalSupplier = HomeService.totalSupplier(snPurcheserComObj.COMPANY_ID).Item1;
            ViewBag.totalSupplier = totalSupplier.Count();

            List<VENDOR> newRegistration = HomeService.newRegistration(snPurcheserComObj.COMPANY_ID).Item1;
            ViewBag.newRegistration = newRegistration;
            ViewBag.newRegistrationCount = newRegistration.Count();

            List<RFQ_BIDDING> newQuto = HomeService.newQutoSubmit(snPurcheserComObj.COMPANY_ID).Item1;
            ViewBag.newSubmitQuot= newQuto;
            ViewBag.newSubmitQuotCount = newQuto.Count();

            List<int> repartitions = new List<int>();
            var montthlist = totalRFQ.Select(x => x.ADDED_DATE.ToString("MMMM")).Distinct();
            foreach (var item in montthlist)
            {
                repartitions.Add(totalRFQ.Count(x => x.ADDED_DATE.ToString("MMMM") == item));
            }
            ViewBag.MONTHLIST = montthlist;
            ViewBag.REP = repartitions;


            List<RFQ_BIDDING> WiningBidsNotify = QuotationService.winingsBidforNftn((string)Session["vendorId"]).Item1.ToList();
            DateTime da = DateTime.Today;
            var A = WiningBidsNotify.Where(c => c.APPROVAL_DATE<da);

            ViewBag.WiningBidsNotify = WiningBidsNotify;
            ViewBag.WiningBidsNotifyCount = WiningBidsNotify.Count();
           

            return View();
        }     
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}