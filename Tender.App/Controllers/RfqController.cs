using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.Models.Models;
using Tender.App.Service;
using Aio.Db.MSSqlEF;
using PagedList;
using System.Net.Mail;
using System.Net;

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
        [UserSessionCheck]
        public ActionResult NewRfqNotifySend(RFQ_BIDDING rfqObj)
        {
            if (rfqObj.RFQ_NUMBER != null)
            {
                VENDOR_COMPANY purcherObj = (VENDOR_COMPANY)Session["ssPurcheserCompany"];
                VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
                List<VENDOR_DETAILS> obj = AccountsService.RfqBysupplierList(rfqObj.RFQ_NUMBER).Item1;
                string AllmailID = string.Empty;
                string CCMailID = string.Empty;
                string MailBody = string.Empty;
                string MailSubject = "Request for qutation for "+ rfqObj.RFQ_NUMBER + " from " +purcherObj.COMPANY_NAME + "";
                foreach(var item in obj) {
                    AllmailID += item.VENDOR_EMAIL + ",";
                }

                AllmailID = AllmailID.Remove(AllmailID.Length - 1, 1);

                CCMailID = purcherObj.COMPANY_NAME;
                MailBody = "<html><body><div><h1 style ='color:blue;'><a href ='rfq.prangroup.com'> "+rfqObj.RFQ_NUMBER+"</a></h1><a href ='rfq.prangroup.com'> Please Check this website for sending quotation</a ></ div></body></html>";
               // MailBody = "From<br/><h4>" + snObj.ORGANIZATION_NAME + "</h4> <br/> <br/>  Dear Sir, <br/> " +
                    //"This is to bring to your kind attention that we need <b>" + rfqObj.TND_PRODUCTS_NAME + "</b>." +
                    //"We would be very grateful to you if you could provide us the quotation of the following item on urgent basis </br>" +
                    //"<style>table, th, td {border:1px solid black;}</style><table style='width: 100 %'>< tr >< th > Item Name </ th > th > Description </ th > < th > Request Quantity </ th >< th > Unit </ th ></ tr >" +
                    //"< tr >< td > " + rfqObj.TND_PRODUCTS_NAME + " </ td >< td > " + rfqObj.PRODUCTS_DESC + " </ td >< td > " + rfqObj.TND_PRODUCTS_QTY + " </ td >< td > " + rfqObj.UNIT + " </ td ></ tr ></ table > " +
                    //"<br/>Please treat this as a matter of urgency. We wish to have the quote within "+rfqObj.END_DATE.ToString("dd-MMM-yyyy")+"(BD Standard Time) date. Kindly also send details on the mode of payment, terms, and conditions so that we can start making the arrangements early enough. In case you have any questions or need clarification, kindly contact us on "+ snObj.VENDOR_EMAIL+"" +
                    //"<br/>We hope to hear from you soon regarding this quote request. We also look forward to a long-standing business relationship with you if this transaction is a success." +
                    //"<br/><br/>Your Faithfully<br/><h4>"+ snObj.ORGANIZATION_NAME+"</h4> <br>"+ snObj.VENDOR_EMAIL+"<br/>" +
                    //"<br/>Thank You";
                SendEMail(AllmailID,CCMailID,MailBody,MailSubject);

            }

            return Json("Mail Send Successfull",JsonRequestBehavior.AllowGet);
        }

        private void SendEMail(string AllmailID,string CCMailID, string MailBody, string MailSubject)
        {
            string Sender_Account = "lsms@prangroup.com";// emal.EMAL_SMTP;
            string Sender_Credential = "@dm!n@#sysmail";// emal.EMAL_PASS;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Sender_Account, Sender_Credential);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.From = new MailAddress("lsms@prangroup.com");
            string[] toAddress = AllmailID.Split(',');
            foreach (string n in toAddress) {
                msg.To.Add(new MailAddress(n));
            }
            //msg.To.Add(new MailAddress(AllmailID));
           // msg.CC.Add(new MailAddress(CCMailID));

            msg.Subject = MailSubject;
            //message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
            msg.Body = MailBody;
            msg.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "lsms@prangroup.com",  // replace with valid value
                    Password = "@dm!n@#sysmail"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "172.17.2.12";
                smtp.Port = 587;
                smtp.EnableSsl = false;
                smtp.Send(msg);
            }
        }


    }
}