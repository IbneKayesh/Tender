using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class CommonService
    {
        public static string getTenderNumber(string tableName)
        {
            string sql = $"select MAX_ID from TABLE_MAX_ID where TABLE_NAME='{tableName}'";
            Tuple<TABLE_MAX_ID, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<TABLE_MAX_ID>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                return string.Empty;               
            }
            else {
                DateTime dt = DateTime.Now;
                int _maxId = _tpl.Item1.MAX_ID + 1;
                return "TN-" + dt.ToString("yy") + dt.ToString("MM") + _maxId.ToString().PadLeft(5, '0');
            }            
        }

        public static string getQutationNumber(string tableName,string rfqNumber)
        {
            string sql = $"select MAX_ID from TABLE_MAX_ID where TABLE_NAME='{tableName}'";
            Tuple<TABLE_MAX_ID, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<TABLE_MAX_ID>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                return string.Empty;
            }
            else
            {  
                DateTime dt = DateTime.Now;
                int _maxId = _tpl.Item1.MAX_ID + 1;
                return "Q" + dt.ToString("yy") + dt.ToString("MM") +rfqNumber.Substring(rfqNumber.Length-5)+ _maxId.ToString().PadLeft(5, '0');
            }
        }
        public static Tuple<List<VENDOR>, EQResult> GetVendor(string vendorId)
        {
            string sql = $"SELECT V.VENDOR_ID,ORGANIZATION_NAME FROM VENDOR V WHERE V.VENDOR_ID='{vendorId}'";
            return DatabaseMSSql.SqlQuery<VENDOR>(sql);
        }
    }
}