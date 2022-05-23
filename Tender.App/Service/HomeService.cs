﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tender.Models.Models;
using Aio.Db.MSSqlEF;

namespace Tender.App.Service
{
    public class HomeService
    {
        public static Tuple<List<RFQ_BIDDING>, EQResult> applyBids(string vendorId)
        {
            string sql = $@"SELECT * FROM  RFQ_BIDDING WHERE VENDOR_ID='{vendorId}'";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_TENDER>, EQResult> newBids(string vendorId)
        {
            string sql = $@"SELECT RT.RFQ_NUMBER FROM RFQ_TENDER RT WHERE RT.RFQ_NUMBER NOT IN (SELECT APP.RFQ_NUMBER FROM RFQ_TENDER_APPROVAL APP) 
                            AND RT.RFQ_NUMBER NOT IN (SELECT DISTINCT RFQ_NUMBER FROM  RFQ_BIDDING RB WHERE RB.VENDOR_ID='{vendorId}')
                            AND RT.END_DATE>=SYSDATE AND RT.START_DATE <=SYSDATE" ;
            var objList = DatabaseMSSql.SqlQuery<RFQ_TENDER>(sql);
            return objList;
        }
        public static Tuple<List<RFQ_TENDER_APPROVAL>, EQResult> NotWinBids(string vendorId)
        {
            string sql = $@"SELECT TA.* FROM RFQ_TENDER_APPROVAL TA
            INNER JOIN ( SELECT QUOTE_NUMBER,RFQ_NUMBER FROM  RFQ_BIDDING RB WHERE RB.VENDOR_ID='{vendorId}') A ON A.RFQ_NUMBER=TA.RFQ_NUMBER
            WHERE TA.VENDOR_ID <>'{vendorId}' ";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TENDER_APPROVAL>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_TENDER>, EQResult> totalRFQ(string vendorId)
        {
            string sql = $@"SELECT RFQ_NUMBER,ADDED_DATE FROM  RFQ_TENDER WHERE  VENDOR_ID='{vendorId}'";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TENDER>(sql);
            return objList;
        }
        public static Tuple<List<RFQ_BIDDING>, EQResult> totalQuto(string vendorId)
        {
            string sql = $@"SELECT RB.* FROM  RFQ_BIDDING RB 
           INNER JOIN RFQ_TENDER RT ON RT.RFQ_NUMBER=RB.RFQ_NUMBER
           WHERE RT.VENDOR_ID='{vendorId}'";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_TENDER>, EQResult> currentRFQ(string vendorId)
        {
            string sql = $@"SELECT RFQ_NUMBER FROM  RFQ_TENDER WHERE 
            RFQ_TENDER.RFQ_NUMBER NOT IN ( SELECT RFQ_NUMBER FROM RFQ_TENDER_APPROVAL )
            AND VENDOR_ID='{vendorId}' AND START_DATE <=SYSDATE AND END_DATE>=SYSDATE";
            var objList = DatabaseMSSql.SqlQuery<RFQ_TENDER>(sql);
            return objList;
        }
        public static Tuple<List<VENDOR>, EQResult> totalSupplier()
        {
            string sql = $@"SELECT * FROM VENDOR WHERE SUPPLIER=1";
            var objList = DatabaseMSSql.SqlQuery<VENDOR>(sql);
            return objList;
        }
        public static Tuple<List<VENDOR>, EQResult> newRegistration()
        {
            string sql = $@"SELECT * FROM VENDOR WHERE  TO_DATE(ADDED_DATE, 'DD-MM-YYYY') BETWEEN  TO_DATE(SYSDATE-7, 'DD-MM-YYYY') AND TO_DATE(SYSDATE, 'DD-MM-YYYY') ORDER BY ADDED_DATE DESC";
            var objList = DatabaseMSSql.SqlQuery<VENDOR>(sql);
            return objList;
        }

        public static Tuple<List<RFQ_BIDDING>, EQResult> newQutoSubmit()
        {
            string sql = $@"SELECT RB.RFQ_NUMBER,RB.QUOTE_NUMBER,V.ORGANIZATION_NAME VENDOR_ID,RB.SUBMIT_DATE FROM RFQ_BIDDING RB
INNER JOIN VENDOR V ON V.VENDOR_ID=RB.VENDOR_ID
WHERE  TO_DATE(RB.ADDED_DATE, 'DD-MM-YYYY') BETWEEN  TO_DATE(SYSDATE-7, 'DD-MM-YYYY') AND TO_DATE(SYSDATE, 'DD-MM-YYYY') 
ORDER BY RB.ADDED_DATE DESC";
            var objList = DatabaseMSSql.SqlQuery<RFQ_BIDDING>(sql);
            return objList;
        }

    }
}