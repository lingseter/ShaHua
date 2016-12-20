using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Utility;
using System.Reflection;
using System.Text;

namespace Repository
{
    public class DataRepository
    {
        public static List<T> GetList<T>(string storeProc, string tableName, string filter = "", int start = 0, int pageLimit = 10)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    return db.Query<T>(storeProc, new { tableName, filter, start, pageLimit }, commandType: CommandType.StoredProcedure).AsList();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("GetList Fail with params:storeProce={0},tableName={1},filter={2}", storeProc, tableName, filter), ex);
                return null;
            }
        }

        public static T Find<T>(string tableName, Guid id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    string queryString = string.Format("SELECT * FROM {0} WHERE Id = @Id", tableName);
                    return db.QuerySingleOrDefault<T>(queryString, new { id });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("Find Fail with params:tableName={0},id={1}", tableName, id.ToString()), ex);
                return default(T);//如果T是引用类型返回null,如果是值类型返回0
            }
        }

        public static T Find<T>(string tableName, string filter)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    string queryString = string.Format("SELECT * FROM {0} WHERE {1}", tableName, filter);
                    return db.QueryFirstOrDefault<T>(queryString);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("Find Fail with params:tableName={0},filter={1}", tableName, filter), ex);
                return default(T);
            }
        }

        public static int Update<T>(T t)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    string tableName = t.ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE " + tableName + " SET");
                    var type = t.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo sP in properties)
                    {
                        if (sP.GetValue(t) != null)
                            sb.Append(" " + sP.Name + "=@" + sP.Name);
                    }
                    sb.Append(" WHERE Id = @Id");
                    int rowsAffected = db.Execute(sb.ToString(), t);
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Update Fail with param:t=" + t.ToString(), ex);
                return -1;
            }
        }

        public static int Add<T>(T t)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    string tableName = t.ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO " + tableName + " SET");
                    var type = t.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo sP in properties)
                    {
                        if (sP.GetValue(t) != null)
                            sb.Append(" " + sP.Name + "=@" + sP.Name);
                    }
                    sb.Append(" WHERE Id = @Id");
                    int rowsAffected = db.Execute(sb.ToString(), t);
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Add Fail with param:t=" + t.ToString(), ex);
                return -1;
            }
        }

        public static int Delete(string tableName, Guid id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    string queryString = string.Format("DELETE FROM {0} WHERE id ='{1}'", tableName, id);
                    int rowsAffected =  db.Execute(queryString);
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("Delete Fail with params:tableName={0},id={1}", tableName, id.ToString()), ex);
                return -1;
            }
        }
    }
}
