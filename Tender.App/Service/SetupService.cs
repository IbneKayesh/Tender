using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using Tender.Models.Models;

namespace Tender.App.Service
{
    public class SetupService
    {
        static string sql = "";
        
        public static EQResult saveItem(TNDR_PRODUCTS _obj)
        {
            var ProductId = CommonService.productId("PRODUCTS_ID");
            List<string> sqlList = new List<string>();

            sqlList.Add( $@"INSERT INTO TND.TNDR_PRODUCTS (PRODUCTS_ID, UNIT,PRODUCTS_NAME,IMAGE_PATH,GROUP_ID)VALUES   ('{ProductId}','{_obj.UNIT}','{_obj.PRODUCTS_NAME}' ,'{_obj.IMAGE_PATH}','{_obj.GROUP_ID}')");
            sqlList.Add($@"UPDATE TABLE_MAX_ID SET MAX_ID=MAX_ID+1 WHERE TABLE_NAME='PRODUCTS_ID'");

            return DatabaseMSSql.ExecuteSqlCommand(sqlList);
        }

        public static Tuple<TNDR_PRODUCTS, EQResult> checkItem(string productName)
        {
            sql = $" SELECT * FROM TNDR_PRODUCTS WHERE LOWER (PRODUCTS_NAME)='{productName.ToLower()}'";
            Tuple<TNDR_PRODUCTS, EQResult> _tpl = DatabaseMSSql.SqlQuerySingle<TNDR_PRODUCTS>(sql);
            return _tpl;
        }

        public static Tuple<List<TNDR_PRODUCTS>, EQResult> getAllproduct(string companyId)
        {
            string sql = $@" SELECT   P.PRODUCTS_ID,
         P.UNIT,
         P.PRODUCTS_NAME,
         NVL (P.IMAGE_PATH, '/App_Asset/dist/img/noImage.jpg') IMAGE_PATH,
         PG.NAME GROUP_ID
        FROM      TNDR_PRODUCTS P
          JOIN TNDR_PRODUCT_GROUP
        PG ON PG.ID = P.GROUP_ID WHERE PG.COMPANY_ID='{companyId}'";
            var objList = DatabaseMSSql.SqlQuery<TNDR_PRODUCTS>(sql);
            return objList;
        }
        public static Tuple<List<TNDR_PRODUCT_GROUP>, EQResult> getpProductGroup(string companyId)
        {
            string sql = $"SELECT * FROM TNDR_PRODUCT_GROUP WHERE COMPANY_ID='{companyId}'";
            return DatabaseMSSql.SqlQuery<TNDR_PRODUCT_GROUP>(sql);
        }
    }
}