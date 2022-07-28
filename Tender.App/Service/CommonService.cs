using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class CommonService
    {
        public static string getTenderNumber(string tableName,string comID)
        {
            string sql = $"select MAX_ID from TABLE_MAX_ID where TABLE_NAME='{tableName}' and COMPANY_ID='{comID}'";
            Tuple<TABLE_MAX_ID, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<TABLE_MAX_ID>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                return string.Empty;               
            }
            else {
                DateTime dt = DateTime.Now;
                int _maxId = _tpl.Item1.MAX_ID + 1;
                return "RFQ-" +dt.ToString("yy") + dt.ToString("MM") + comID + _maxId.ToString().PadLeft(5, '0');
            }            
        }

        public static string getQutationNumber(string tableName,string rfqNumber,string comId)
        {
            string sql = $"select MAX_ID from TABLE_MAX_ID where TABLE_NAME='{tableName}' and COMPANY_ID='{comId}'";
            Tuple<TABLE_MAX_ID, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<TABLE_MAX_ID>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                return string.Empty;
            }
            else
            {  
                DateTime dt = DateTime.Now;
                int _maxId = _tpl.Item1.MAX_ID + 1;
                return "Q" + dt.ToString("yy") + dt.ToString("MM") +rfqNumber.Substring(rfqNumber.Length-8)+ _maxId.ToString().PadLeft(5, '0');
            }
        }
        public static int getQutationSL(string rfqNumber)
        {
            string sql = $"SELECT RFQ_SL FROM RFQ_BIDDING WHERE RFQ_NUMBER='{rfqNumber}'";
            Tuple<RFQ_BIDDING, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<RFQ_BIDDING>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                return 0;
            }
            else
            {               
                return _tpl.Item1.RFQ_SL;
            }
        }
        public static string productId(string tableName)
        {
            string sql = $"select MAX_ID from TABLE_MAX_ID where TABLE_NAME='{tableName}'";
            Tuple<TABLE_MAX_ID, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<TABLE_MAX_ID>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                return string.Empty;
            }
            else
            {
                int _maxId = _tpl.Item1.MAX_ID + 1;
                return _maxId.ToString();
            }
        }

        public static Tuple<List<VENDOR>, EQResult> GetVendor(string vendorId)
        {
            string sql = $"SELECT V.VENDOR_ID,ORGANIZATION_NAME FROM VENDOR V WHERE V.VENDOR_ID='{vendorId}'";
            return DatabaseMSSql.SqlQuery<VENDOR>(sql);
        }
        public static Tuple<List<COMPANY>, EQResult> GetCompany(string _companyId)
        {
            string sql = $"SELECT V.COMPANY_ID,COMPANY_NAME FROM COMPANY V WHERE V.COMPANY_ID=NVL('{_companyId}',V.COMPANY_ID)";
            return DatabaseMSSql.SqlQuery<COMPANY>(sql);
        }
        public static decimal getQuotMinimumBidRate(string rfqNumber, string vendorId)
        {
            string sql = $"SELECT MIN(PRODUCTS_RATE)PRODUCTS_RATE FROM RFQ_BIDDING WHERE RFQ_NUMBER = '{rfqNumber}' AND VENDOR_ID = '{vendorId}'";
            Tuple<RFQ_BIDDING, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<RFQ_BIDDING>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                return 0;
            }
            else
            {
                return _tpl.Item1.PRODUCTS_RATE;
            }
        }
    }
}