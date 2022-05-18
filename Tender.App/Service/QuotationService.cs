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
        public static Tuple<List<RFQ_TenderView>, EQResult> getAllTender(string vendorId)
        {
            string sql = $@" SELECT R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID,
                            CASE WHEN  R.SELL_BUY=0 THEN 'Buyer' ELSE 'Seller ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                               R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME , TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE , R.PRODUCTS_QUANTITY , 
                               R.LAST_DELIVERY_DATE,
                               CASE WHEN  R.PARTIAL_SHIPMENT=2 THEN 'Allow' WHEN  R.PARTIAL_SHIPMENT=1 THEN 'Not Allow' ELSE '' END PARTIAL_SHIPMENT, SM.SHIPMENT_MODE_NAME TENDER_SHIPMENT_MODE, 
                               P.PORT_NAME TENDER_PORT_ID, R.DELIVERY_ADDRESS, R.RECEIVER_NAME, 
                               R.RECEIVER_DETAILS, CASE WHEN  R.COST_EX_INC=1 THEN 'Include' ELSE 'Exclude' END COST_EX_INC, INCO.INCO_TERMS_NAME, 
                               R.CURRENCY_NAME, R.CURRENCY_RATE, TPM.PAYMENT_MODE_NAME PAY_A, 
                               R.PAY_AP, TPM.PAYMENT_MODE_NAME PAY_B, R.PAY_BP, 
                               R.IS_APPROVE,R.PRODUCTS_ID,NVL(BIDDERS.SUBMITED_BIDS,0) SUBMITED_BIDS ,NVL(APP.APPROVAL_ID,0)  APPROVAL_ID,NVL2(T1.RFQ_NUMBER,'Y','N')VSUBMIT_STATUS
                            FROM TND.RFQ_TENDER R
                            INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=R.PRODUCTS_ID
                            LEFT JOIN TNDR_SHIPMENT_MODE SM ON SM.SHIPMENT_MODE_ID=R.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT P ON P.PORT_ID=R.PORT_ID
                            LEFT JOIN TNDR_INCO_TERMS INCO ON INCO.INCO_TERMS_ID=R.INCO_TERMS
                            INNER JOIN TNDR_PAYMENT_MODE TPM ON TPM.PAYMENT_MODE_ID=R.PAY_A
                            INNER JOIN TNDR_PAYMENT_MODE TPB ON TPB.PAYMENT_MODE_ID=R.PAY_B
                            LEFT JOIN ( SELECT RFQ_NUMBER,MAX(RFQ_SL)SUBMITED_BIDS  FROM RFQ_BIDDING GROUP BY RFQ_NUMBER ) BIDDERS ON BIDDERS.RFQ_NUMBER=R.RFQ_NUMBER
                            LEFT JOIN RFQ_TENDER_APPROVAL APP ON APP.RFQ_NUMBER=R.RFQ_NUMBER
                             LEFT JOIN (
                            SELECT DISTINCT RFQ_NUMBER, VENDOR_ID FROM RFQ_BIDDING WHERE VENDOR_ID='{vendorId}' 
                            ) T1 ON T1.RFQ_NUMBER=R.RFQ_NUMBER                 
                            ORDER BY R.RFQ_NUMBER ASC
                            ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TenderView>(sql);
            return objList;
        }
        public static Tuple<RFQ_BIDDING, EQResult> getTenderById(string rfqNumber)
        {
            string sql = $@"SELECT R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID,
                            CASE WHEN  R.SELL_BUY=0 THEN 'Buyer' ELSE 'Seller ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                               R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME TND_PRODUCTS_NAME, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
                               R.LAST_DELIVERY_DATE,
                               CASE WHEN  R.PARTIAL_SHIPMENT=2 THEN 'Allow' WHEN  R.PARTIAL_SHIPMENT=1 THEN 'Not Allow' ELSE '' END PARTIAL_SHIPMENT, SM.SHIPMENT_MODE_NAME TENDER_SHIPMENT_MODE, 
                               P.PORT_NAME TENDER_PORT_ID, R.DELIVERY_ADDRESS, R.RECEIVER_NAME, 
                               R.RECEIVER_DETAILS, CASE WHEN  R.COST_EX_INC=1 THEN 'Include' ELSE 'Exclude' END COST_EX_INC, INCO.INCO_TERMS_NAME, 
                               R.CURRENCY_NAME, R.CURRENCY_RATE, TPM.PAYMENT_MODE_NAME PAY_A, 
                               R.PAY_AP, TPM.PAYMENT_MODE_NAME PAY_B, R.PAY_BP, 
                               R.IS_APPROVE,R.PRODUCTS_ID, NVL(BIDDERS.SUBMITED_BIDS,0) TOTAL_BIDDING
                            FROM TND.RFQ_TENDER R
                            INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=R.PRODUCTS_ID
                            LEFT JOIN TNDR_SHIPMENT_MODE SM ON SM.SHIPMENT_MODE_ID=R.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT P ON P.PORT_ID=R.PORT_ID
                            LEFT JOIN TNDR_INCO_TERMS INCO ON INCO.INCO_TERMS_ID=R.INCO_TERMS
                            INNER JOIN TNDR_PAYMENT_MODE TPM ON TPM.PAYMENT_MODE_ID=R.PAY_A
                            INNER JOIN TNDR_PAYMENT_MODE TPB ON TPB.PAYMENT_MODE_ID=R.PAY_B
                            LEFT JOIN ( SELECT RFQ_NUMBER,MAX(RFQ_SL)SUBMITED_BIDS  FROM RFQ_BIDDING GROUP BY RFQ_NUMBER ) BIDDERS ON BIDDERS.RFQ_NUMBER=R.RFQ_NUMBER
                            WHERE R.RFQ_NUMBER=NVL('{rfqNumber}',R.RFQ_NUMBER )
                            ";
            Tuple<RFQ_BIDDING, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<RFQ_BIDDING>(sql);
            return _tpl;
        }

        public static Tuple<List<RFQ_BIDDING>, EQResult> getTenderListForCompare(string rfqNumber)
        {
            string sql = $@"SELECT RB.QUOTE_NUMBER, R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID, VN.ORGANIZATION_NAME VENDOR_NAME,RB.VENDOR_ID ,
                            CASE WHEN  R.SELL_BUY=0 THEN 'Buyer' ELSE 'Seller ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                              R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME PRODUCTS_ID, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
                               R.LAST_DELIVERY_DATE,
                               CASE WHEN  R.PARTIAL_SHIPMENT=2 THEN 'Allow' WHEN  R.PARTIAL_SHIPMENT=1 THEN 'Not Allow' ELSE '' END PARTIAL_SHIPMENT, SM.SHIPMENT_MODE_NAME TENDER_SHIPMENT_MODE, 
                               P.PORT_NAME TENDER_PORT_ID, R.DELIVERY_ADDRESS, R.RECEIVER_NAME, 
                               R.RECEIVER_DETAILS, CASE WHEN  R.COST_EX_INC=1 THEN 'Include' ELSE 'Exclude' END COST_EX_INC, INCO.INCO_TERMS_NAME, 
                               R.CURRENCY_NAME, R.CURRENCY_RATE, TPM.PAYMENT_MODE_NAME PAY_A, 
                               R.PAY_AP, TPM.PAYMENT_MODE_NAME PAY_B, R.PAY_BP, 
                               R.IS_APPROVE,R.PRODUCTS_ID,
                               RB.PRODUCTS_DESC , RB.PRODUCTS_QUANTITY, RB.PRODUCTS_RATE ,RB.SENDER_NAME,RB.SENDER_DETAILS,RB.LOADING_ADDRESS, QSM.SHIPMENT_MODE_NAME Q_SHIPMENT_MODE,
                               QP.PORT_NAME Q_PORT_NAME,RB.SUBMIT_DATE ,NVL(APP.APPROVAL_ID,0)  APPROVAL_ID
                            FROM RFQ_BIDDING RB 
                            INNER  JOIN RFQ_TENDER R ON R.RFQ_NUMBER=RB.RFQ_NUMBER
                            INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=R.PRODUCTS_ID
                            LEFT JOIN TNDR_SHIPMENT_MODE SM ON SM.SHIPMENT_MODE_ID=R.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT P ON P.PORT_ID=R.PORT_ID
                            LEFT JOIN TNDR_INCO_TERMS INCO ON INCO.INCO_TERMS_ID=R.INCO_TERMS
                            INNER JOIN TNDR_PAYMENT_MODE TPM ON TPM.PAYMENT_MODE_ID=R.PAY_A
                            INNER JOIN TNDR_PAYMENT_MODE TPB ON TPB.PAYMENT_MODE_ID=R.PAY_B
                            LEFT JOIN TNDR_SHIPMENT_MODE QSM ON QSM.SHIPMENT_MODE_ID=RB.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT QP ON QP.PORT_ID=RB.PORT_ID
                            INNER JOIN VENDOR VN ON VN.VENDOR_ID=RB.VENDOR_ID
                            LEFT JOIN RFQ_TENDER_APPROVAL APP ON APP.RFQ_NUMBER=R.RFQ_NUMBER
                            WHERE R.RFQ_NUMBER='{rfqNumber}'
                            ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_BIDDING>, EQResult> winingsBid(string vendorId)
        {
            string sql = $@"SELECT RB.RFQ_NUMBER, RB.QUOTE_NUMBER,TP.PRODUCTS_NAME TND_PRODUCTS_NAME, RB.PRODUCTS_RATE,RB.PRODUCTS_QUANTITY,TP.UNIT,RB.SUBMIT_DATE,APP.APPROVAL_DATE,APP.APPROVAL_NOTE
                            FROM RFQ_BIDDING RB
                            INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=RB.PRODUCTS_ID
                            INNER JOIN RFQ_TENDER_APPROVAL APP ON APP.RFQ_NUMBER=RB.RFQ_NUMBER  AND APP.VENDOR_ID=RB.VENDOR_ID AND APP.QUOTE_NUMBER=RB.QUOTE_NUMBER                        
                            WHERE RB.VENDOR_ID='{vendorId}'
                            ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

        public static EQResult SaveQuotationData(RFQ_BIDDING _obj)
        {
            var qtNumber = CommonService.getQutationNumber("RFQ_BIDDING", _obj.RFQ_NUMBER);
            int slNo = CommonService.getQutationSL(_obj.RFQ_NUMBER)+1;

            List<string> sqlList = new List<string>();

            sqlList.Add($@"INSERT INTO TND.RFQ_BIDDING (RFQ_NUMBER,QUOTE_NUMBER,RFQ_SL,VENDOR_ID,SUBMIT_DATE,PRODUCTS_ID,PRODUCTS_DESC,PRODUCTS_RATE, PRODUCTS_QUANTITY ,SHIPMENT_MODE,PORT_ID, 
                           LOADING_ADDRESS, SENDER_NAME, SENDER_DETAILS ) 
                           VALUES ( '{_obj.RFQ_NUMBER}', '{qtNumber}',{slNo},'{_obj.VENDOR_ID}',TO_DATE('{_obj.SUBMIT_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR') ,'{_obj.PRODUCTS_ID}' ,
                           '{_obj.PRODUCTS_DESC}' ,{_obj.PRODUCTS_RATE} ,{_obj.PRODUCTS_QUANTITY} , {_obj.SHIPMENT_MODE},{_obj.PORT_ID} ,'{_obj.LOADING_ADDRESS}' ,'{_obj.SENDER_NAME}','{_obj.SENDER_DETAILS}' )");
            sqlList.Add($@"UPDATE TABLE_MAX_ID SET MAX_ID=MAX_ID+1 WHERE TABLE_NAME='RFQ_BIDDING'");
             return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

        public static EQResult ApproveQuotation(RFQ_TENDER_APPROVAL _obj)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($@" INSERT INTO TND.RFQ_TENDER_APPROVAL ( APPROVAL_ID, RFQ_NUMBER, QUOTE_NUMBER, VENDOR_ID, APPROVAL_NOTE) 
                    VALUES ('{_obj.APPROVAL_ID}' ,'{_obj.RFQ_NUMBER}','{_obj.QUOTE_NUMBER}','{_obj.VENDOR_ID}','{_obj.APPROVAL_NOTE}')");
            return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

    }
}