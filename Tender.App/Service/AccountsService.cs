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
                sql = $@"insert into VENDOR(VENDOR_ID,VENDOR_EMAIL,VENDOR_PASSWD,ORGANIZATION_NAME,COUNTRY_NAME,SUPPLIER,PURCHASER,SUPPLIER_NOTIFY,PURCHASER_NOTIFY,VENDOR_USER_ID)
                    values('{_obj.VENDOR_ID}','{_obj.VENDOR_EMAIL.ToLower()}','{_obj.VENDOR_PASSWD}','{_obj.ORGANIZATION_NAME}','{_obj.COUNTRY_NAME}','{_obj.SUPPLIER}','{_obj.PURCHASER}','{_obj.SUPPLIER_NOTIFY}','{_obj.PURCHASER_NOTIFY}','{_obj.VENDOR_EMAIL}')";
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
        public static EQResult change_password(string id, CHANGE_PASWD _obj)
        {
            sql = $"update VENDOR set VENDOR_PASSWD='{_obj.NPASWD}' where VENDOR_ID='{id}'";
            return DatabaseMSSql.ExecuteSqlCommand(sql);
        }
        public static EQResult add_edit_follow_me(string id, string cID, string cNAME)
        {
            if (cID == "v-pro")
            {
                sql = $"select VENDOR_ID,PRODUCTS_ID,PRODUCTS_NAME,IS_ACTIVE from VENDOR_PRODUCTS where VENDOR_ID='{id}' and PRODUCTS_ID='{cNAME}'";
                var _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_CATEGORY>(sql);
                if (_tpl.Item2.ROWS == 0)
                {
                    sql = $"INSERT INTO VENDOR_PRODUCTS(VENDOR_ID,PRODUCTS_ID,PRODUCTS_NAME,IS_ACTIVE)VALUES('{id}','{cNAME}','{cNAME}',1)";
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
                else
                {
                    if (_tpl.Item1.IS_ACTIVE == 1)
                    {
                        sql = $"update VENDOR_PRODUCTS set IS_ACTIVE=0 where VENDOR_ID='{id}' and PRODUCTS_ID='{cNAME}'";
                    }
                    else
                    {
                        sql = $"update VENDOR_PRODUCTS set IS_ACTIVE=1 where VENDOR_ID='{id}' and PRODUCTS_ID='{cNAME}'";
                    }
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
            }
            else if (cID == "v-cat")
            {
                sql = $"select VENDOR_ID,CATEGORY_ID,CATEGORY_NAME,IS_ACTIVE from VENDOR_CATEGORY where VENDOR_ID='{id}' and CATEGORY_ID='{cNAME}'";
                var _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_CATEGORY>(sql);
                if (_tpl.Item2.ROWS == 0)
                {
                    sql = $"INSERT INTO VENDOR_CATEGORY(VENDOR_ID,CATEGORY_ID,CATEGORY_NAME,IS_ACTIVE)VALUES('{id}','{cNAME}','{cNAME}',1)";
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
                else
                {
                    if (_tpl.Item1.IS_ACTIVE == 1)
                    {
                        sql = $"update VENDOR_CATEGORY set IS_ACTIVE=0 where VENDOR_ID='{id}' and CATEGORY_ID='{cNAME}'";
                    }
                    else
                    {
                        sql = $"update VENDOR_CATEGORY set IS_ACTIVE=1 where VENDOR_ID='{id}' and CATEGORY_ID='{cNAME}'";
                    }
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
            }
            else if (cID == "v-cer")
            {
                sql = $"select VENDOR_ID,CERTIFICATE_ID,CERTIFICATE_NAME,IS_ACTIVE from VENDOR_CERTIFICATE where VENDOR_ID='{id}' and CERTIFICATE_ID='{cNAME}'";
                var _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_CATEGORY>(sql);
                if (_tpl.Item2.ROWS == 0)
                {
                    sql = $"INSERT INTO VENDOR_CERTIFICATE(VENDOR_ID,CERTIFICATE_ID,CERTIFICATE_NAME,IS_ACTIVE)VALUES('{id}','{cNAME}','{cNAME}',1)";
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
                else
                {
                    if (_tpl.Item1.IS_ACTIVE == 1)
                    {
                        sql = $"update VENDOR_CERTIFICATE set IS_ACTIVE=0 where VENDOR_ID='{id}' and CERTIFICATE_ID='{cNAME}'";
                    }
                    else
                    {
                        sql = $"update VENDOR_CERTIFICATE set IS_ACTIVE=1 where VENDOR_ID='{id}' and CERTIFICATE_ID='{cNAME}'";
                    }
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }

            }
            else if (cID == "v-doc")
            {
                sql = $"select VENDOR_ID,DOCUMENTS_ID,DOCUMENTS_NAME,IS_ACTIVE from VENDOR_DOCUMENTS where VENDOR_ID='{id}' and DOCUMENTS_ID='{cNAME}'";
                var _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_CATEGORY>(sql);
                if (_tpl.Item2.ROWS == 0)
                {
                    sql = $"INSERT INTO VENDOR_DOCUMENTS(VENDOR_ID,DOCUMENTS_ID,DOCUMENTS_NAME,IS_ACTIVE)VALUES('{id}','{cNAME}','{cNAME}',1)";
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
                else
                {
                    if (_tpl.Item1.IS_ACTIVE == 1)
                    {
                        sql = $"update VENDOR_DOCUMENTS set IS_ACTIVE=0 where VENDOR_ID='{id}' and DOCUMENTS_ID='{cNAME}'";
                    }
                    else
                    {
                        sql = $"update VENDOR_DOCUMENTS set IS_ACTIVE=1 where VENDOR_ID='{id}' and DOCUMENTS_ID='{cNAME}'";
                    }
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
            }
            return new EQResult();
        }
        public static Tuple<List<VENDOR_CATEGORY>, EQResult> getVENDOR_CATEGORY(string id)
        {
            sql = $@"SELECT T1.CATEGORY_ID,T1.CATEGORY_NAME,
                    CASE WHEN T2.IS_ACTIVE IS NULL
                    THEN 0
                    ELSE
                    T2.IS_ACTIVE
                    END IS_ACTIVE
                    FROM TNDR_CATEGORY T1
                    LEFT JOIN VENDOR_CATEGORY T2 ON T1.CATEGORY_ID = T2.CATEGORY_ID
                    AND T2.VENDOR_ID='{id}'";
            return DatabaseMSSql.SqlQuery<VENDOR_CATEGORY>(sql);
        }
        public static Tuple<List<VENDOR_CERTIFICATE>, EQResult> getVENDOR_CERTIFICATE(string id)
        {
            sql = $@"SELECT T1.CERTIFICATE_ID,T1.CERTIFICATE_NAME,
                    CASE WHEN T2.IS_ACTIVE IS NULL
                    THEN 0
                    ELSE
                    T2.IS_ACTIVE
                    END IS_ACTIVE
                    FROM TNDR_CERTIFICATE T1
                    LEFT JOIN VENDOR_CERTIFICATE T2 ON T1.CERTIFICATE_ID = T2.CERTIFICATE_ID
                    AND T2.VENDOR_ID='{id}'";
            return DatabaseMSSql.SqlQuery<VENDOR_CERTIFICATE>(sql);
        }

        public static Tuple<List<VENDOR_DOCUMENTS>, EQResult> getVENDOR_DOCUMENTS(string id)
        {
            sql = $@"SELECT T1.DOCUMENTS_ID,T1.DOCUMENTS_NAME,
                    CASE WHEN T2.IS_ACTIVE IS NULL
                    THEN 0
                    ELSE
                    T2.IS_ACTIVE
                    END IS_ACTIVE
                    FROM TNDR_DOCUMENTS T1
                    LEFT JOIN VENDOR_DOCUMENTS T2 ON T1.DOCUMENTS_ID = T2.DOCUMENTS_ID
                    AND T2.VENDOR_ID='{id}'";
            return DatabaseMSSql.SqlQuery<VENDOR_DOCUMENTS>(sql);
        }

        public static Tuple<List<VENDOR_PRODUCTS>, EQResult> getVENDOR_PRODUCTS(string id)
        {
            sql = $@"SELECT T1.PRODUCTS_ID,T1.PRODUCTS_NAME,
                    CASE WHEN T2.IS_ACTIVE IS NULL
                    THEN 0
                    ELSE
                    T2.IS_ACTIVE
                    END IS_ACTIVE
                    FROM TNDR_PRODUCTS T1
                    LEFT JOIN VENDOR_PRODUCTS T2 ON T1.PRODUCTS_ID = T2.PRODUCTS_ID
                    AND T2.VENDOR_ID='{id}'";
            return DatabaseMSSql.SqlQuery<VENDOR_PRODUCTS>(sql);
        }

        public static EQResult profileUpdate(VENDOR_DETAILS _obj)
        {
                sql = $@"update VENDOR set VENDOR_USER_ID='{_obj.VENDOR_USER_ID}',CONTACT_NAME='{_obj.CONTACT_NAME}',
                CONTACT_NUMBER='{_obj.CONTACT_NAME}',ADDRESS_1='{_obj.ADDRESS_1}',ADDRESS_2='{_obj.ADDRESS_2}',YEAR_OF_ESTABLISHMENT='{_obj.YEAR_OF_ESTABLISHMENT}',
                YEARLY_TRUNOVER='{_obj.YEARLY_TRUNOVER}',TAX_NUMBER='{_obj.TAX_NUMBER}',TRADE_NUMBER='{_obj.TRADE_NUMBER}',
                PURCHASER='{_obj.PURCHASER}',PURCHASER_NOTIFY='{_obj.PURCHASER_NOTIFY}',SUPPLIER='{_obj.SUPPLIER}',SUPPLIER_NOTIFY='{_obj.SUPPLIER_NOTIFY}'
                where VENDOR_ID='{_obj.VENDOR_ID}'";
                return DatabaseMSSql.ExecuteSqlCommand(sql);
        }
        public static Tuple<VENDOR_DETAILS, EQResult> getProfile( string id)
        {
            sql = $"select t.* from VENDOR t where t.VENDOR_ID='{id}'";
            Tuple<VENDOR_DETAILS, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_DETAILS>(sql);
            return _tpl;
        }


    }
}