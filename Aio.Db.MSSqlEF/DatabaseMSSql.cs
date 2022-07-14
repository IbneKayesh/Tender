using Aio.Db.MSSqlEF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Aio.Db.MSSqlEF
{
    public static class DatabaseMSSql
    {
        public static EQResult ExecuteSqlCommand(string sql, params object[] parameters)
        {
            EQResult result = new EQResult();
            using (AioDbContext db = new AioDbContext())
            {
                using (DbContextTransaction trn = db.Database.BeginTransaction())
                {
                    try
                    {
                        result.ROWS = db.Database.ExecuteSqlCommand(sql, parameters);
                        db.SaveChanges();
                        trn.Commit();
                        result.SUCCESS = true;
                        result.MESSAGES = AppKeys.SUCCESS_MESSAGES;
                    }
                    catch (Exception ex)
                    {
                        result.SUCCESS = false;
                        result.MESSAGES = ex.Message;
                        result.ROWS = 0;
                        trn.Rollback();
                    }
                }
            }
            return result;
        }
        public static EQResult ExecuteSqlCommand(List<string> sqlList)
        {
            EQResult result = new EQResult();
            using (AioDbContext db = new AioDbContext())
            {
                using (DbContextTransaction trn = db.Database.BeginTransaction())
                {
                    try
                    {
                        int r = 0;
                        foreach (string sql in sqlList)
                        {
                            r += db.Database.ExecuteSqlCommand(sql);
                        }
                        db.SaveChanges();
                        trn.Commit();
                        result.ROWS = r;
                        result.SUCCESS = true;
                        result.MESSAGES = AppKeys.SUCCESS_MESSAGES;
                    }
                    catch (Exception ex)
                    {
                        result.SUCCESS = false;
                        result.MESSAGES = ex.Message;
                        result.ROWS = 0;
                        trn.Rollback();
                    }
                }
            }
            return result;
        }

        public static EQResult SqlQuery(string sql)
        {
            EQResult result = new EQResult();
            using (AioDbContext db = new AioDbContext())
            {
                try
                {
                    result.ROWS = db.Database.SqlQuery<int>(sql).SingleOrDefault();
                    result.SUCCESS = true;
                    result.MESSAGES = AppKeys.SUCCESS_MESSAGES;
                    return result;
                }
                catch (Exception ex)
                {
                    result.SUCCESS = false;
                    result.MESSAGES = ex.Message;
                    result.ROWS = 0;
                    return result;
                }
            }
        }

        public static Tuple<List<T>, EQResult> SqlQuery<T>(string sql, params object[] parameters)
        {
            EQResult result = new EQResult();
            using (AioDbContext db = new AioDbContext())
            {
                try
                {
                    var objList = db.Database.SqlQuery<T>(sql, parameters).ToList();
                    result.ROWS = objList.Count;
                    result.SUCCESS = true;
                    result.MESSAGES = AppKeys.SUCCESS_MESSAGES;
                    return new Tuple<List<T>, EQResult>(objList, result);
                }
                catch (Exception ex)
                {
                    result.SUCCESS = false;
                    result.MESSAGES = ex.Message;
                    result.ROWS = 0;
                    return new Tuple<List<T>, EQResult>(new List<T>(), result);
                }
            }
        }
        public static Tuple<T, EQResult> SqlQuerySingle<T>(string sql, params object[] parameters) where T : new()
        {
            EQResult result = new EQResult();
            using (AioDbContext db = new AioDbContext())
            {
                try
                {
                    var objList = db.Database.SqlQuery<T>(sql, parameters).ToList();
                    if (objList.Count > 0)
                    {
                        result.ROWS = objList.Count;
                        result.SUCCESS = true;
                        result.MESSAGES = AppKeys.SUCCESS_MESSAGES;
                        return new Tuple<T, EQResult>(objList.FirstOrDefault(), result);
                    }
                    result.ROWS = 0;
                    result.SUCCESS = false;
                    result.MESSAGES = AppKeys.NO_ROWS_FOUND;
                    return new Tuple<T, EQResult>(new T(), result);
                }
                catch (Exception ex)
                {
                    result.SUCCESS = false;
                    result.MESSAGES = ex.Message;
                    result.ROWS = 0;
                    return new Tuple<T, EQResult>(new T(), result);
                }
            }
        }
        public static Tuple<T, EQResult> SqlProcedureSingle<T>(string sql, params object[] parameters) where T : new()
        {
            EQResult result = new EQResult();
            using (AioDbContext db = new AioDbContext())
            {
                try
                {
                    var objList = db.Database.SqlQuery<T>(sql, parameters).ToList();
                    if (objList.Count > 0)
                    {
                        result.ROWS = objList.Count;
                        result.SUCCESS = true;
                        result.MESSAGES = AppKeys.SUCCESS_MESSAGES;
                        return new Tuple<T, EQResult>(objList.FirstOrDefault(), result);
                    }
                    result.ROWS = 0;
                    result.SUCCESS = false;
                    result.MESSAGES = AppKeys.NO_ROWS_FOUND;
                    return new Tuple<T, EQResult>(new T(), result);
                }
                catch (Exception ex)
                {
                    result.SUCCESS = false;
                    result.MESSAGES = ex.Message;
                    result.ROWS = 0;
                    return new Tuple<T, EQResult>(new T(), result);
                }
            }
        }
    }
}
