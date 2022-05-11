using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class QuotationService
    {
        public static Tuple<List<VENDOR>, EQResult> GetVendor(string vendorId)
        {
            string sql = $"SELECT V.VENDOR_ID,ORGANIZATION_NAME FROM VENDOR V WHERE V.VENDOR_ID='{vendorId}'";
            return DatabaseMSSql.SqlQuery<VENDOR>(sql);
        }

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

    }
}