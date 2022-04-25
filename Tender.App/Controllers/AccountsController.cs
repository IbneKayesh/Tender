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
                ModelState.AddModelError("", _tpl.Item2.MESSAGES);
            }
            return View(obj);
        }
        public ActionResult Logout()
        {
           return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Registration()
        {
            DropDownFor_Signup();
            return View(new VENDOR());
        }
        [HttpPost]
        public ActionResult Registration(VENDOR obj)
        {
            if (ModelState.IsValid)
            {
                EQResult _tpl = AccountsService.registration(obj);
                if (_tpl.SUCCESS && _tpl.ROWS == 1)
                {
                    return RedirectToAction("SendConfirmEmail", _tpl);
                }
                ModelState.AddModelError("", _tpl.MESSAGES);
            }
            DropDownFor_Signup();
            return View(obj);
        }

        public ActionResult RegistrationSuccessfull(VENDOR obj)
        {
            if (obj == null)
                obj = new VENDOR();
            obj.ORGANIZATION_NAME = "xxxx";
            obj.VENDOR_EMAIL = "a....1@gmail.com";
            return View(obj);
        }
        public ActionResult SendConfirmEmail(VENDOR obj)
        {
            return RedirectToAction("RegistrationSuccessfull", obj);
        }


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
        public ActionResult UserProfile(VENDOR_DETAILS obj , HttpPostedFileBase ProfilePicture)
        {

            DropDownFor_Signup();
            return View(new VENDOR_DETAILS().getAll());
        }

        [HttpPost]
        public ActionResult ChangePassword(String Email,string UserId, string OldPass, string NewPass,string ConfirmPass)
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
                new SelectListItem { Selected = true, Text = "Afghanistan", Value ="AFGHANISTAN"},
                    new SelectListItem { Selected = true, Text = "Albania", Value ="ALBANIA"},
                    new SelectListItem { Selected = true, Text = "Algeria", Value ="ALGERIA"},
                    new SelectListItem { Selected = true, Text = "Andorra", Value ="ANDORRA"},
                    new SelectListItem { Selected = true, Text = "Angola", Value ="ANGOLA"},
                    new SelectListItem { Selected = true, Text = "Antigua and Barbuda", Value ="ANTIGUA AND BARBUDA"},
                    new SelectListItem { Selected = true, Text = "Argentina", Value ="ARGENTINA"},
                    new SelectListItem { Selected = true, Text = "Armenia", Value ="ARMENIA"},
                    new SelectListItem { Selected = true, Text = "Australia", Value ="AUSTRALIA"},
                    new SelectListItem { Selected = true, Text = "Austria", Value ="AUSTRIA"},
                    new SelectListItem { Selected = true, Text = "Azerbaijan", Value ="AZERBAIJAN"},
                    new SelectListItem { Selected = true, Text = "The Bahamas", Value ="THE BAHAMAS"},
                    new SelectListItem { Selected = true, Text = "Bahrain", Value ="BAHRAIN"},
                    new SelectListItem { Selected = true, Text = "Bangladesh", Value ="BANGLADESH"},
                    new SelectListItem { Selected = true, Text = "Barbados", Value ="BARBADOS"},
                    new SelectListItem { Selected = true, Text = "Belarus", Value ="BELARUS"},
                    new SelectListItem { Selected = true, Text = "Belgium", Value ="BELGIUM"},
                    new SelectListItem { Selected = true, Text = "Belize", Value ="BELIZE"},
                    new SelectListItem { Selected = true, Text = "Benin", Value ="BENIN"},
                    new SelectListItem { Selected = true, Text = "Bhutan", Value ="BHUTAN"},
                    new SelectListItem { Selected = true, Text = "Bolivia", Value ="BOLIVIA"},
                    new SelectListItem { Selected = true, Text = "Bosnia and Herzegovina", Value ="BOSNIA AND HERZEGOVINA"},
                    new SelectListItem { Selected = true, Text = "Botswana", Value ="BOTSWANA"},
                    new SelectListItem { Selected = true, Text = "Brazil", Value ="BRAZIL"},
                    new SelectListItem { Selected = true, Text = "Brunei", Value ="BRUNEI"},
                    new SelectListItem { Selected = true, Text = "Bulgaria", Value ="BULGARIA"},
                    new SelectListItem { Selected = true, Text = "Burkina Faso", Value ="BURKINA FASO"},
                    new SelectListItem { Selected = true, Text = "Burundi", Value ="BURUNDI"},
                    new SelectListItem { Selected = true, Text = "Cambodia", Value ="CAMBODIA"},
                    new SelectListItem { Selected = true, Text = "Cameroon", Value ="CAMEROON"},
                    new SelectListItem { Selected = true, Text = "Canada", Value ="CANADA"},
                    new SelectListItem { Selected = true, Text = "Cape Verde", Value ="CAPE VERDE"},
                    new SelectListItem { Selected = true, Text = "Central African Republic", Value ="CENTRAL AFRICAN REPUBLIC"},
                    new SelectListItem { Selected = true, Text = "Chad", Value ="CHAD"},
                    new SelectListItem { Selected = true, Text = "Chile", Value ="CHILE"},
                    new SelectListItem { Selected = true, Text = "China", Value ="CHINA"},
                    new SelectListItem { Selected = true, Text = "Colombia", Value ="COLOMBIA"},
                    new SelectListItem { Selected = true, Text = "Comoros", Value ="COMOROS"},
                    new SelectListItem { Selected = true, Text = "Congo, Republic of the", Value ="CONGO, REPUBLIC OF THE"},
                    new SelectListItem { Selected = true, Text = "Congo, Democratic Republic of the", Value ="CONGO, DEMOCRATIC REPUBLIC OF THE"},
                    new SelectListItem { Selected = true, Text = "Costa Rica", Value ="COSTA RICA"},
                    new SelectListItem { Selected = true, Text = "Cote d'Ivoire", Value ="COTE D'IVOIRE"},
                    new SelectListItem { Selected = true, Text = "Croatia", Value ="CROATIA"},
                    new SelectListItem { Selected = true, Text = "Cuba", Value ="CUBA"},
                    new SelectListItem { Selected = true, Text = "Cyprus", Value ="CYPRUS"},
                    new SelectListItem { Selected = true, Text = "Czech Republic", Value ="CZECH REPUBLIC"},
                    new SelectListItem { Selected = true, Text = "Denmark", Value ="DENMARK"},
                    new SelectListItem { Selected = true, Text = "Djibouti", Value ="DJIBOUTI"},
                    new SelectListItem { Selected = true, Text = "Dominica", Value ="DOMINICA"},
                    new SelectListItem { Selected = true, Text = "Dominican Republic", Value ="DOMINICAN REPUBLIC"},
                    new SelectListItem { Selected = true, Text = "East Timor (Timor-Leste)", Value ="EAST TIMOR (TIMOR-LESTE)"},
                    new SelectListItem { Selected = true, Text = "Ecuador", Value ="ECUADOR"},
                    new SelectListItem { Selected = true, Text = "Egypt", Value ="EGYPT"},
                    new SelectListItem { Selected = true, Text = "El Salvador", Value ="EL SALVADOR"},
                    new SelectListItem { Selected = true, Text = "Equatorial Guinea", Value ="EQUATORIAL GUINEA"},
                    new SelectListItem { Selected = true, Text = "Eritrea", Value ="ERITREA"},
                    new SelectListItem { Selected = true, Text = "Estonia", Value ="ESTONIA"},
                    new SelectListItem { Selected = true, Text = "Ethiopia", Value ="ETHIOPIA"},
                    new SelectListItem { Selected = true, Text = "Fiji", Value ="FIJI"},
                    new SelectListItem { Selected = true, Text = "Finland", Value ="FINLAND"},
                    new SelectListItem { Selected = true, Text = "France", Value ="FRANCE"},
                    new SelectListItem { Selected = true, Text = "Gabon", Value ="GABON"},
                    new SelectListItem { Selected = true, Text = "The Gambia", Value ="THE GAMBIA"},
                    new SelectListItem { Selected = true, Text = "Georgia", Value ="GEORGIA"},
                    new SelectListItem { Selected = true, Text = "Germany", Value ="GERMANY"},
                    new SelectListItem { Selected = true, Text = "Ghana", Value ="GHANA"},
                    new SelectListItem { Selected = true, Text = "Greece", Value ="GREECE"},
                    new SelectListItem { Selected = true, Text = "Grenada", Value ="GRENADA"},
                    new SelectListItem { Selected = true, Text = "Guatemala", Value ="GUATEMALA"},
                    new SelectListItem { Selected = true, Text = "Guinea", Value ="GUINEA"},
                    new SelectListItem { Selected = true, Text = "Guinea-Bissau", Value ="GUINEA-BISSAU"},
                    new SelectListItem { Selected = true, Text = "Guyana", Value ="GUYANA"},
                    new SelectListItem { Selected = true, Text = "Haiti", Value ="HAITI"},
                    new SelectListItem { Selected = true, Text = "Honduras", Value ="HONDURAS"},
                    new SelectListItem { Selected = true, Text = "Hungary", Value ="HUNGARY"},
                    new SelectListItem { Selected = true, Text = "Iceland", Value ="ICELAND"},
                    new SelectListItem { Selected = true, Text = "India", Value ="INDIA"},
                    new SelectListItem { Selected = true, Text = "Indonesia", Value ="INDONESIA"},
                    new SelectListItem { Selected = true, Text = "Iran", Value ="IRAN"},
                    new SelectListItem { Selected = true, Text = "Iraq", Value ="IRAQ"},
                    new SelectListItem { Selected = true, Text = "Ireland", Value ="IRELAND"},
                    new SelectListItem { Selected = true, Text = "Israel", Value ="ISRAEL"},
                    new SelectListItem { Selected = true, Text = "Italy", Value ="ITALY"},
                    new SelectListItem { Selected = true, Text = "Jamaica", Value ="JAMAICA"},
                    new SelectListItem { Selected = true, Text = "Japan", Value ="JAPAN"},
                    new SelectListItem { Selected = true, Text = "Jordan", Value ="JORDAN"},
                    new SelectListItem { Selected = true, Text = "Kazakhstan", Value ="KAZAKHSTAN"},
                    new SelectListItem { Selected = true, Text = "Kenya", Value ="KENYA"},
                    new SelectListItem { Selected = true, Text = "Kiribati", Value ="KIRIBATI"},
                    new SelectListItem { Selected = true, Text = "Korea, North", Value ="KOREA, NORTH"},
                    new SelectListItem { Selected = true, Text = "Korea, South", Value ="KOREA, SOUTH"},
                    new SelectListItem { Selected = true, Text = "Kosovo", Value ="KOSOVO"},
                    new SelectListItem { Selected = true, Text = "Kuwait", Value ="KUWAIT"},
                    new SelectListItem { Selected = true, Text = "Kyrgyzstan", Value ="KYRGYZSTAN"},
                    new SelectListItem { Selected = true, Text = "Laos", Value ="LAOS"},
                    new SelectListItem { Selected = true, Text = "Latvia", Value ="LATVIA"},
                    new SelectListItem { Selected = true, Text = "Lebanon", Value ="LEBANON"},
                    new SelectListItem { Selected = true, Text = "Lesotho", Value ="LESOTHO"},
                    new SelectListItem { Selected = true, Text = "Liberia", Value ="LIBERIA"},
                    new SelectListItem { Selected = true, Text = "Libya", Value ="LIBYA"},
                    new SelectListItem { Selected = true, Text = "Liechtenstein", Value ="LIECHTENSTEIN"},
                    new SelectListItem { Selected = true, Text = "Lithuania", Value ="LITHUANIA"},
                    new SelectListItem { Selected = true, Text = "Luxembourg", Value ="LUXEMBOURG"},
                    new SelectListItem { Selected = true, Text = "Macedonia", Value ="MACEDONIA"},
                    new SelectListItem { Selected = true, Text = "Madagascar", Value ="MADAGASCAR"},
                    new SelectListItem { Selected = true, Text = "Malawi", Value ="MALAWI"},
                    new SelectListItem { Selected = true, Text = "Malaysia", Value ="MALAYSIA"},
                    new SelectListItem { Selected = true, Text = "Maldives", Value ="MALDIVES"},
                    new SelectListItem { Selected = true, Text = "Mali", Value ="MALI"},
                    new SelectListItem { Selected = true, Text = "Malta", Value ="MALTA"},
                    new SelectListItem { Selected = true, Text = "Marshall Islands", Value ="MARSHALL ISLANDS"},
                    new SelectListItem { Selected = true, Text = "Mauritania", Value ="MAURITANIA"},
                    new SelectListItem { Selected = true, Text = "Mauritius", Value ="MAURITIUS"},
                    new SelectListItem { Selected = true, Text = "Mexico", Value ="MEXICO"},
                    new SelectListItem { Selected = true, Text = "Micronesia, Federated States of", Value ="MICRONESIA, FEDERATED STATES OF"},
                    new SelectListItem { Selected = true, Text = "Moldova", Value ="MOLDOVA"},
                    new SelectListItem { Selected = true, Text = "Monaco", Value ="MONACO"},
                    new SelectListItem { Selected = true, Text = "Mongolia", Value ="MONGOLIA"},
                    new SelectListItem { Selected = true, Text = "Montenegro", Value ="MONTENEGRO"},
                    new SelectListItem { Selected = true, Text = "Morocco", Value ="MOROCCO"},
                    new SelectListItem { Selected = true, Text = "Mozambique", Value ="MOZAMBIQUE"},
                    new SelectListItem { Selected = true, Text = "Myanmar (Burma)", Value ="MYANMAR (BURMA)"},
                    new SelectListItem { Selected = true, Text = "Namibia", Value ="NAMIBIA"},
                    new SelectListItem { Selected = true, Text = "Nauru", Value ="NAURU"},
                    new SelectListItem { Selected = true, Text = "Nepal", Value ="NEPAL"},
                    new SelectListItem { Selected = true, Text = "Netherlands", Value ="NETHERLANDS"},
                    new SelectListItem { Selected = true, Text = "New Zealand", Value ="NEW ZEALAND"},
                    new SelectListItem { Selected = true, Text = "Nicaragua", Value ="NICARAGUA"},
                    new SelectListItem { Selected = true, Text = "Niger", Value ="NIGER"},
                    new SelectListItem { Selected = true, Text = "Nigeria", Value ="NIGERIA"},
                    new SelectListItem { Selected = true, Text = "Norway", Value ="NORWAY"},
                    new SelectListItem { Selected = true, Text = "Oman", Value ="OMAN"},
                    new SelectListItem { Selected = true, Text = "Pakistan", Value ="PAKISTAN"},
                    new SelectListItem { Selected = true, Text = "Palau", Value ="PALAU"},
                    new SelectListItem { Selected = true, Text = "Panama", Value ="PANAMA"},
                    new SelectListItem { Selected = true, Text = "Papua New Guinea", Value ="PAPUA NEW GUINEA"},
                    new SelectListItem { Selected = true, Text = "Paraguay", Value ="PARAGUAY"},
                    new SelectListItem { Selected = true, Text = "Peru", Value ="PERU"},
                    new SelectListItem { Selected = true, Text = "Philippines", Value ="PHILIPPINES"},
                    new SelectListItem { Selected = true, Text = "Poland", Value ="POLAND"},
                    new SelectListItem { Selected = true, Text = "Portugal", Value ="PORTUGAL"},
                    new SelectListItem { Selected = true, Text = "Qatar", Value ="QATAR"},
                    new SelectListItem { Selected = true, Text = "Romania", Value ="ROMANIA"},
                    new SelectListItem { Selected = true, Text = "Russia", Value ="RUSSIA"},
                    new SelectListItem { Selected = true, Text = "Rwanda", Value ="RWANDA"},
                    new SelectListItem { Selected = true, Text = "Saint Kitts and Nevis", Value ="SAINT KITTS AND NEVIS"},
                    new SelectListItem { Selected = true, Text = "Saint Lucia", Value ="SAINT LUCIA"},
                    new SelectListItem { Selected = true, Text = "Saint Vincent and the Grenadines", Value ="SAINT VINCENT AND THE GRENADINES"},
                    new SelectListItem { Selected = true, Text = "Samoa", Value ="SAMOA"},
                    new SelectListItem { Selected = true, Text = "San Marino", Value ="SAN MARINO"},
                    new SelectListItem { Selected = true, Text = "Sao Tome and Principe", Value ="SAO TOME AND PRINCIPE"},
                    new SelectListItem { Selected = true, Text = "Saudi Arabia", Value ="SAUDI ARABIA"},
                    new SelectListItem { Selected = true, Text = "Senegal", Value ="SENEGAL"},
                    new SelectListItem { Selected = true, Text = "Serbia", Value ="SERBIA"},
                    new SelectListItem { Selected = true, Text = "Seychelles", Value ="SEYCHELLES"},
                    new SelectListItem { Selected = true, Text = "Sierra Leone", Value ="SIERRA LEONE"},
                    new SelectListItem { Selected = true, Text = "Singapore", Value ="SINGAPORE"},
                    new SelectListItem { Selected = true, Text = "Slovakia", Value ="SLOVAKIA"},
                    new SelectListItem { Selected = true, Text = "Slovenia", Value ="SLOVENIA"},
                    new SelectListItem { Selected = true, Text = "Solomon Islands", Value ="SOLOMON ISLANDS"},
                    new SelectListItem { Selected = true, Text = "Somalia", Value ="SOMALIA"},
                    new SelectListItem { Selected = true, Text = "South Africa", Value ="SOUTH AFRICA"},
                    new SelectListItem { Selected = true, Text = "South Sudan", Value ="SOUTH SUDAN"},
                    new SelectListItem { Selected = true, Text = "Spain", Value ="SPAIN"},
                    new SelectListItem { Selected = true, Text = "Sri Lanka", Value ="SRI LANKA"},
                    new SelectListItem { Selected = true, Text = "Sudan", Value ="SUDAN"},
                    new SelectListItem { Selected = true, Text = "Suriname", Value ="SURINAME"},
                    new SelectListItem { Selected = true, Text = "Swaziland", Value ="SWAZILAND"},
                    new SelectListItem { Selected = true, Text = "Sweden", Value ="SWEDEN"},
                    new SelectListItem { Selected = true, Text = "Switzerland", Value ="SWITZERLAND"},
                    new SelectListItem { Selected = true, Text = "Syria", Value ="SYRIA"},
                    new SelectListItem { Selected = true, Text = "Taiwan", Value ="TAIWAN"},
                    new SelectListItem { Selected = true, Text = "Tajikistan", Value ="TAJIKISTAN"},
                    new SelectListItem { Selected = true, Text = "Tanzania", Value ="TANZANIA"},
                    new SelectListItem { Selected = true, Text = "Thailand", Value ="THAILAND"},
                    new SelectListItem { Selected = true, Text = "Togo", Value ="TOGO"},
                    new SelectListItem { Selected = true, Text = "Tonga", Value ="TONGA"},
                    new SelectListItem { Selected = true, Text = "Trinidad and Tobago", Value ="TRINIDAD AND TOBAGO"},
                    new SelectListItem { Selected = true, Text = "Tunisia", Value ="TUNISIA"},
                    new SelectListItem { Selected = true, Text = "Turkey", Value ="TURKEY"},
                    new SelectListItem { Selected = true, Text = "Turkmenistan", Value ="TURKMENISTAN"},
                    new SelectListItem { Selected = true, Text = "Tuvalu", Value ="TUVALU"},
                    new SelectListItem { Selected = true, Text = "Uganda", Value ="UGANDA"},
                    new SelectListItem { Selected = true, Text = "Ukraine", Value ="UKRAINE"},
                    new SelectListItem { Selected = true, Text = "United Arab Emirates", Value ="UNITED ARAB EMIRATES"},
                    new SelectListItem { Selected = true, Text = "United Kingdom", Value ="UNITED KINGDOM"},
                    new SelectListItem { Selected = true, Text = "United States of America", Value ="UNITED STATES OF AMERICA"},
                    new SelectListItem { Selected = true, Text = "Uruguay", Value ="URUGUAY"},
                    new SelectListItem { Selected = true, Text = "Uzbekistan", Value ="UZBEKISTAN"},
                    new SelectListItem { Selected = true, Text = "Vanuatu", Value ="VANUATU"},
                    new SelectListItem { Selected = true, Text = "Vatican City (Holy See)", Value ="VATICAN CITY (HOLY SEE)"},
                    new SelectListItem { Selected = true, Text = "Venezuela", Value ="VENEZUELA"},
                    new SelectListItem { Selected = true, Text = "Vietnam", Value ="VIETNAM"},
                    new SelectListItem { Selected = true, Text = "Yemen", Value ="YEMEN"},
                    new SelectListItem { Selected = true, Text = "Zambia", Value ="ZAMBIA"},
                    new SelectListItem { Selected = true, Text = "Zimbabwe", Value ="ZIMBABWE"},
                      }, "Value", "Text", 1);
            return DataList;
        }
    }
}