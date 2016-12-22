using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Utility;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IRepositories;

namespace Repositories
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        public string TableName { get; set; }

        public Task<T> GetById(string id)
        {
            string storeProc = "GetById";
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    return db.QueryFirstOrDefaultAsync<T>(storeProc, new { TableName, id }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("Find Fail with params:tableName={0},id={1},storeProc={2}", TableName, id, storeProc), ex);
                return Task.FromResult(default(T));//如果T是引用类型返回null,如果是值类型返回0
            }
        }

        public Task<IEnumerable<T>> GetList(string filter = null, int start = 0, int pageLimit = 10)
        {
            string storeProc = "GetPagingList";
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    return db.QueryAsync<T>(storeProc, new { TableName, filter, start, pageLimit }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("GetList Fail with params:storeProce={0},tableName={1},filter={2}", storeProc, TableName, filter), ex);
                return Task.FromResult(default(IEnumerable<T>));//如果T是引用类型返回null,如果是值类型返回0
            }
        }

        public Task<bool> Add(T entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO " + TableName + " SET");
                    GetEntityString(entity, sb);
                    sb.Append(" WHERE Id = @Id");
                    bool result = db.ExecuteAsync(sb.ToString(), entity).Result > 0;
                    return Task.FromResult(result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Add Fail with param:t=" + entity.ToString(), ex);
                return Task.FromResult(false);
            }
        }

        public Task<bool> AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            string storeProc = "DeleteById";
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    bool result = db.ExecuteAsync(storeProc, new { TableName, id }, commandType: CommandType.StoredProcedure).Result > 0;
                    return Task.FromResult(result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("Delete Fail with params:tableName={0},id={1},storeProc={2}", TableName, id, storeProc), ex);
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteRange(IEnumerable<string> idList)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE " + TableName + " SET");
                    GetEntityString(entity, sb);
                    sb.Append(" WHERE Id = @Id");
                    bool result = db.ExecuteAsync(sb.ToString(), entity).Result > 0;
                    return Task.FromResult(result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException("Update Fail with param:t=" + entity.ToString(), ex);
                return Task.FromResult(false);
            }
        }

        private static void GetEntityString(T entity, StringBuilder sb)
        {
            var type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo sP in properties)
            {
                if (sP.GetValue(entity) != null)
                    sb.Append(" " + sP.Name + "=@" + sP.Name);
            }
        }
    }
}
