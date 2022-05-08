using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class AccountsService
    {
        static string sql = "";
        public static Tuple<VENDOR_LOGIN, EQResult> UserLogin(VENDOR_LOGIN _obj)
        {
            sql = $"select VENDOR_EMAIL,VENDOR_PASSWD from VENDOR t where t.VENDOR_EMAIL='{_obj.VENDOR_EMAIL}' and VENDOR_PASSWD='{_obj.VENDOR_PASSWD}'";
            return DatabaseMSSql.SqlQuerySingle<VENDOR_LOGIN>(sql);
        }
        public static Tuple<List<VENDOR_LOGIN>, EQResult> getItemMaster()
        {
            sql = $"select ITEM_ID,ITEM_NAME,ITEM_TYPE from ITEM_MASTER";
            return DatabaseMSSql.SqlQuery<VENDOR_LOGIN>(sql);
        }
        public static EQResult registration(VENDOR _obj)
        {
            sql = $"select VENDOR_EMAIL,VENDOR_PASSWD from VENDOR t where t.VENDOR_EMAIL='{_obj.VENDOR_EMAIL.ToLower()}'";
            Tuple<VENDOR_LOGIN, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_LOGIN>(sql);
            if (!_tpl.Item2.SUCCESS)
            {
                sql = $@"insert into VENDOR(VENDOR_ID,VENDOR_EMAIL,VENDOR_PASSWD,ORGANIZATION_NAME,COUNTRY_NAME,SUPPLIER,PURCHASER,SUPPLIER_NOTIFY,PURCHASER_NOTIFY)
                    values('{_obj.VENDOR_ID}','{_obj.VENDOR_EMAIL.ToLower()}','{_obj.VENDOR_PASSWD}','{_obj.ORGANIZATION_NAME}','{_obj.COUNTRY_NAME}','{_obj.SUPPLIER}','{_obj.PURCHASER}','{_obj.SUPPLIER_NOTIFY}','{_obj.PURCHASER_NOTIFY}')";
                return DatabaseMSSql.ExecuteSqlCommand(sql);
            }
            EQResult obj = new EQResult();
            obj.ROWS = 99;
            obj.MESSAGES = "Email already exists";
            return obj;
        }
        public static EQResult email_confirmation(string id)
        {
            sql = $"update VENDOR set IS_CONFIRMED=1 where VENDOR_ID='{id}'";
            return DatabaseMSSql.ExecuteSqlCommand(sql);
        }
    }
}