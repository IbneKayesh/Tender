using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Tender.App.Controllers
{
    public class RemoteValidationController : Controller
    {
        // GET: RemoteValidationController
        public JsonResult PasswordComplexity(string VENDOR_PASSWD)
        {
            var regex = new Regex(@"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[\W_]).{6}");
            var r = regex.Match(VENDOR_PASSWD);
            if (r.Success)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}