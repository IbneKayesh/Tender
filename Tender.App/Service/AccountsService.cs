using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class AccountsService
    {
        static string sql = "";
        public static Tuple<VENDER_SESSION, EQResult> UserLogin(VENDOR_LOGIN _obj)
        {
            sql = $"select VENDOR_EMAIL,VENDOR_ID,ORGANIZATION_NAME,COUNTRY_NAME,SUPPLIER,PURCHASER,VENDOR_USER_ID from VENDOR t where t.VENDOR_EMAIL='{_obj.VENDOR_EMAIL}' and VENDOR_PASSWD='{_obj.VENDOR_PASSWD}'";
            return DatabaseMSSql.SqlQuerySingle<VENDER_SESSION>(sql);
        }
        #region FORGET PASSWORD
        public static EQResult SaveToken(MAIL_NOTIFICATION_TOKEN _obj, string token_type)
        {
            sql = $"select * from MAIL_NOTIFICATION_TOKEN WHERE TOKEN_TYPE='{token_type}' AND LOWER(VENDOR_EMAIL)='{_obj.VENDOR_EMAIL.ToLower()}'";
            Tuple<VENDOR_LOGIN, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_LOGIN>(sql);
            if (_tpl.Item2.ROWS == 0)
            {
                sql = $@"INSERT INTO MAIL_NOTIFICATION_TOKEN (VENDOR_EMAIL,TOKEN,TOKEN_TYPE) VALUES ('{_obj.VENDOR_EMAIL}',{_obj.TOKEN},'{token_type}')";
                return DatabaseMSSql.ExecuteSqlCommand(sql);
            }
            else {
                sql = $@"UPDATE MAIL_NOTIFICATION_TOKEN SET TOKEN='{_obj.TOKEN}' WHERE TOKEN_TYPE='{token_type}' AND LOWER(VENDOR_EMAIL)='{_obj.VENDOR_EMAIL.ToLower()}'";
                return DatabaseMSSql.ExecuteSqlCommand(sql);
            }            
        }
        public static Tuple<VENDOR_LOGIN, EQResult> EmailVerification(VENDOR_LOGIN _obj)
        {
            sql = $"select VENDOR_EMAIL,VENDOR_ID,VENDOR_PASSWD,ORGANIZATION_NAME,COUNTRY_NAME,SUPPLIER,PURCHASER,VENDOR_USER_ID from VENDOR t where t.VENDOR_EMAIL='{_obj.VENDOR_EMAIL}'";
            return DatabaseMSSql.SqlQuerySingle<VENDOR_LOGIN>(sql);
        }

        public static Tuple<MAIL_NOTIFICATION_TOKEN, EQResult> EmailTokenVerify(MAIL_NOTIFICATION_TOKEN _obj, string token_type)
        {
            sql = $"SELECT * from MAIL_NOTIFICATION_TOKEN t where t.VENDOR_EMAIL='{_obj.VENDOR_EMAIL}' AND t.TOKEN={_obj.TOKEN} AND T.TOKEN_TYPE='{token_type}'";
            return DatabaseMSSql.SqlQuerySingle<MAIL_NOTIFICATION_TOKEN>(sql);
        }

        public static EQResult ResetPassword(VENDOR_LOGIN _obj)
        {
            sql = $@"UPDATE VENDOR SET VENDOR_PASSWD='{_obj.VENDOR_PASSWD}' WHERE LOWER(VENDOR_EMAIL)='{_obj.VENDOR_EMAIL.ToLower()}'";        
            return DatabaseMSSql.ExecuteSqlCommand(sql);
           
        }

        #endregion


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
            sql = $"update VENDOR set IS_ACTIVE=1 where VENDOR_EMAIL='{id}'";
            return DatabaseMSSql.ExecuteSqlCommand(sql);
        }
        public static EQResult change_password(string id, CHANGE_PASWD _obj)
        {
            sql = $"update VENDOR set VENDOR_PASSWD='{_obj.NPASWD}' where VENDOR_ID='{id}'";
            return DatabaseMSSql.ExecuteSqlCommand(sql);
        }
        public static EQResult add_edit_follow_me(string id, string cID, string cNAME)
        {
            if (cID == "v-pcat")
            {
                sql = $"select VENDOR_ID,PRODUCTS_GROUP_ID,PRODUCTS_GROUP_NAME,IS_ACTIVE from VENDOR_PRODUCTS_GROUP where VENDOR_ID ='{id}' and PRODUCTS_GROUP_ID='{cNAME}'";
                var _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_CATEGORY>(sql);
                if (_tpl.Item2.ROWS == 0)
                {
                    sql = $"INSERT INTO VENDOR_PRODUCTS_GROUP(VENDOR_ID,PRODUCTS_GROUP_ID,PRODUCTS_GROUP_NAME,IS_ACTIVE)VALUES('{id}','{cNAME}','{cNAME}',1)";
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
                else
                {
                    if (_tpl.Item1.IS_ACTIVE == 1)
                    {
                        sql = $"update VENDOR_PRODUCTS_GROUP set IS_ACTIVE=0 where VENDOR_ID='{id}' and PRODUCTS_GROUP_ID='{cNAME}'";
                    }
                    else
                    {
                        sql = $"update VENDOR_PRODUCTS_GROUP set IS_ACTIVE=1 where VENDOR_ID='{id}' and PRODUCTS_GROUP_ID='{cNAME}'";
                    }
                    return DatabaseMSSql.ExecuteSqlCommand(sql);
                }
            }

            else if (cID == "v-pro")
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
                    //AND T2.VENDOR_ID='{id}' AND T1.DOCUMENTS_TYPE='USER'";
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

        public static Tuple<List<VENDOR_PRODUCTS_GROUP>, EQResult> getVENDOR_PRODUCTS_GROUP(string id)
        {
            sql = $@"SELECT T1.ID PRODUCTS_GROUP_ID,T1.NAME PRODUCTS_GROUP_NAME,
                    CASE WHEN T2.IS_ACTIVE IS NULL
                    THEN 0
                    ELSE
                    T2.IS_ACTIVE
                    END IS_ACTIVE
                    FROM TNDR_PRODUCT_GROUP T1
                    LEFT JOIN VENDOR_PRODUCTS_GROUP T2 ON T1.ID = T2.PRODUCTS_GROUP_ID                   
                    AND T2.VENDOR_ID='{id}'";
            return DatabaseMSSql.SqlQuery<VENDOR_PRODUCTS_GROUP>(sql);
        }
        public static Tuple<List<VENDOR_FILE>, EQResult> getVENDOR_FILE_LIST(string _vendorid)
        {
            sql = $@"SELECT VF.*,NVL(TC.CERTIFICATE_NAME,TD.DOCUMENTS_NAME) DOC_NAME FROM VENDOR_FILE VF
                    LEFT JOIN  TNDR_CERTIFICATE TC ON TC.CERTIFICATE_ID=VF.DOC_ID AND VF.DOCUMENT_TYPE='Certificate'
                    LEFT JOIN  TNDR_DOCUMENTS TD ON TD.DOCUMENTS_ID=VF.DOC_ID AND VF.DOCUMENT_TYPE='Documents'
                    WHERE VF.IS_ACTIVE=1 AND VF.VENDOR_ID='{_vendorid}'";
            return DatabaseMSSql.SqlQuery<VENDOR_FILE>(sql);
        }
        public static EQResult profileUpdate(VENDOR_DETAILS _obj)
        {
            sql = $@"update VENDOR set VENDOR_USER_ID='{_obj.VENDOR_USER_ID}',CONTACT_NAME='{_obj.CONTACT_NAME}',NO_OF_EMPLOYEES='{_obj.NO_OF_EMPLOYEES}',
                CONTACT_NUMBER='{_obj.CONTACT_NUMBER}',ADDRESS_1='{_obj.ADDRESS_1}',ADDRESS_2='{_obj.ADDRESS_2}',YEAR_OF_ESTABLISHMENT='{_obj.YEAR_OF_ESTABLISHMENT}',
                YEARLY_TRUNOVER='{_obj.YEARLY_TRUNOVER}',TAX_NUMBER='{_obj.TAX_NUMBER}',TRADE_NUMBER='{_obj.TRADE_NUMBER}',
                PURCHASER_NOTIFY='{_obj.PURCHASER_NOTIFY}',SUPPLIER_NOTIFY='{_obj.SUPPLIER_NOTIFY}',PROFILE_IMAGE='{_obj.PROFILE_IMAGE}',
                CONTACT_NAME2='{_obj.CONTACT_NAME2}',CONTACT_NUMBER2='{_obj.CONTACT_NUMBER2}', CONTACT_NAME3='{_obj.CONTACT_NAME3}',CONTACT_NUMBER3='{_obj.CONTACT_NUMBER3}'
                where VENDOR_ID='{_obj.VENDOR_ID}'";
            return DatabaseMSSql.ExecuteSqlCommand(sql);
        }

        public static EQResult documentSave(VENDOR_FILE _obj)
        {
            sql = $@"INSERT INTO TND.VENDOR_FILE (
                   FILE_ID, VENDOR_ID, FILE_PATH,FILE_NUMBER, FILE_NAME, FILE_TYPE,DOC_ID, DOCUMENT_TYPE,CREATE_USER, CREATE_DEVICE)
                   VALUES ( q'[{_obj.FILE_ID}]', q'[{_obj.VENDOR_ID}]',q'[{_obj.FILE_PATH}]',q'[{_obj.FILE_NUMBER}]',q'[{_obj.FILE_NAME}]',q'[{_obj.FILE_TYPE}]',q'[{_obj.DOC_ID}]',q'[{_obj.DOCUMENT_TYPE}]',q'[{_obj.CREATE_USER}]',q'[{_obj.CREATE_DEVICE}]')";
            return DatabaseMSSql.ExecuteSqlCommand(sql);
        }
        public static Tuple<VENDOR_FILE, EQResult> filePath(string _fileId)
        {
            sql = $"SELECT * FROM VENDOR_FILE VF WHERE VF.FILE_ID='{_fileId}' AND IS_ACTIVE=1";
            return DatabaseMSSql.SqlQuerySingle<VENDOR_FILE>(sql);
        }

        public static Tuple<VENDOR_DETAILS, EQResult> getProfile(string id)
        {
            sql = $"select t.* from VENDOR t where t.VENDOR_ID='{id}'";
            Tuple<VENDOR_DETAILS, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_DETAILS>(sql);
            return _tpl;
        }
        public static Tuple<VENDOR_DETAILS, EQResult> checkUserId(string userId, string vendorId)
        {
            sql = $"select * from VENDOR  where LOWER( VENDOR_USER_ID ) ='{userId.ToLower()}' and VENDOR_ID <>('{vendorId}')";
            Tuple<VENDOR_DETAILS, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<VENDOR_DETAILS>(sql);
            return _tpl;
        }


        public static Tuple<List<VENDOR_DETAILS>, EQResult> supplierList()
        {
            string sql = $@"SELECT * FROM VENDOR WHERE SUPPLIER=1";
            var objList = DatabaseMSSql.SqlQuery<VENDOR_DETAILS>(sql);
            return objList;
        }

    }
}