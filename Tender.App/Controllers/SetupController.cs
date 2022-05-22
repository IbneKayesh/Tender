using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tender.App.Service;
using Tender.Models.Models;

namespace Tender.App.Controllers
{
    public class SetupController : Controller
    {
        #region Category
        // GET: Setup
        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult CreateCategory()
        {
            return View();
        }
        public ActionResult CreateCategory(string obj)
        {

            return View();
        }
        #endregion

        #region tenderProduct       
        [HttpGet]
        public ActionResult CreateTenderProduct()
        {
            TNDR_PRODUCTS obj = new TNDR_PRODUCTS();
            return View(obj);
        }
        [HttpPost]
        public ActionResult CreateTenderProduct(TNDR_PRODUCTS obj)
        {
            if (ModelState.IsValid)
            {
                if (SetupService.checkItem(obj.PRODUCTS_NAME).Item1.PRODUCTS_NAME != null)
                {
                    ModelState.AddModelError("", "This Product Name Already Exist");
                    return View(obj);
                }

                if (obj.ProductPicture != null)
                {
                    if (obj.ProductPicture.ContentLength <= 102400)
                    {
                        string pic = System.IO.Path.GetFileName(obj.ProductPicture.FileName);
                        var extension = Path.GetExtension(obj.ProductPicture.FileName);
                        string path = System.IO.Path.Combine(
                                               Server.MapPath("~/App_Asset/dist/img/productImage"), pic);
                        System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(obj.ProductPicture.InputStream);

                        if (UploadedImage.PhysicalDimension.Width == 800 && UploadedImage.PhysicalDimension.Height == 800)
                        {
                            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == "jpeg")
                            {
                                obj.ProductPicture.SaveAs(path);
                                obj.IMAGE_PATH = "App_Asset/dist/img/productImage/" + pic;
                                var _tpl = SetupService.saveItem(obj);
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    obj.ProductPicture.InputStream.CopyTo(ms);
                                    byte[] array = ms.GetBuffer();
                                }
                                if (_tpl.SUCCESS == true)
                                {
                                    TempData["msg"] = AlertService.SaveSuccess();
                                }
                            }
                            else
                            {
                                TempData["msg"] = AlertService.SaveWarningOK("Image format must be jpg, jpeg,png");
                                return View(obj);
                            }
                        }
                        else
                        {
                            TempData["msg"] = AlertService.SaveWarningOK("Image Size must be 800 X 800");
                            return View(obj);
                        }


                    }
                    else
                    {
                        TempData["msg"] = AlertService.SaveWarningOK("Image Size must be less then 100 kb");
                        return View(obj);
                    }
                }
                else
                {
                    var _tpl = SetupService.saveItem(obj);
                    if (_tpl.SUCCESS == true)
                    {
                        TempData["msg"] = AlertService.SaveSuccess();
                    }
                }
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
                ModelState.AddModelError("Err", "Invalid Data");
            }
            return View(obj);
        }


        public ActionResult GetAll()
        {
            List<TNDR_PRODUCTS> obj = SetupService.getAllproduct().Item1;
            if (obj.Count() > 0)
            {
                return Json(new { data = obj }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = new List<TNDR_PRODUCTS>() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}