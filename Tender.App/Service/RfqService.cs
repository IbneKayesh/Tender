using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tender.Models.Models;


namespace Tender.App.Service
{
    public class RfqService
    {
        public static Tuple<List<RFQ_TenderView>, EQResult> getAllRfqList(string companyId)
        {
            string sql = $@"SELECT RT.RFQ_NUMBER ,RT.START_DATE,RT.END_DATE, CASE WHEN  RT.SELL_BUY=0 THEN 'Buyer' ELSE 'Seller ' END 
                SELL_BUY,TP.PRODUCTS_NAME ||'('||TP.UNIT ||')' PRODUCTS_NAME,RT.PRODUCTS_QUANTITY,RT.CURRENCY_NAME,RT.IS_ACTIVE,
                NVL(BIDDERS.SUBMITED_BIDS,0) SUBMITED_BIDS,RT.IS_APPROVE,NVL(RTA.APPROVAL_ID,'NO_CONFIRM') APPROVAL_STATUS
                FROM RFQ_TENDER RT
                INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=RT.PRODUCTS_ID
                LEFT JOIN RFQ_TENDER_APPROVAL RTA ON RTA.RFQ_NUMBER=RT.RFQ_NUMBER
                LEFT JOIN ( SELECT RFQ_NUMBER,MAX(RFQ_SL)SUBMITED_BIDS  FROM RFQ_BIDDING GROUP BY RFQ_NUMBER ) BIDDERS ON BIDDERS.RFQ_NUMBER=RT.RFQ_NUMBER
                WHERE RT.COMPANY_ID='{companyId}' AND NVL(RTA.APPROVAL_ID,'NO_CONFIRM')='NO_CONFIRM'
                AND TO_DATE(RT.END_DATE,'DD/MM/YYYY') >=TO_DATE(SYSDATE, 'DD/MM/YYYY')                
                ORDER BY RT.RFQ_NUMBER DESC";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TenderView>(sql);
            return objList;
        }

        public static EQResult ApproveTender(int appStatus, string rfqNumber)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($@"UPDATE RFQ_TENDER SET IS_APPROVE={appStatus} WHERE RFQ_NUMBER='{rfqNumber}' AND IS_ACTIVE=1");
            return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

    }
}