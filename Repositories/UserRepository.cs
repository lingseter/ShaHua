using System;
using System.Threading.Tasks;
using DataModels;
using IRepositories;
using System.Data;
using System.Data.SqlClient;
using Utility;
using Dapper;

namespace Repositories
{
    public class UserRepository : DataRepository<User>, IUserRepository
    {
        public UserRepository()
        {
            TableName = "User";
        }

        public Task<User> GetByName(string name)
        {
            string storeProc = "GetByName";
            try
            {
                using (IDbConnection db = new SqlConnection(WebConfig.ConnectionString))
                {
                    db.Open();
                    return db.QueryFirstOrDefaultAsync<User>(storeProc, new { TableName, name }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(string.Format("Find Fail with params:tableName={0},name={1},storeProc={2}", TableName, name, storeProc), ex);
                return Task.FromResult(default(User));//如果T是引用类型返回null,如果是值类型返回0
            }
        }
    }
}
