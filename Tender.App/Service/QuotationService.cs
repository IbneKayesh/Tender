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
        public static Tuple<List<RFQ_TenderView>, EQResult> SearchAllTender(string vendorId,string searchType)
        {
            var sql = "";
            if (searchType == "1") {
                sql = $@" SELECT R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID,
                           CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
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
            }
                     
            var objList = DatabaseMSSql.SqlQuery<RFQ_TenderView>(sql);
            return objList;
        }
        public static Tuple<List<RFQ_TenderView>, EQResult> getAllTender(string vendorId,string companyId)
        {
            string sql = $@"SELECT R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID,
                            CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
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
                               R.IS_APPROVE,R.PRODUCTS_ID,NVL(BIDDERS.SUBMITED_BIDS,0) SUBMITED_BIDS ,NVL(APP.APPROVAL_ID,0)  APPROVAL_ID,'N' VSUBMIT_STATUS
                            FROM TND.RFQ_TENDER R
                            INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=R.PRODUCTS_ID
                            LEFT JOIN TNDR_SHIPMENT_MODE SM ON SM.SHIPMENT_MODE_ID=R.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT P ON P.PORT_ID=R.PORT_ID
                            LEFT JOIN TNDR_INCO_TERMS INCO ON INCO.INCO_TERMS_ID=R.INCO_TERMS
                            INNER JOIN TNDR_PAYMENT_MODE TPM ON TPM.PAYMENT_MODE_ID=R.PAY_A
                            INNER JOIN TNDR_PAYMENT_MODE TPB ON TPB.PAYMENT_MODE_ID=R.PAY_B
                            LEFT JOIN ( SELECT RFQ_NUMBER,MAX(RFQ_SL)SUBMITED_BIDS  FROM RFQ_BIDDING GROUP BY RFQ_NUMBER ) BIDDERS ON BIDDERS.RFQ_NUMBER=R.RFQ_NUMBER
                            LEFT JOIN RFQ_TENDER_APPROVAL APP ON APP.RFQ_NUMBER=R.RFQ_NUMBER
                            WHERE R.COMPANY_ID='{companyId}'
                            ORDER BY R.RFQ_NUMBER DESC                            
                            ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TenderView>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_TenderView>, EQResult> getAllTenderForSupplier(string vendorId)
        {
            string sql = $@"SELECT DISTINCT R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID,
                           CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
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
                               R.IS_APPROVE,R.PRODUCTS_ID,NVL(BIDDERS.SUBMITED_BIDS,0) SUBMITED_BIDS ,NVL(APP.APPROVAL_ID,0)  APPROVAL_ID,NVL2(T1.RFQ_NUMBER,'Y','N')VSUBMIT_STATUS, COMPANY.COMPANY_NAME,C.COMPANY_ID
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
                            INNER JOIN VENDOR_COMPANY C ON C.COMPANY_ID=R.COMPANY_ID AND C.IS_ACTIVE=1
                            INNER JOIN COMPANY ON COMPANY.COMPANY_ID=C.COMPANY_ID
                           WHERE TP.GROUP_ID IN (
                            SELECT DISTINCT PRODUCTS_GROUP_ID FROM VENDOR_PRODUCTS_GROUP WHERE VENDOR_ID='{vendorId}'  AND IS_ACTIVE=1
                            ) 
                            AND TO_DATE(END_DATE,'DD/MM/YYYY') >=TO_DATE(SYSDATE,'DD/MM/YYYY')  
                            AND TO_DATE(START_DATE,'DD/MM/YYYY')<= TO_DATE(SYSDATE,'DD/MM/YYYY')
                            AND R.IS_APPROVE=0
                            ORDER BY R.RFQ_NUMBER ASC
                            ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TenderView>(sql);
            return objList;
        }

        public static Tuple<RFQ_BIDDING, EQResult> getTenderById(string rfqNumber)
        {
            string sql = $@"SELECT R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID,
                            CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                               R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME TND_PRODUCTS_NAME,TP.IMAGE_PATH, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
                               R.LAST_DELIVERY_DATE,
                               CASE WHEN  R.PARTIAL_SHIPMENT=2 THEN 'Allow' WHEN  R.PARTIAL_SHIPMENT=1 THEN 'Not Allow' ELSE '' END PARTIAL_SHIPMENT, SM.SHIPMENT_MODE_NAME TENDER_SHIPMENT_MODE, 
                               P.PORT_NAME TENDER_PORT_ID, R.DELIVERY_ADDRESS, R.RECEIVER_NAME, 
                               R.RECEIVER_DETAILS, CASE WHEN  R.COST_EX_INC=1 THEN 'Include' ELSE 'Exclude' END COST_EX_INC, INCO.INCO_TERMS_NAME, 
                               R.CURRENCY_NAME, R.CURRENCY_RATE, TPM.PAYMENT_MODE_NAME PAY_A, 
                               R.PAY_AP, TPM.PAYMENT_MODE_NAME PAY_B, R.PAY_BP, 
                               R.IS_APPROVE,R.PRODUCTS_ID, NVL(BIDDERS.SUBMITED_BIDS,0) TOTAL_BIDDING,R.SHIPMENT_MODE,R.PORT_ID,COMPANY.COMPANY_ID , COMPANY.COMPANY_NAME
                            FROM TND.RFQ_TENDER R
                            INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=R.PRODUCTS_ID
                            LEFT JOIN TNDR_SHIPMENT_MODE SM ON SM.SHIPMENT_MODE_ID=R.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT P ON P.PORT_ID=R.PORT_ID
                            LEFT JOIN TNDR_INCO_TERMS INCO ON INCO.INCO_TERMS_ID=R.INCO_TERMS
                            INNER JOIN TNDR_PAYMENT_MODE TPM ON TPM.PAYMENT_MODE_ID=R.PAY_A
                            INNER JOIN TNDR_PAYMENT_MODE TPB ON TPB.PAYMENT_MODE_ID=R.PAY_B
                            LEFT JOIN ( SELECT RFQ_NUMBER,MAX(RFQ_SL)SUBMITED_BIDS  FROM RFQ_BIDDING GROUP BY RFQ_NUMBER ) BIDDERS ON BIDDERS.RFQ_NUMBER=R.RFQ_NUMBER
                            INNER JOIN COMPANY ON COMPANY.COMPANY_ID=R.COMPANY_ID                            
                            WHERE R.RFQ_NUMBER=NVL('{rfqNumber}',R.RFQ_NUMBER )
                            ";
            Tuple<RFQ_BIDDING, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<RFQ_BIDDING>(sql);
            return _tpl;
        }
        public static Tuple<RFQ_BIDDING, EQResult> getQuotById(string quotNumber)
        {
            string sql = $@"SELECT R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID,
                            CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                               R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME TND_PRODUCTS_NAME,TP.IMAGE_PATH, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
                               R.LAST_DELIVERY_DATE,
                               CASE WHEN  R.PARTIAL_SHIPMENT=2 THEN 'Allow' WHEN  R.PARTIAL_SHIPMENT=1 THEN 'Not Allow' ELSE '' END PARTIAL_SHIPMENT, SM.SHIPMENT_MODE_NAME TENDER_SHIPMENT_MODE, 
                               P.PORT_NAME TENDER_PORT_ID, R.DELIVERY_ADDRESS, R.RECEIVER_NAME, 
                               R.RECEIVER_DETAILS, CASE WHEN  R.COST_EX_INC=1 THEN 'Include' ELSE 'Exclude' END COST_EX_INC, INCO.INCO_TERMS_NAME, 
                               R.CURRENCY_NAME, R.CURRENCY_RATE, TPM.PAYMENT_MODE_NAME PAY_A, 
                               R.PAY_AP, TPM.PAYMENT_MODE_NAME PAY_B, R.PAY_BP, 
                               R.IS_APPROVE,R.PRODUCTS_ID, NVL(BIDDERS.SUBMITED_BIDS,0) TOTAL_BIDDING,R.SHIPMENT_MODE,R.PORT_ID,COMPANY.COMPANY_ID , COMPANY.COMPANY_NAME
                            ,B.QUOTE_NUMBER,B.LOADING_ADDRESS,B.NOTE,B.PRODUCTS_DESC,B.PRODUCTS_QUANTITY,B.PRODUCTS_RATE,B.SENDER_DETAILS,B.SENDER_NAME,B.SUBMIT_DATE,SMB.SHIPMENT_MODE_NAME QUOT_SHIPMENT_MODE,PB.PORT_NAME QUOT_PORT
                            FROM TND.RFQ_TENDER R
                            INNER JOIN RFQ_BIDDING B ON B.RFQ_NUMBER=R.RFQ_NUMBER
                            INNER JOIN TNDR_PRODUCTS TP ON TP.PRODUCTS_ID=R.PRODUCTS_ID
                            LEFT JOIN TNDR_SHIPMENT_MODE SM ON SM.SHIPMENT_MODE_ID=R.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT P ON P.PORT_ID=R.PORT_ID
                            LEFT JOIN TNDR_INCO_TERMS INCO ON INCO.INCO_TERMS_ID=R.INCO_TERMS
                            INNER JOIN TNDR_PAYMENT_MODE TPM ON TPM.PAYMENT_MODE_ID=R.PAY_A
                            INNER JOIN TNDR_PAYMENT_MODE TPB ON TPB.PAYMENT_MODE_ID=R.PAY_B
                            LEFT JOIN ( SELECT RFQ_NUMBER,MAX(RFQ_SL)SUBMITED_BIDS  FROM RFQ_BIDDING GROUP BY RFQ_NUMBER ) BIDDERS ON BIDDERS.RFQ_NUMBER=R.RFQ_NUMBER
                            INNER JOIN COMPANY ON COMPANY.COMPANY_ID=R.COMPANY_ID                            
                            LEFT JOIN TNDR_SHIPMENT_MODE SMB ON SMB.SHIPMENT_MODE_ID=B.SHIPMENT_MODE
                            LEFT JOIN TNDR_PORT PB ON PB.PORT_ID=B.PORT_ID
                            WHERE B.QUOTE_NUMBER ='{quotNumber}'
                            ";
            Tuple<RFQ_BIDDING, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<RFQ_BIDDING>(sql);
            return _tpl;
        }
        public static Tuple<List<RFQ_TNDR_DOCUMENTS>, EQResult> getTenderDocumentList(string rfqNumber)
        {
            string sql = $@"SELECT RTD.RFQ_NUMBER, TD.DOCUMENTS_NAME,RTD.DOCUMENTS_ID,RTD.IS_ACTIVE FROM RFQ_TNDR_DOCUMENTS RTD
                            INNER JOIN  TNDR_DOCUMENTS TD ON TD.DOCUMENTS_ID=RTD.DOCUMENTS_ID
                            WHERE RTD.RFQ_NUMBER='{rfqNumber}'";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TNDR_DOCUMENTS>(sql);
            return objList;
        }
        public static Tuple<List<RFQ_BIDDING>, EQResult> getTenderListForCompare(string rfqNumber)
        {
            string sql = $@"SELECT RB.QUOTE_NUMBER, R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID, VN.ORGANIZATION_NAME VENDOR_NAME,RB.VENDOR_ID ,
                           CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                              R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME PRODUCTS_ID, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE,TP.IMAGE_PATH, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
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
            string sql = $@"SELECT RB.QUOTE_NUMBER, R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID, VN.ORGANIZATION_NAME VENDOR_NAME,RB.VENDOR_ID ,RB.SUBMIT_DATE, APP.APPROVAL_DATE, APP.APPROVAL_NOTE,
                            CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                              R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME TND_PRODUCTS_NAME, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE,TP.IMAGE_PATH, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
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
                            INNER JOIN RFQ_TENDER_APPROVAL APP  ON APP.RFQ_NUMBER = RB.RFQ_NUMBER AND APP.VENDOR_ID = RB.VENDOR_ID AND APP.QUOTE_NUMBER = RB.QUOTE_NUMBER
                            WHERE RB.VENDOR_ID='{vendorId}'
                            ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_BIDDING>, EQResult> totalApplyBids(string vendorId)
        {
            string sql = $@"SELECT RB.*,P.PRODUCTS_NAME TND_PRODUCTS_NAME,T.CURRENCY_NAME,P.UNIT  FROM RFQ_BIDDING RB
                            INNER JOIN TNDR_PRODUCTS P ON P.PRODUCTS_ID=RB.PRODUCTS_ID
                            INNER JOIN RFQ_TENDER T ON T.RFQ_NUMBER=RB.RFQ_NUMBER
                            WHERE RB.VENDOR_ID='{vendorId}' ORDER BY RB.QUOTE_NUMBER DESC";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_BIDDING>, EQResult> winingsBidforNftn(string vendorId)
        {
            string sql = $@"SELECT RB.QUOTE_NUMBER, R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID, VN.ORGANIZATION_NAME VENDOR_NAME,RB.VENDOR_ID ,RB.SUBMIT_DATE, APP.APPROVAL_DATE, APP.APPROVAL_NOTE,
                           CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                              R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME TND_PRODUCTS_NAME, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE,TP.IMAGE_PATH, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
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
                            INNER JOIN RFQ_TENDER_APPROVAL APP  ON APP.RFQ_NUMBER = RB.RFQ_NUMBER AND APP.VENDOR_ID = RB.VENDOR_ID AND APP.QUOTE_NUMBER = RB.QUOTE_NUMBER
                            WHERE RB.VENDOR_ID='{vendorId}'
                            AND ADD_MONTHS((to_date(APP.APPROVAL_DATE,'DD-MM-YYYY')), 1)>=to_date(sysdate,'DD-MM-YYYY')
                            ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

        public static EQResult SaveQuotationData(RFQ_BIDDING _obj)
        {
            var qtNumber = CommonService.getQutationNumber("RFQ_BIDDING", _obj.RFQ_NUMBER,_obj.COMPANY_ID);
            int slNo = CommonService.getQutationSL(_obj.RFQ_NUMBER)+1;

            List<string> sqlList = new List<string>();

            sqlList.Add($@"INSERT INTO RFQ_BIDDING (RFQ_NUMBER,QUOTE_NUMBER,RFQ_SL,VENDOR_ID,SUBMIT_DATE,PRODUCTS_ID,PRODUCTS_DESC,PRODUCTS_RATE, PRODUCTS_QUANTITY ,SHIPMENT_MODE,PORT_ID, 
                           LOADING_ADDRESS, SENDER_NAME, SENDER_DETAILS ) 
                           VALUES ( '{_obj.RFQ_NUMBER}', '{qtNumber}',{slNo},'{_obj.VENDOR_ID}',TO_DATE('{_obj.SUBMIT_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR') ,'{_obj.PRODUCTS_ID}' ,
                           '{_obj.PRODUCTS_DESC}' ,{_obj.PRODUCTS_RATE} ,{_obj.PRODUCTS_QUANTITY} , {_obj.SHIPMENT_MODE},{_obj.PORT_ID} ,'{_obj.LOADING_ADDRESS}' ,'{_obj.SENDER_NAME}','{_obj.SENDER_DETAILS}' )");
            sqlList.Add($@"UPDATE TABLE_MAX_ID SET MAX_ID=MAX_ID+1 WHERE TABLE_NAME='RFQ_BIDDING' AND COMPANY_ID='{_obj.COMPANY_ID}'");
             return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

        public static EQResult ApproveQuotation(RFQ_TENDER_APPROVAL _obj)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($@" INSERT INTO RFQ_TENDER_APPROVAL ( APPROVAL_ID, RFQ_NUMBER, QUOTE_NUMBER, VENDOR_ID, APPROVAL_NOTE) 
                    VALUES ('{_obj.APPROVAL_ID}' ,'{_obj.RFQ_NUMBER}','{_obj.QUOTE_NUMBER}','{_obj.VENDOR_ID}','{_obj.APPROVAL_NOTE}')");
            return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }
        public static EQResult ApproveQuotationUpdate(RFQ_TENDER_APPROVAL _obj)
        {
            List<string> sqlList = new List<string>();
            if (_obj.FIRST_APRV_BY != null && _obj.FIRST_APRV_STATUS != null) {
                sqlList.Add($@"UPDATE TND.RFQ_TENDER_APPROVAL SET       
                           FIRST_APRV_STATUS ='{_obj.FIRST_APRV_STATUS}',
                           FIRST_APRV_DATE =TO_DATE('{_obj.FIRST_APRV_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR'),
                           FIRST_APRV_NOTE='{_obj.FIRST_APRV_NOTE}',
                           FIRST_APRV_BY='{_obj.FIRST_APRV_BY}'
                           where RFQ_NUMBER='{_obj.RFQ_NUMBER}' AND QUOTE_NUMBER='{_obj.QUOTE_NUMBER}'");
            }
            else if (_obj.SEC_APRV_BY != null && _obj.SEC_APRV_STATUS != null)
            {
                sqlList.Add($@"UPDATE TND.RFQ_TENDER_APPROVAL SET       
                           SEC_APRV_STATUS='{_obj.SEC_APRV_STATUS}',
                           SEC_APRV_DATE=TO_DATE('{_obj.SEC_APRV_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR'),
                           SEC_APRV_NOTE='{_obj.SEC_APRV_NOTE}',
                           SEC_APRV_BY='{_obj.SEC_APRV_BY}'
                           where RFQ_NUMBER='{_obj.RFQ_NUMBER}' AND QUOTE_NUMBER='{_obj.QUOTE_NUMBER}'");
            }
            else if (_obj.THIRD_APRV_BY != null && _obj.THIRD_APRV_STATUS != null)
            {
                sqlList.Add($@"UPDATE TND.RFQ_TENDER_APPROVAL SET
                           THIRD_APRV_STATUS='{_obj.THIRD_APRV_STATUS}',
                           THIRD_APRV_DATE=TO_DATE('{_obj.THIRD_APRV_DATE.ToString("dd-MMM-yyyy")}','DD-MON-RRRR'),
                           THIRD_APRV_NOTE='{_obj.THIRD_APRV_NOTE}',
                           THIRD_APRV_BY='{_obj.THIRD_APRV_BY}'
                           where RFQ_NUMBER='{_obj.RFQ_NUMBER}' AND QUOTE_NUMBER='{_obj.QUOTE_NUMBER}'");
            }
            
            return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

        public static Tuple<List<RFQ_TENDER_APPROVAL_VIEW>, EQResult> ApproveTenderList()
        {
            string sql = $@"SELECT RB.QUOTE_NUMBER, R.RFQ_NUMBER, R.VENDOR_ID TND_VENDOR_ID, VN.ORGANIZATION_NAME VENDOR_NAME,RB.VENDOR_ID ,
                            CASE WHEN  R.SELL_BUY=1 THEN 'Buy' ELSE 'Sale ' END SELL_BUY, 
                            CASE WHEN    R.LOCAL_IMPORT=1 THEN 'Local' ELSE 'Importer' END  LOCAL_IMPORT,
                            CASE WHEN     R.RE_BID=1 THEN 'Submit Once' ELSE 'Submit Multiple' END RE_BID,
                            CASE WHEN  R.LOWER_RATE=1 THEN 'Lower Rate Only' ELSE 'Any Rate' END LOWER_RATE, 
                              R.START_DATE, R.END_DATE, TP.PRODUCTS_NAME PRODUCTS_ID, TP.UNIT, 
                               R.PRODUCTS_DESC TND_PRODUCTS_DESC, R.PRODUCTS_RATE TND_PRODUCTS_RATE,TP.IMAGE_PATH, R.PRODUCTS_QUANTITY TND_PRODUCTS_QTY, 
                               R.LAST_DELIVERY_DATE,
                               CASE WHEN  R.PARTIAL_SHIPMENT=2 THEN 'Allow' WHEN  R.PARTIAL_SHIPMENT=1 THEN 'Not Allow' ELSE '' END PARTIAL_SHIPMENT, SM.SHIPMENT_MODE_NAME TENDER_SHIPMENT_MODE, 
                               P.PORT_NAME TENDER_PORT_ID, R.DELIVERY_ADDRESS, R.RECEIVER_NAME, 
                               R.RECEIVER_DETAILS, CASE WHEN  R.COST_EX_INC=1 THEN 'Include' ELSE 'Exclude' END COST_EX_INC, INCO.INCO_TERMS_NAME, 
                               R.CURRENCY_NAME, R.CURRENCY_RATE, TPM.PAYMENT_MODE_NAME PAY_A, 
                               R.PAY_AP, TPM.PAYMENT_MODE_NAME PAY_B, R.PAY_BP, 
                               R.IS_APPROVE,R.PRODUCTS_ID,
                               RB.PRODUCTS_DESC , RB.PRODUCTS_QUANTITY, RB.PRODUCTS_RATE ,RB.SENDER_NAME,RB.SENDER_DETAILS,RB.LOADING_ADDRESS, QSM.SHIPMENT_MODE_NAME Q_SHIPMENT_MODE,
                               QP.PORT_NAME Q_PORT_NAME,RB.SUBMIT_DATE ,NVL(APP.APPROVAL_ID,0)  APPROVAL_ID,APP.APPROVAL_DATE,APP.APPROVAL_NOTE,APP.APPROVE_BY,APP.FIRST_APRV_BY,APP.FIRST_APRV_DATE,APP.FIRST_APRV_STATUS,APP.FIRST_APRV_NOTE,APP.SEC_APRV_BY,APP.SEC_APRV_DATE,APP.SEC_APRV_NOTE,APP.SEC_APRV_STATUS,APP.THIRD_APRV_BY,APP.THIRD_APRV_DATE,APP.THIRD_APRV_NOTE,APP.THIRD_APRV_STATUS
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
                            INNER JOIN RFQ_TENDER_APPROVAL APP ON APP.RFQ_NUMBER=R.RFQ_NUMBER AND APP.QUOTE_NUMBER=RB.QUOTE_NUMBER";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TENDER_APPROVAL_VIEW>(sql);
            return objList;
        }
        public static Tuple<List<RFQ_BIDDING>, EQResult> submitOnceCheck(string rfqNumber,string vendorId)
        {
            string sql = $@"SELECT * FROM RFQ_BIDDING WHERE RFQ_NUMBER = '{rfqNumber}' AND VENDOR_ID = '{vendorId}'";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }
    }
}