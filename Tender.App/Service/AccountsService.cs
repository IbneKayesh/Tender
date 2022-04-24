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
            string id = Guid.NewGuid().ToString();
            sql = $@"insert into VENDOR(VENDOR_ID,VENDOR_EMAIL,VENDOR_PASSWD,ORGANIZATION_NAME,COUNTRY_NAME,SUPPLIER,PURCHASER,SUPPLIER_NOTIFY,PURCHASER_NOTIFY)
                    values('{id}','{_obj.VENDOR_EMAIL}','{_obj.VENDOR_PASSWD}','{_obj.ORGANIZATION_NAME}','{_obj.COUNTRY_NAME}','{_obj.SUPPLIER}','{_obj.PURCHASER}','{_obj.SUPPLIER_NOTIFY}','{_obj.PURCHASER_NOTIFY}')";
            return DatabaseMSSql.ExecuteSqlCommand(sql);
        }
    }
}