using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class TenderService
    {
        public static SelectList DropDownList_Sel_Buy()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="Buy", Value = "0" },new SelectListItem{ Text="Sales", Value = "1" }}, "Value", "Text", 1);
            return DataList;
        }
        public static List<SelectListItem> getAnyRate()
        {
            var objList = new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "Lower Rate Only", Value ="0"},
                new SelectListItem { Selected = false, Text = "Any Rate", Value = "1"},
              };
            return objList;
        }
        public static List<SelectListItem> getReBidding()
        {
            var objList = new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "Submit Once", Value ="0"},
                new SelectListItem { Selected = false, Text = "Submit Multiple", Value = "1"},
            };
            return objList;
        }

        public static SelectList DropDownList_item()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="CORN FLOWER", Value = "1" },
                   new SelectListItem{ Text="WHEAT", Value = "2" },
                   new SelectListItem{ Text= "PIG IRON-INDIA", Value = "3" },
                   new SelectListItem{ Text="REFINED PALM OIL", Value = "4" }
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_partialshipment()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="Allow", Value = "1" },
                   new SelectListItem{ Text="Not Allow", Value = "0" }
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_shipmentMode()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="Sea", Value = "1" },
                   new SelectListItem{ Text="Road", Value = "2" },
                   new SelectListItem{ Text="Air", Value = "3" }
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_Port()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="abc", Value = "1" },
                   new SelectListItem{ Text="asdfasdf", Value = "2" },
                   new SelectListItem{ Text="asdfasd", Value = "3" }
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_Incoterms()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="cfr", Value = "1" },
                   new SelectListItem{ Text="cob", Value = "2" },
                   new SelectListItem{ Text="cpt", Value = "3" }
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_CostType()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="Include", Value = "1" },
                   new SelectListItem{ Text="Exclude", Value = "2" }
                  }, "Value", "Text", 1);
            return DataList;
        }

        public static SelectList DropDown_currencyList()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {
                      new SelectListItem{ Text="United States dollar(USD)-$", Value = "1" }
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_payment()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {
                      new SelectListItem{ Text="Cash/TT", Value = "1" },
                      new SelectListItem{ Text="Credit", Value = "1" },
                      new SelectListItem{ Text="Latter Of Credit", Value = "1" },
                      new SelectListItem{ Text="CAD", Value = "1" },
                  }, "Value", "Text", 1);
            return DataList;
        }
    }
}