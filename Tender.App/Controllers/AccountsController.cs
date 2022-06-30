using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tender.App.Service;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class AccountsController : Controller
    {
        private object _he;
        #region Login_Logout
        [HttpGet]
        public ActionResult Login()
        {
            return View(new VENDOR_LOGIN());
        }
        [HttpPost]
        public ActionResult Login(VENDOR_LOGIN obj)
        {
            if (ModelState.IsValid)
            {
                Tuple<VENDER_SESSION, EQResult> _tpl = AccountsService.UserLogin(obj);
                if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
                {
                    Session["ssUser"] = _tpl.Item1;
                    Session["_vendorUserId"] = _tpl.Item1.VENDOR_USER_ID;
                    Session["_vendorEmail"] = _tpl.Item1.VENDOR_EMAIL;
                    Session["_orgName"] = _tpl.Item1.ORGANIZATION_NAME;
                    Session["vendorId"] = _tpl.Item1.VENDOR_ID;
                    Session["_supplierStatus"] = _tpl.Item1.SUPPLIER;
                    Session["_purcherserStatus"] = _tpl.Item1.PURCHASER;

                    return RedirectToAction("Index", "Home");
                }
                //ModelState.AddModelError("", _tpl.Item2.MESSAGES);
                ModelState.AddModelError("", "User Id/Password is wrong");
            }
            return View(obj);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return Redirect("Login");
        }
        #endregion


        #region Registration
        [HttpGet]
        public ActionResult Registration()
        {
            DropDownFor_Signup();
            return View(new VENDOR());
        }
        [HttpPost]
        public ActionResult Registration(VENDOR obj)
        {
            obj.PURCHASER = obj.PURCHASER_X == "on" ? 1 : 0;
            obj.PURCHASER_NOTIFY = obj.PURCHASER_NOTIFY_X == "on" ? 1 : 0;
            obj.SUPPLIER = obj.SUPPLIER_X == "on" ? 1 : 0;
            obj.SUPPLIER_NOTIFY = obj.SUPPLIER_NOTIFY_X == "on" ? 1 : 0;

            if (ModelState.IsValid)
            {
                if (obj.SUPPLIER == 0 && obj.PURCHASER == 0)
                {
                    ModelState.AddModelError("", "Select Supplier");
                }
                else if (obj.SUPPLIER == 1 && obj.PURCHASER == 1)
                {
                    ModelState.AddModelError("", "Select Registration Type Purchaser or Supplier any one");
                }
                else
                {
                    obj.VENDOR_ID = Guid.NewGuid().ToString();
                    EQResult _tpl = AccountsService.registration(obj);
                    if (_tpl.SUCCESS && _tpl.ROWS == 1)
                    {
                        Random rnd = new Random();
                        int confirmationToken = rnd.Next(000000, 999999);

                        MAIL_NOTIFICATION_TOKEN tokenObj = new MAIL_NOTIFICATION_TOKEN();
                        tokenObj.VENDOR_EMAIL = obj.VENDOR_EMAIL;
                        tokenObj.TOKEN = confirmationToken;

                        EQResult _tpl1 = AccountsService.SaveToken(tokenObj, "R");
                        if (_tpl1.SUCCESS)
                        {
                            string mailbody = "Your registration confirmation email is " + obj.VENDOR_EMAIL + " that confirm a verification Code is: <h3>" + confirmationToken + "</h3>";

                            SendEMail(obj.VENDOR_EMAIL, "Registration Confirmation Code for http://rfq.prangroup.com/", mailbody);
                            TempData["msg"] = AlertService.SaveSuccess("Please check your email");
                            ViewBag.VENDOR_EMAIL = obj.VENDOR_EMAIL;
                            TempData["regObj"] = obj;
                            TempData["regOk"] = _tpl;
                            return RedirectToAction("RegistrationSuccessfull", obj);
                        }
                    }
                    else if (_tpl.ROWS == 99)
                    {
                        ModelState.AddModelError("", _tpl.MESSAGES);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong, try again");
            }
            DropDownFor_Signup();
            return View(obj);
        }
        //again send notification code method for ajax

        public ActionResult AgainSendToken(string email)
        {
            var VENDOR_EMAIL = email;
            if (!String.IsNullOrEmpty(VENDOR_EMAIL.ToString()))
            {
                Random rnd = new Random();
                int confirmationToken = rnd.Next(000000, 999999);

                MAIL_NOTIFICATION_TOKEN tokenObj = new MAIL_NOTIFICATION_TOKEN();
                tokenObj.VENDOR_EMAIL = VENDOR_EMAIL;
                tokenObj.TOKEN = confirmationToken;

                EQResult _tpl1 = AccountsService.SaveToken(tokenObj, "R");
                if (_tpl1.SUCCESS)
                {
                    string mailbody = "Your registration confirmation email is " + VENDOR_EMAIL + " that confirm a verification Code is: <h3>" + confirmationToken + "</h3>";

                    SendEMail(VENDOR_EMAIL, "Registration Confirmation Code for http://rfq.prangroup.com/", mailbody);
                    TempData["msg"] = AlertService.SaveSuccess("Please check your email");
                    ViewBag.VENDOR_EMAIL = VENDOR_EMAIL;
                    //TempData["regObj"] = obj;
                    //TempData["regOk"] = _tpl;
                    //return RedirectToAction("RegistrationSuccessfull", obj);
                    return Json("Successful", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Error ! Please again regstration", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegistrationSuccessfull(VENDOR obj)
        {

            return View(obj);
        }
        [HttpPost]
        public ActionResult RegistrationSuccessfull(VENDOR obj, int token)
        {
            if (String.IsNullOrEmpty(token.ToString()))
            {
                //TempData["msg"] = AlertService.SaveSuccess("Please enter verification code");
                return View(obj);
            }
            else
            {
                MAIL_NOTIFICATION_TOKEN tokenObj = new MAIL_NOTIFICATION_TOKEN();
                tokenObj.VENDOR_EMAIL = obj.VENDOR_EMAIL;
                tokenObj.TOKEN = token;
                Tuple<MAIL_NOTIFICATION_TOKEN, EQResult> _tpl = AccountsService.EmailTokenVerify(tokenObj, "R");
                if (!string.IsNullOrEmpty(_tpl.Item1.VENDOR_EMAIL))
                {
                    TempData["msg"] = AlertService.SaveSuccess("Verification Code Matched Successful");
                    return RedirectToAction("RegistrationConfirmation", obj);
                    //return RedirectToAction("RegistrationConfirmation", "Accounts", new { id = tokenObj.VENDOR_EMAIL });
                }
                else
                {
                    TempData["msg"] = AlertService.SaveWarningOK("Verification code not matched");
                    return View(obj);
                }
            }
        }
        public ActionResult RegistrationConfirmation(VENDOR obj)
        {
            EQResult _tpl = AccountsService.email_confirmation(obj.VENDOR_EMAIL);
            TempData["regOk"] = (_tpl.ROWS) == 1 ? "OK" : "NO";
            return View();
        }

        #endregion

        public ActionResult ForgotPassword()
        {
            VENDOR_LOGIN obj = new VENDOR_LOGIN();
            return View(obj);
        }

        [HttpPost]
        public ActionResult ForgotPassword(VENDOR_LOGIN obj)
        {
            Tuple<VENDOR_LOGIN, EQResult> _tpl = AccountsService.EmailVerification(obj);
            string emailAddress = _tpl.Item1.VENDOR_EMAIL;
            if (!string.IsNullOrEmpty(emailAddress))
            {
                Random rnd = new Random();
                int confirmationToken = rnd.Next(000000, 999999);

                MAIL_NOTIFICATION_TOKEN tokenObj = new MAIL_NOTIFICATION_TOKEN();
                tokenObj.VENDOR_EMAIL = obj.VENDOR_EMAIL;
                tokenObj.TOKEN = confirmationToken;

                EQResult _tpl1 = AccountsService.SaveToken(tokenObj, "F");
                if (_tpl1.SUCCESS)
                {
                    SendEMail(obj.VENDOR_EMAIL, "Reset password code for http://rfq.prangroup.com/", confirmationToken.ToString());
                    TempData["msg"] = AlertService.SaveSuccess("Please check your email");
                    ViewBag.VENDOR_EMAIL = obj.VENDOR_EMAIL;
                    return RedirectToAction("TokenMatch", obj);
                }
                else
                {
                    TempData["msg"] = AlertService.SaveWarningOK(_tpl1.MESSAGES);
                }
            }
            else
            {
                TempData["msg"] = AlertService.SaveWarningOK("email not found");
            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult TokenMatch(VENDOR_LOGIN obj)
        {
            return View(obj);
        }
        [HttpPost]
        public ActionResult TokenMatch(VENDOR_LOGIN _obj, int tokenNumber)
        {
            MAIL_NOTIFICATION_TOKEN obj = new MAIL_NOTIFICATION_TOKEN();
            obj.VENDOR_EMAIL = _obj.VENDOR_EMAIL;
            obj.TOKEN = tokenNumber;
            Tuple<MAIL_NOTIFICATION_TOKEN, EQResult> _tpl = AccountsService.EmailTokenVerify(obj, "F");
            if (!string.IsNullOrEmpty(_tpl.Item1.VENDOR_EMAIL))
            {
                TempData["msg"] = AlertService.SaveSuccess("Verification Code Matched Successful");
                return RedirectToAction("ResetPassword", _obj);
            }
            else
            {
                TempData["msg"] = AlertService.SaveWarningOK("Verification code not matched");
            }
            return View(_obj);
        }

        [HttpGet]
        public ActionResult ResetPassword(VENDOR_LOGIN obj)
        {
            return View(obj);
        }

        [HttpPost]
        public ActionResult ResetPassword(VENDOR_LOGIN obj, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                EQResult _tpl1 = AccountsService.ResetPassword(obj);
                if (_tpl1.SUCCESS)
                {
                    TempData["msg"] = AlertService.SaveSuccess("Password Reset Successful");
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["msg"] = AlertService.SaveWarningOK(_tpl1.MESSAGES);
                }
            }
            return View(obj);
        }

        private void SendEMail(string emailid, string subject, string body)
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
            msg.To.Add(new MailAddress(emailid));

            msg.Subject = subject;
            //message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
            msg.Body = body;
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
        private void SendRegistrationEmail(string emailid, string subject, string body)
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
            msg.To.Add(new MailAddress(emailid));

            msg.Subject = subject;
            //message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
            msg.Body = body;
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


        [UserSessionCheck]
        [HttpGet]
        public ActionResult UserProfile(string id)
        {
            VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
            DropDownFor_Signup();
            Tuple<VENDOR_DETAILS, EQResult> _tpl = AccountsService.getProfile(snObj.VENDOR_ID);
            if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
            {
                _tpl.Item1.VENDOR_CATEGORY = AccountsService.getVENDOR_CATEGORY(snObj.VENDOR_ID).Item1;
                _tpl.Item1.VENDOR_CERTIFICATE = AccountsService.getVENDOR_CERTIFICATE(snObj.VENDOR_ID).Item1;
                _tpl.Item1.VENDOR_DOCUMENTS = AccountsService.getVENDOR_DOCUMENTS(snObj.VENDOR_ID).Item1;
                _tpl.Item1.VENDOR_PRODUCTS = AccountsService.getVENDOR_PRODUCTS(snObj.VENDOR_ID).Item1;
                _tpl.Item1.VENDOR_PRODUCTS_GROUP = AccountsService.getVENDOR_PRODUCTS_GROUP(snObj.VENDOR_ID).Item1;
                _tpl.Item1.VENDOR_DOCUMENTS_LIST = AccountsService.getVENDOR_FILE_LIST(snObj.VENDOR_ID).Item1;
                _tpl.Item1.VENDOR_COMPANY = AccountsService.getVENDOR_COMPANY(snObj.VENDOR_ID).Item1;
                return View(_tpl.Item1);
            }
            return RedirectToAction("Index", "Home");
        }
        [UserSessionCheck]
        [HttpPost]
        public ActionResult UserProfile(VENDOR_DETAILS obj, HttpPostedFileBase ProfilePicture)
        {
            VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
            DropDownFor_Signup();
            if (ModelState.IsValid)
            {
                obj.PURCHASER = obj.PURCHASER_X == "on" ? 1 : 0;
                obj.PURCHASER_NOTIFY = obj.PURCHASER_NOTIFY_X == "on" ? 1 : 0;
                obj.SUPPLIER = obj.SUPPLIER_X == "on" ? 1 : 0;
                obj.SUPPLIER_NOTIFY = obj.SUPPLIER_NOTIFY_X == "on" ? 1 : 0;
                if (AccountsService.checkUserId(obj.VENDOR_USER_ID, snObj.VENDOR_ID).Item1.VENDOR_USER_ID != null)
                {
                    ModelState.AddModelError("", "This User Id Already Exist");
                    return View(obj);
                }
                else if (obj.PURCHASER_NOTIFY == 1 && obj.SUPPLIER_NOTIFY == 1)
                {
                    ModelState.AddModelError("", "Please Select Purchaser notification  or Supplier notification, choose One ");
                    return View(obj);
                }
                else
                {
                    if (ProfilePicture != null)
                    {
                        if (ProfilePicture.ContentLength <= 102400)
                        {
                            string pic = System.IO.Path.GetFileName(ProfilePicture.FileName);
                            string path = System.IO.Path.Combine(
                                                   Server.MapPath("~/App_Asset/dist/img/profileImage"), pic);
                            var extension = Path.GetExtension(ProfilePicture.FileName);
                            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == "jpeg")
                            {
                                ProfilePicture.SaveAs(path);
                                obj.PROFILE_IMAGE = "App_Asset/dist/img/profileImage/" + pic;
                                var _tpl = AccountsService.profileUpdate(obj);
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    ProfilePicture.InputStream.CopyTo(ms);
                                    byte[] array = ms.GetBuffer();
                                }
                                if (_tpl.SUCCESS == true)
                                {
                                    TempData["msg"] = AlertService.SaveSuccess("Profile Update Successfully");
                                }
                                else if (_tpl.SUCCESS == false)
                                {
                                    TempData["msg"] = AlertService.SaveSuccess(_tpl.MESSAGES);
                                }
                                return RedirectToAction("UserProfile", "Accounts");
                            }
                            else
                            {
                                TempData["msg"] = AlertService.SaveWarningOK("Image format must be jpg, jpeg,png");
                                return View(obj);
                            }
                        }
                        else
                        {
                            TempData["msg"] = AlertService.SaveWarningOK("Image size less then 100 KB");
                            return View(obj);
                        }

                    }
                    var _tpl1 = AccountsService.profileUpdate(obj);
                    if (_tpl1.SUCCESS == true)
                    {
                        TempData["msg"] = AlertService.SaveSuccess("Profile Update Successfully");
                    }
                    else if (_tpl1.SUCCESS == false)
                    {
                        TempData["msg"] = AlertService.SaveSuccess(_tpl1.MESSAGES);
                    }

                }

            }
            return RedirectToAction("UserProfile", "Accounts");
        }

        //partial view method
        [UserSessionCheck]
        [HttpPost]
        public ActionResult UploadDocument(VENDOR_FILE obj, HttpPostedFileBase Document)
        {
            if (ModelState.IsValid)
            {
                VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
                if (Document != null && obj.DOC_ID != null)
                {
                    if (Document.ContentLength <= 10240*1024*5)
                    {
                        string pic = System.IO.Path.GetFileName(Document.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/App_Asset/SDocument"), pic);
                        var extension = Path.GetExtension(Document.FileName);
                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg" ||extension.ToLower()==".pdf" || extension.ToLower() == ".docx")
                        {
                            Document.SaveAs(path);
                            obj.FILE_PATH = path;
                            obj.VENDOR_ID = snObj.VENDOR_ID;
                            obj.FILE_ID = Guid.NewGuid().ToString();
                            obj.FILE_NAME = pic;
                            obj.FILE_TYPE = extension;
                            obj.FILE_NUMBER = obj.DOC_ID;
                            obj.DOC_ID = obj.DOC_ID;
                            obj.DOCUMENT_TYPE = obj.DOCUMENT_TYPE;
                            obj.CREATE_USER = snObj.VENDOR_ID;
                            obj.CREATE_DEVICE = System.Environment.MachineName;
                            var _tpl = AccountsService.documentSave(obj);
                            if (_tpl.SUCCESS == true)
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    Document.InputStream.CopyTo(ms);
                                    byte[] array = ms.GetBuffer();
                                }
                                TempData["msg"] = AlertService.SaveSuccess("File Upload  Successfully");
                            }
                            else if (_tpl.MESSAGES == "ORA-00001: unique constraint (TND.SYS_C0014456) violated")
                            {
                                TempData["msg"] = AlertService.SaveWarningOK("This file name is already exist ! Please rename your file");
                                return RedirectToAction("UserProfile", "Accounts");
                            }
                            else if (_tpl.SUCCESS == false)
                            {
                                TempData["msg"] = AlertService.SaveWarningOK(_tpl.MESSAGES);
                            }
                           
                            return RedirectToAction("UserProfile", "Accounts");
                        }
                        else
                        {
                            TempData["msg"] = AlertService.SaveWarningOK("File format must be jpg, jpeg,png,pdf,docx");
                            return View(obj);
                        }
                    }
                    else
                    {
                        TempData["msg"] = AlertService.SaveWarningOK("File size less then 5 MB");
                        return View(obj);
                    }
                }
            }
            return View("UserProfile");
        }

        [UserSessionCheck]
        [HttpGet]
        public ActionResult supplierProfile(string id)
        {
            VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
            DropDownFor_Signup();
            Tuple<VENDOR_DETAILS, EQResult> _tpl = AccountsService.getProfile(id);
            if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
            {
                _tpl.Item1.VENDOR_CATEGORY = AccountsService.getVENDOR_CATEGORY(id).Item1;
                _tpl.Item1.VENDOR_CERTIFICATE = AccountsService.getVENDOR_CERTIFICATE(id).Item1;
                _tpl.Item1.VENDOR_DOCUMENTS = AccountsService.getVENDOR_DOCUMENTS(id).Item1;
                _tpl.Item1.VENDOR_PRODUCTS = AccountsService.getVENDOR_PRODUCTS(id).Item1;
                _tpl.Item1.VENDOR_PRODUCTS_GROUP = AccountsService.getVENDOR_PRODUCTS_GROUP(id).Item1;
                _tpl.Item1.VENDOR_DOCUMENTS_LIST = AccountsService.getVENDOR_FILE_LIST(id).Item1;
                return View(_tpl.Item1);

            }
            return RedirectToAction("Index", "Home");
        }

        [UserSessionCheck]
        public void FollowMe(string cId, string cNm)
        {
            VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
            EQResult _tpl = AccountsService.add_edit_follow_me(snObj.VENDOR_ID, cId, cNm);
        }

        [UserSessionCheck]
        [HttpPost]
        public ActionResult ChangePassword(CHANGE_PASWD obj)
        {
            if (ModelState.IsValid)
            {
                VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
                EQResult _tpl = AccountsService.change_password(snObj.VENDOR_ID, obj);
                if (_tpl.SUCCESS && _tpl.ROWS == 1)
                {
                    return RedirectToAction("UserProfile");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong, try again");
                }
            }
            return View("UserProfile", new VENDOR_DETAILS().getAll());
        }

        [UserSessionCheck]
        public ActionResult ViewAllSupplier()
        {

            List<VENDOR_DETAILS> obj = AccountsService.supplierList().Item1;
            return View(obj);

        }


        public void DropDownFor_Signup()
        {
            ViewBag.COUNTRY_NAME = DropDownList_All_Country();
            ViewBag.CERTIFICATE_ID = new SelectList(TenderService.getvendorDoc().Item1.ToList(), "CERTIFICATE_ID", "CERTIFICATE_NAME");
        }
        public SelectList DropDownList_All_Country()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {
                new SelectListItem { Text = "Afghanistan", Value ="AFGHANISTAN"},
                    new SelectListItem { Text = "Albania", Value ="ALBANIA"},
                    new SelectListItem { Text = "Algeria", Value ="ALGERIA"},
                    new SelectListItem { Text = "Andorra", Value ="ANDORRA"},
                    new SelectListItem { Text = "Angola", Value ="ANGOLA"},
                    new SelectListItem { Text = "Antigua and Barbuda", Value ="ANTIGUA AND BARBUDA"},
                    new SelectListItem { Text = "Argentina", Value ="ARGENTINA"},
                    new SelectListItem { Text = "Armenia", Value ="ARMENIA"},
                    new SelectListItem { Text = "Australia", Value ="AUSTRALIA"},
                    new SelectListItem { Text = "Austria", Value ="AUSTRIA"},
                    new SelectListItem { Text = "Azerbaijan", Value ="AZERBAIJAN"},
                    new SelectListItem { Text = "The Bahamas", Value ="THE BAHAMAS"},
                    new SelectListItem { Text = "Bahrain", Value ="BAHRAIN"},
                    new SelectListItem { Text = "Bangladesh", Value ="BANGLADESH"},
                    new SelectListItem { Text = "Barbados", Value ="BARBADOS"},
                    new SelectListItem { Text = "Belarus", Value ="BELARUS"},
                    new SelectListItem { Text = "Belgium", Value ="BELGIUM"},
                    new SelectListItem { Text = "Belize", Value ="BELIZE"},
                    new SelectListItem { Text = "Benin", Value ="BENIN"},
                    new SelectListItem { Text = "Bhutan", Value ="BHUTAN"},
                    new SelectListItem { Text = "Bolivia", Value ="BOLIVIA"},
                    new SelectListItem { Text = "Bosnia and Herzegovina", Value ="BOSNIA AND HERZEGOVINA"},
                    new SelectListItem { Text = "Botswana", Value ="BOTSWANA"},
                    new SelectListItem { Text = "Brazil", Value ="BRAZIL"},
                    new SelectListItem { Text = "Brunei", Value ="BRUNEI"},
                    new SelectListItem { Text = "Bulgaria", Value ="BULGARIA"},
                    new SelectListItem { Text = "Burkina Faso", Value ="BURKINA FASO"},
                    new SelectListItem { Text = "Burundi", Value ="BURUNDI"},
                    new SelectListItem { Text = "Cambodia", Value ="CAMBODIA"},
                    new SelectListItem { Text = "Cameroon", Value ="CAMEROON"},
                    new SelectListItem { Text = "Canada", Value ="CANADA"},
                    new SelectListItem { Text = "Cape Verde", Value ="CAPE VERDE"},
                    new SelectListItem { Text = "Central African Republic", Value ="CENTRAL AFRICAN REPUBLIC"},
                    new SelectListItem { Text = "Chad", Value ="CHAD"},
                    new SelectListItem { Text = "Chile", Value ="CHILE"},
                    new SelectListItem { Text = "China", Value ="CHINA"},
                    new SelectListItem { Text = "Colombia", Value ="COLOMBIA"},
                    new SelectListItem { Text = "Comoros", Value ="COMOROS"},
                    new SelectListItem { Text = "Congo, Republic of the", Value ="CONGO, REPUBLIC OF THE"},
                    new SelectListItem { Text = "Congo, Democratic Republic of the", Value ="CONGO, DEMOCRATIC REPUBLIC OF THE"},
                    new SelectListItem { Text = "Costa Rica", Value ="COSTA RICA"},
                    new SelectListItem { Text = "Cote d'Ivoire", Value ="COTE D'IVOIRE"},
                    new SelectListItem { Text = "Croatia", Value ="CROATIA"},
                    new SelectListItem { Text = "Cuba", Value ="CUBA"},
                    new SelectListItem { Text = "Cyprus", Value ="CYPRUS"},
                    new SelectListItem { Text = "Czech Republic", Value ="CZECH REPUBLIC"},
                    new SelectListItem { Text = "Denmark", Value ="DENMARK"},
                    new SelectListItem { Text = "Djibouti", Value ="DJIBOUTI"},
                    new SelectListItem { Text = "Dominica", Value ="DOMINICA"},
                    new SelectListItem { Text = "Dominican Republic", Value ="DOMINICAN REPUBLIC"},
                    new SelectListItem { Text = "East Timor (Timor-Leste)", Value ="EAST TIMOR (TIMOR-LESTE)"},
                    new SelectListItem { Text = "Ecuador", Value ="ECUADOR"},
                    new SelectListItem { Text = "Egypt", Value ="EGYPT"},
                    new SelectListItem { Text = "El Salvador", Value ="EL SALVADOR"},
                    new SelectListItem { Text = "Equatorial Guinea", Value ="EQUATORIAL GUINEA"},
                    new SelectListItem { Text = "Eritrea", Value ="ERITREA"},
                    new SelectListItem { Text = "Estonia", Value ="ESTONIA"},
                    new SelectListItem { Text = "Ethiopia", Value ="ETHIOPIA"},
                    new SelectListItem { Text = "Fiji", Value ="FIJI"},
                    new SelectListItem { Text = "Finland", Value ="FINLAND"},
                    new SelectListItem { Text = "France", Value ="FRANCE"},
                    new SelectListItem { Text = "Gabon", Value ="GABON"},
                    new SelectListItem { Text = "The Gambia", Value ="THE GAMBIA"},
                    new SelectListItem { Text = "Georgia", Value ="GEORGIA"},
                    new SelectListItem { Text = "Germany", Value ="GERMANY"},
                    new SelectListItem { Text = "Ghana", Value ="GHANA"},
                    new SelectListItem { Text = "Greece", Value ="GREECE"},
                    new SelectListItem { Text = "Grenada", Value ="GRENADA"},
                    new SelectListItem { Text = "Guatemala", Value ="GUATEMALA"},
                    new SelectListItem { Text = "Guinea", Value ="GUINEA"},
                    new SelectListItem { Text = "Guinea-Bissau", Value ="GUINEA-BISSAU"},
                    new SelectListItem { Text = "Guyana", Value ="GUYANA"},
                    new SelectListItem { Text = "Haiti", Value ="HAITI"},
                    new SelectListItem { Text = "Honduras", Value ="HONDURAS"},
                    new SelectListItem { Text = "Hungary", Value ="HUNGARY"},
                    new SelectListItem { Text = "Iceland", Value ="ICELAND"},
                    new SelectListItem { Text = "India", Value ="INDIA"},
                    new SelectListItem { Text = "Indonesia", Value ="INDONESIA"},
                    new SelectListItem { Text = "Iran", Value ="IRAN"},
                    new SelectListItem { Text = "Iraq", Value ="IRAQ"},
                    new SelectListItem { Text = "Ireland", Value ="IRELAND"},
                    new SelectListItem { Text = "Israel", Value ="ISRAEL"},
                    new SelectListItem { Text = "Italy", Value ="ITALY"},
                    new SelectListItem { Text = "Jamaica", Value ="JAMAICA"},
                    new SelectListItem { Text = "Japan", Value ="JAPAN"},
                    new SelectListItem { Text = "Jordan", Value ="JORDAN"},
                    new SelectListItem { Text = "Kazakhstan", Value ="KAZAKHSTAN"},
                    new SelectListItem { Text = "Kenya", Value ="KENYA"},
                    new SelectListItem { Text = "Kiribati", Value ="KIRIBATI"},
                    new SelectListItem { Text = "Korea, North", Value ="KOREA, NORTH"},
                    new SelectListItem { Text = "Korea, South", Value ="KOREA, SOUTH"},
                    new SelectListItem { Text = "Kosovo", Value ="KOSOVO"},
                    new SelectListItem { Text = "Kuwait", Value ="KUWAIT"},
                    new SelectListItem { Text = "Kyrgyzstan", Value ="KYRGYZSTAN"},
                    new SelectListItem { Text = "Laos", Value ="LAOS"},
                    new SelectListItem { Text = "Latvia", Value ="LATVIA"},
                    new SelectListItem { Text = "Lebanon", Value ="LEBANON"},
                    new SelectListItem { Text = "Lesotho", Value ="LESOTHO"},
                    new SelectListItem { Text = "Liberia", Value ="LIBERIA"},
                    new SelectListItem { Text = "Libya", Value ="LIBYA"},
                    new SelectListItem { Text = "Liechtenstein", Value ="LIECHTENSTEIN"},
                    new SelectListItem { Text = "Lithuania", Value ="LITHUANIA"},
                    new SelectListItem { Text = "Luxembourg", Value ="LUXEMBOURG"},
                    new SelectListItem { Text = "Macedonia", Value ="MACEDONIA"},
                    new SelectListItem { Text = "Madagascar", Value ="MADAGASCAR"},
                    new SelectListItem { Text = "Malawi", Value ="MALAWI"},
                    new SelectListItem { Text = "Malaysia", Value ="MALAYSIA"},
                    new SelectListItem { Text = "Maldives", Value ="MALDIVES"},
                    new SelectListItem { Text = "Mali", Value ="MALI"},
                    new SelectListItem { Text = "Malta", Value ="MALTA"},
                    new SelectListItem { Text = "Marshall Islands", Value ="MARSHALL ISLANDS"},
                    new SelectListItem { Text = "Mauritania", Value ="MAURITANIA"},
                    new SelectListItem { Text = "Mauritius", Value ="MAURITIUS"},
                    new SelectListItem { Text = "Mexico", Value ="MEXICO"},
                    new SelectListItem { Text = "Micronesia, Federated States of", Value ="MICRONESIA, FEDERATED STATES OF"},
                    new SelectListItem { Text = "Moldova", Value ="MOLDOVA"},
                    new SelectListItem { Text = "Monaco", Value ="MONACO"},
                    new SelectListItem { Text = "Mongolia", Value ="MONGOLIA"},
                    new SelectListItem { Text = "Montenegro", Value ="MONTENEGRO"},
                    new SelectListItem { Text = "Morocco", Value ="MOROCCO"},
                    new SelectListItem { Text = "Mozambique", Value ="MOZAMBIQUE"},
                    new SelectListItem { Text = "Myanmar (Burma)", Value ="MYANMAR (BURMA)"},
                    new SelectListItem { Text = "Namibia", Value ="NAMIBIA"},
                    new SelectListItem { Text = "Nauru", Value ="NAURU"},
                    new SelectListItem { Text = "Nepal", Value ="NEPAL"},
                    new SelectListItem { Text = "Netherlands", Value ="NETHERLANDS"},
                    new SelectListItem { Text = "New Zealand", Value ="NEW ZEALAND"},
                    new SelectListItem { Text = "Nicaragua", Value ="NICARAGUA"},
                    new SelectListItem { Text = "Niger", Value ="NIGER"},
                    new SelectListItem { Text = "Nigeria", Value ="NIGERIA"},
                    new SelectListItem { Text = "Norway", Value ="NORWAY"},
                    new SelectListItem { Text = "Oman", Value ="OMAN"},
                    new SelectListItem { Text = "Pakistan", Value ="PAKISTAN"},
                    new SelectListItem { Text = "Palau", Value ="PALAU"},
                    new SelectListItem { Text = "Panama", Value ="PANAMA"},
                    new SelectListItem { Text = "Papua New Guinea", Value ="PAPUA NEW GUINEA"},
                    new SelectListItem { Text = "Paraguay", Value ="PARAGUAY"},
                    new SelectListItem { Text = "Peru", Value ="PERU"},
                    new SelectListItem { Text = "Philippines", Value ="PHILIPPINES"},
                    new SelectListItem { Text = "Poland", Value ="POLAND"},
                    new SelectListItem { Text = "Portugal", Value ="PORTUGAL"},
                    new SelectListItem { Text = "Qatar", Value ="QATAR"},
                    new SelectListItem { Text = "Romania", Value ="ROMANIA"},
                    new SelectListItem { Text = "Russia", Value ="RUSSIA"},
                    new SelectListItem { Text = "Rwanda", Value ="RWANDA"},
                    new SelectListItem { Text = "Saint Kitts and Nevis", Value ="SAINT KITTS AND NEVIS"},
                    new SelectListItem { Text = "Saint Lucia", Value ="SAINT LUCIA"},
                    new SelectListItem { Text = "Saint Vincent and the Grenadines", Value ="SAINT VINCENT AND THE GRENADINES"},
                    new SelectListItem { Text = "Samoa", Value ="SAMOA"},
                    new SelectListItem { Text = "San Marino", Value ="SAN MARINO"},
                    new SelectListItem { Text = "Sao Tome and Principe", Value ="SAO TOME AND PRINCIPE"},
                    new SelectListItem { Text = "Saudi Arabia", Value ="SAUDI ARABIA"},
                    new SelectListItem { Text = "Senegal", Value ="SENEGAL"},
                    new SelectListItem { Text = "Serbia", Value ="SERBIA"},
                    new SelectListItem { Text = "Seychelles", Value ="SEYCHELLES"},
                    new SelectListItem { Text = "Sierra Leone", Value ="SIERRA LEONE"},
                    new SelectListItem { Text = "Singapore", Value ="SINGAPORE"},
                    new SelectListItem { Text = "Slovakia", Value ="SLOVAKIA"},
                    new SelectListItem { Text = "Slovenia", Value ="SLOVENIA"},
                    new SelectListItem { Text = "Solomon Islands", Value ="SOLOMON ISLANDS"},
                    new SelectListItem { Text = "Somalia", Value ="SOMALIA"},
                    new SelectListItem { Text = "South Africa", Value ="SOUTH AFRICA"},
                    new SelectListItem { Text = "South Sudan", Value ="SOUTH SUDAN"},
                    new SelectListItem { Text = "Spain", Value ="SPAIN"},
                    new SelectListItem { Text = "Sri Lanka", Value ="SRI LANKA"},
                    new SelectListItem { Text = "Sudan", Value ="SUDAN"},
                    new SelectListItem { Text = "Suriname", Value ="SURINAME"},
                    new SelectListItem { Text = "Swaziland", Value ="SWAZILAND"},
                    new SelectListItem { Text = "Sweden", Value ="SWEDEN"},
                    new SelectListItem { Text = "Switzerland", Value ="SWITZERLAND"},
                    new SelectListItem { Text = "Syria", Value ="SYRIA"},
                    new SelectListItem { Text = "Taiwan", Value ="TAIWAN"},
                    new SelectListItem { Text = "Tajikistan", Value ="TAJIKISTAN"},
                    new SelectListItem { Text = "Tanzania", Value ="TANZANIA"},
                    new SelectListItem { Text = "Thailand", Value ="THAILAND"},
                    new SelectListItem { Text = "Togo", Value ="TOGO"},
                    new SelectListItem { Text = "Tonga", Value ="TONGA"},
                    new SelectListItem { Text = "Trinidad and Tobago", Value ="TRINIDAD AND TOBAGO"},
                    new SelectListItem { Text = "Tunisia", Value ="TUNISIA"},
                    new SelectListItem { Text = "Turkey", Value ="TURKEY"},
                    new SelectListItem { Text = "Turkmenistan", Value ="TURKMENISTAN"},
                    new SelectListItem { Text = "Tuvalu", Value ="TUVALU"},
                    new SelectListItem { Text = "Uganda", Value ="UGANDA"},
                    new SelectListItem { Text = "Ukraine", Value ="UKRAINE"},
                    new SelectListItem { Text = "United Arab Emirates", Value ="UNITED ARAB EMIRATES"},
                    new SelectListItem { Text = "United Kingdom", Value ="UNITED KINGDOM"},
                    new SelectListItem { Text = "United States of America", Value ="UNITED STATES OF AMERICA"},
                    new SelectListItem { Text = "Uruguay", Value ="URUGUAY"},
                    new SelectListItem { Text = "Uzbekistan", Value ="UZBEKISTAN"},
                    new SelectListItem { Text = "Vanuatu", Value ="VANUATU"},
                    new SelectListItem { Text = "Vatican City (Holy See)", Value ="VATICAN CITY (HOLY SEE)"},
                    new SelectListItem { Text = "Venezuela", Value ="VENEZUELA"},
                    new SelectListItem { Text = "Vietnam", Value ="VIETNAM"},
                    new SelectListItem { Text = "Yemen", Value ="YEMEN"},
                    new SelectListItem { Text = "Zambia", Value ="ZAMBIA"},
                    new SelectListItem { Text = "Zimbabwe", Value ="ZIMBABWE"},
                      }, "Value", "Text", 1);
            return DataList;
        }

        [HttpGet]
        public ActionResult DropDownFor_DocList(string DOCUMENT_TYPE)
        {
            VENDER_SESSION snObj = (VENDER_SESSION)Session["ssUser"];
            if (DOCUMENT_TYPE == "Documents") {
                var objList = TenderService.getVendorUploadDocument(snObj.VENDOR_ID).Item1.ToList();
                return Json(new SelectList(objList, "DOCUMENTS_ID", "DOCUMENTS_NAME"), JsonRequestBehavior.AllowGet);
            }
            else if(DOCUMENT_TYPE == "Certificate")
            {
                var objList = TenderService.getvendorUploadCertificate(snObj.VENDOR_ID).Item1.ToList();
                return Json(new SelectList(objList, "CERTIFICATE_ID", "CERTIFICATE_NAME"),JsonRequestBehavior.AllowGet);
            }
            return Json(new SelectList(null), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FileDownload(string id)
        {
            var path = AccountsService.filePath(id).Item1.FILE_PATH;
           // var path = data.Item1.;
            var fileName = AccountsService.filePath(id).Item1.FILE_NAME;
            if (path != null)
            {
                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(path);

                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            return Json("Invalid document");
        }
    }
}