using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.App.Service;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class AccountsController : Controller
    {
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
                Tuple<VENDOR_LOGIN, EQResult> _tpl = AccountsService.UserLogin(obj);
                if (_tpl.Item2.SUCCESS && _tpl.Item2.ROWS == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                //ModelState.AddModelError("", _tpl.Item2.MESSAGES);
                ModelState.AddModelError("", "User Id/Password is wrong");
            }
            return View(obj);
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
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
                    ModelState.AddModelError("", "Select Registration Type Purchaser or Supplier");
                }
                else
                {
                    obj.VENDOR_ID = Guid.NewGuid().ToString();
                    EQResult _tpl = AccountsService.registration(obj);
                    if (_tpl.SUCCESS && _tpl.ROWS == 1)
                    {
                        TempData["regObj"] = obj;
                        TempData["regOk"] = _tpl;
                        return RedirectToAction("RegistrationSuccessfull");
                    }
                    else if (_tpl.ROWS == 99)
                    {
                        ModelState.AddModelError("", _tpl.MESSAGES);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Something went wrong, try again");
                    }
                }
            }
            DropDownFor_Signup();
            return View(obj);
        }

        public ActionResult RegistrationSuccessfull()
        {
            //Create a email confirmation link here



            VENDOR obj = new VENDOR();
            obj.ORGANIZATION_NAME = "Your Org Name";
            obj.VENDOR_EMAIL = "a..z@gmail.com";

            if (TempData["regObj"] != null && TempData["regOk"] != null)
            {
                obj = (VENDOR)TempData["regObj"];
                obj.VENDOR_EMAIL = "..." + obj.VENDOR_EMAIL.Remove(3);
                EQResult _tpl = (EQResult)TempData["regOk"];
            }
            return View(obj);
        }

        public ActionResult RegistrationConfirmation(string id)
        {
            EQResult _tpl = AccountsService.email_confirmation(id);
            TempData["regOk"] = (_tpl.ROWS) == 1 ? "OK" : "NO";
            return View();
        }

        #endregion






        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(VENDOR_LOGIN obj)
        {
            return View(obj);
        }



        [HttpGet]
        public ActionResult UserProfile(string id)
        {
            DropDownFor_Signup();
            return View(new VENDOR_DETAILS().getAll());
        }
        [HttpPost]
        public ActionResult UserProfile(VENDOR_DETAILS obj, HttpPostedFileBase ProfilePicture)
        {

            DropDownFor_Signup();
            return View(new VENDOR_DETAILS().getAll());
        }

        public ActionResult ChangePassword()
        {
            CHANGE_PASWD obj = new CHANGE_PASWD();
            return PartialView(obj);
        }


        [HttpPost]
        public ActionResult ChangePassword(CHANGE_PASWD obj)
        {
            return RedirectToAction("Logout");
        }


        public void DropDownFor_Signup()
        {
            ViewBag.COUNTRY_NAME = DropDownList_All_Country();

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
    }
}