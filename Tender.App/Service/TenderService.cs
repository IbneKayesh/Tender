using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class TenderService
    {
        public static EQResult SaveData(RFQ_TENDER _obj)
        {
            var tenderNumber = CommonService.getTenderNumber("RFQ_TENDER");
            List<string> sqlList = new List<string>();

            sqlList.Add($@"INSERT INTO RFQ_TENDER (
                       RFQ_NUMBER, VENDOR_ID, SELL_BUY, 
                       LOCAL_IMPORT, RE_BID, LOWER_RATE, 
                       START_DATE, END_DATE, PRODUCTS_ID, 
                       PRODUCTS_DESC, PRODUCTS_RATE, PRODUCTS_QUANTITY, 
                       LAST_DELIVERY_DATE, PARTIAL_SHIPMENT, SHIPMENT_MODE, 
                       PORT_ID, DELIVERY_ADDRESS, RECEIVER_NAME, 
                       RECEIVER_DETAILS, COST_EX_INC, INCO_TERMS, 
                       CURRENCY_NAME, CURRENCY_RATE, PAY_A, 
                       PAY_AP, PAY_B, PAY_BP) 
                       VALUES ('{tenderNumber}' ,'{_obj.VENDOR_ID}' ,{_obj.SELL_BUY} ,{_obj.LOCAL_IMPORT},{_obj.RE_BID} ,{_obj.LOWER_RATE} ,
                                TO_DATE('{_obj.START_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR') ,TO_DATE('{_obj.END_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR')
                                ,'{_obj.PRODUCTS_ID}' ,'{_obj.PRODUCTS_DESC}' ,{_obj.PRODUCTS_RATE} ,{_obj.PRODUCTS_QUANTITY} ,
                                TO_DATE('{_obj.LAST_DELIVERY_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR') ,{_obj.PARTIAL_SHIPMENT} ,
                                {_obj.SHIPMENT_MODE},{_obj.PORT_ID} ,'{_obj.DELIVERY_ADDRESS}' ,'{_obj.RECEIVER_NAME}' ,'{_obj.RECEIVER_DETAILS}' ,
                                {_obj.COST_EX_INC} ,'{_obj.INCO_TERMS}'
                                ,'{_obj.CURRENCY_NAME}' ,{_obj.CURRENCY_RATE} ,'{_obj.PAY_A}' ,{_obj.PAY_AP} ,'{_obj.PAY_B}' ,{_obj.PAY_BP} )");
            sqlList.Add($@"UPDATE TABLE_MAX_ID SET MAX_ID=MAX_ID+1 WHERE TABLE_NAME='RFQ_TENDER'");
           
            return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

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
                      new SelectListItem{ Text="Credit", Value = "2" },
                      new SelectListItem{ Text="Latter Of Credit", Value = "3" },
                      new SelectListItem{ Text="CAD", Value = "4" },
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_importer()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {
                      new SelectListItem{ Text="Local", Value = "1" },
                      new SelectListItem{ Text="Importer", Value = "2" }
                  }, "Value", "Text", 1);
            return DataList;
        }

        public static Tuple<List<TNDR_PRODUCTS>, EQResult> GetVendorWiseItem(string vendorId)
        {
            string sql = $"SELECT TP.PRODUCTS_ID,TP.PRODUCTS_NAME ||'- ('||TP.UNIT||')' PRODUCTS_NAME FROM TNDR_PRODUCTS TP INNER JOIN VENDOR_PRODUCTS VP ON TP.PRODUCTS_ID=VP.PRODUCTS_ID WHERE VP.IS_ACTIVE=1 AND  VP.VENDOR_ID='{vendorId}'";
            return DatabaseMSSql.SqlQuery<TNDR_PRODUCTS>(sql);
        }
        public static Tuple<List<TNDR_SHIPMENT_MODE>, EQResult> getShipmentMode()
        {
            string sql = $" SELECT * FROM TNDR_SHIPMENT_MODE";
            return DatabaseMSSql.SqlQuery<TNDR_SHIPMENT_MODE>(sql);
        }
        public static Tuple<List<TNDR_PORT>, EQResult> getPort()
        {
            string sql = $"SELECT * FROM TNDR_PORT";
            return DatabaseMSSql.SqlQuery<TNDR_PORT>(sql);
        }
        public static Tuple<List<TNDR_INCO_TERMS>, EQResult> getIncoterms()
        {
            string sql = $" SELECT * FROM TNDR_INCO_TERMS";
            return DatabaseMSSql.SqlQuery<TNDR_INCO_TERMS>(sql);
        }
        public static Tuple<List<TNDR_PAYMENT_MODE>, EQResult> getPaymentMode()
        {
            string sql = $" SELECT * FROM TNDR_PAYMENT_MODE";
            return DatabaseMSSql.SqlQuery<TNDR_PAYMENT_MODE>(sql);
        }
        public static Tuple<List<TNDR_CURRENCY>, EQResult> getCurrency()
        {
            string sql = $" SELECT * FROM TNDR_CURRENCY";
            return DatabaseMSSql.SqlQuery<TNDR_CURRENCY>(sql);
        }
        public static Tuple<List<TNDR_DOCUMENTS>, EQResult> getTnderDoc()
        {
            string sql = $" SELECT * FROM TNDR_DOCUMENTS WHERE DOCUMENTS_TYPE ='TENDER'";
            return DatabaseMSSql.SqlQuery<TNDR_DOCUMENTS>(sql);
        }
    }
}