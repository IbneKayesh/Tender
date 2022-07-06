using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class TenderService
    {
        public static EQResult SaveData(RFQ_TENDER _obj)
        {
            var tenderNumber = CommonService.getTenderNumber("RFQ_TENDER",_obj.COMPANY_ID);
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
                       PAY_AP, PAY_B, PAY_BP,COMPANY_ID) 
                       VALUES ('{tenderNumber}' ,'{_obj.VENDOR_ID}' ,{_obj.SELL_BUY} ,{_obj.LOCAL_IMPORT},{_obj.RE_BID} ,{_obj.LOWER_RATE} ,
                                TO_DATE('{_obj.START_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR') ,TO_DATE('{_obj.END_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR')
                                ,'{_obj.PRODUCTS_ID}' ,'{_obj.PRODUCTS_DESC}' ,{_obj.PRODUCTS_RATE} ,{_obj.PRODUCTS_QUANTITY} ,
                                TO_DATE('{_obj.LAST_DELIVERY_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR') ,{_obj.PARTIAL_SHIPMENT} ,
                                {_obj.SHIPMENT_MODE},{_obj.PORT_ID} ,'{_obj.DELIVERY_ADDRESS}' ,'{_obj.RECEIVER_NAME}' ,'{_obj.RECEIVER_DETAILS}' ,
                                {_obj.COST_EX_INC} ,'{_obj.INCO_TERMS}'
                                ,'{_obj.CURRENCY_NAME}' ,{_obj.CURRENCY_RATE} ,'{_obj.PAY_A}' ,{_obj.PAY_AP} ,'{_obj.PAY_B}' ,{_obj.PAY_BP},{_obj.COMPANY_ID} )");
            if (_obj.RFQ_TNDR_DOCUMENTS !=null)
            {
                foreach (var item in _obj.RFQ_TNDR_DOCUMENTS)
                {
                    sqlList.Add($@"INSERT INTO TND.RFQ_TNDR_DOCUMENTS ( RFQ_NUMBER, DOCUMENTS_ID) VALUES ( '{tenderNumber}','{item.DOCUMENTS_ID}')");
                }
            }
            sqlList.Add($@"UPDATE TABLE_MAX_ID SET MAX_ID=MAX_ID+1 WHERE TABLE_NAME='RFQ_TENDER' AND COMPANY_ID='{_obj.COMPANY_ID}'");

            return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

        public static SelectList DropDownList_Sel_Buy()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {new SelectListItem{ Text="Buy", Value = "1" },new SelectListItem{ Text="Sales", Value = "2" }}, "Value", "Text", 1);
            return DataList;
        }
        public static List<SelectListItem> getAnyRate()
        {
            var objList = new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "Lower Rate Only", Value ="1"},
                new SelectListItem { Selected = false, Text = "Any Rate", Value = "2"},
              };
            return objList;
        }
        public static List<SelectListItem> getReBidding()
        {
            var objList = new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "Submit Once", Value ="1"},
                new SelectListItem { Selected = false, Text = "Submit Multiple", Value = "2"},
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
                  {
                      new SelectListItem{ Text="Not Allow", Value = "1" },
                      new SelectListItem{ Text="Allow", Value = "2" },
                  }, "Value", "Text", 1);
            return DataList;
        }
        public static SelectList DropDown_shipmentMode()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {
                   new SelectListItem{ Text="Sea", Value = "1" },
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

                  {
                   new SelectListItem{ Text="Include", Value = "1" },
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
                  }, "Value", "Text", null);
            return DataList;
        }

        public static Tuple<List<TNDR_PRODUCTS>, EQResult> GetVendorWiseItem(string vendorId)
        {
            string sql = $@"SELECT   TP.PRODUCTS_ID,
         TP.PRODUCTS_NAME || '- (' || TP.UNIT || ')' PRODUCTS_NAME
  FROM      TNDR_PRODUCTS TP
         INNER JOIN
            VENDOR_PRODUCTS_GROUP VP
         ON TP.GROUP_ID = VP.PRODUCTS_GROUP_ID
 WHERE   VP.IS_ACTIVE = 1
         AND VP.VENDOR_ID = '{vendorId}'";
            return DatabaseMSSql.SqlQuery<TNDR_PRODUCTS>(sql);
        }
        public static Tuple<List<TNDR_SHIPMENT_MODE>, EQResult> getShipmentMode()
        {
            string sql = $" SELECT * FROM TNDR_SHIPMENT_MODE";
            return DatabaseMSSql.SqlQuery<TNDR_SHIPMENT_MODE>(sql);
        }
        public static Tuple<List<TNDR_PAYMENT_MODE>, EQResult> getPaymentModeByID(string _id)
        {
            string sql = $"SELECT * FROM TNDR_PAYMENT_MODE WHERE PAYMENT_MODE_ID <> '{_id}'";
            return DatabaseMSSql.SqlQuery<TNDR_PAYMENT_MODE>(sql);
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
          //  string sql = $" SELECT * FROM TNDR_DOCUMENTS WHERE DOCUMENTS_TYPE ='TENDER'";
            string sql = $"SELECT * FROM TNDR_DOCUMENTS";
            return DatabaseMSSql.SqlQuery<TNDR_DOCUMENTS>(sql);
        }
        public static Tuple<List<TNDR_DOCUMENTS>, EQResult> getVendorUploadDocument(string _vendorID)
        {
            //  string sql = $" SELECT * FROM TNDR_DOCUMENTS WHERE DOCUMENTS_TYPE ='TENDER'";
            string sql = $@"SELECT * FROM TNDR_DOCUMENTS WHERE DOCUMENTS_ID NOT IN ( SELECT TD.DOCUMENTS_ID FROM TNDR_DOCUMENTS TD 
                            INNER JOIN VENDOR_FILE VF ON VF.DOC_ID = TD.DOCUMENTS_ID AND VF.DOCUMENT_TYPE = 'Documents'
                            WHERE VF.IS_ACTIVE = 1 AND VF.VENDOR_ID = '{_vendorID}')";
            return DatabaseMSSql.SqlQuery<TNDR_DOCUMENTS>(sql);
        }
        public static Tuple<List<TNDR_CERTIFICATE>, EQResult> getvendorUploadCertificate(string _vendorID)
        {
            string sql = $@"SELECT * FROM TNDR_CERTIFICATE WHERE CERTIFICATE_ID NOT IN (SELECT TD.CERTIFICATE_ID FROM TNDR_CERTIFICATE TD 
                            INNER JOIN VENDOR_FILE VF ON VF.DOC_ID = TD.CERTIFICATE_ID AND VF.DOCUMENT_TYPE = 'Certificate'
                            WHERE VF.IS_ACTIVE = 1 AND VF.VENDOR_ID = '{_vendorID}')";
            return DatabaseMSSql.SqlQuery<TNDR_CERTIFICATE>(sql);
        }

        public static Tuple<List<TNDR_CERTIFICATE>, EQResult> getvendorDoc()
        {
            string sql = $" SELECT * FROM TNDR_CERTIFICATE";
            return DatabaseMSSql.SqlQuery<TNDR_CERTIFICATE>(sql);
        }
        public static SelectList DropDown_SearchType()
        {
            SelectList DataList = new SelectList(
                  new List<SelectListItem>
                  {
                      new SelectListItem{ Text="View All Tender", Value = "1" },
                      new SelectListItem{ Text="Approval Tender", Value = "2" },
                      new SelectListItem{ Text="Not Approval Tender", Value = "3" },
                      new SelectListItem{ Text="No Qutotation Apply but Expeire", Value = "4" }
                  }, "Value", "Text", null);
            return DataList;
        }
        public static Tuple<List<VENDOR_DETAILS>, EQResult> companyProductCatWiseSupllierList(string companyId,string productId)
        {
            string sql = $@"SELECT DISTINCT V.* FROM VENDOR V
                            INNER JOIN VENDOR_PRODUCTS_GROUP VPG ON VPG.VENDOR_ID=V.VENDOR_ID
                            INNER JOIN VENDOR_COMPANY VC ON VC.VENDOR_ID=V.VENDOR_ID
                            INNER JOIN COMPANY C ON C.COMPANY_ID=VC.COMPANY_ID
                            INNER JOIN TNDR_PRODUCT_GROUP TPG ON TPG.ID=VPG.PRODUCTS_GROUP_ID
                            INNER JOIN TNDR_PRODUCTS TP ON TP.GROUP_ID=TPG.ID
                            INNER JOIN RFQ_TENDER RT ON RT.PRODUCTS_ID=TP.PRODUCTS_ID AND RT.COMPANY_ID=C.COMPANY_ID
                            WHERE -- RT.RFQ_NUMBER='RFQ-220600017'
                            TP.PRODUCTS_ID=NVL('{productId}',TP.PRODUCTS_ID) 
                            AND C.COMPANY_ID=NVL('{companyId}',C.COMPANY_ID)
                            AND V.SUPPLIER=1 AND V.IS_ACTIVE=1";
            return DatabaseMSSql.SqlQuery<VENDOR_DETAILS>(sql);
        }
    }
}