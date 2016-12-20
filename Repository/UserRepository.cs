using System;
using System.Collections.Generic;
using DataModels;
using IRepository;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private static string tableName = "User";
        private static string storeProc = "GetPagingList";
        public Task<bool> AddAsync(User user)
        {
            bool result = DataRepository.Add(user) > 0;
            return Task.FromResult(result);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            bool result = DataRepository.Delete(tableName, id) > 0;
            return Task.FromResult(result);
        }

        public Task<User> FindByIdAsync(Guid id)
        {
            User user = DataRepository.Find<User>(tableName, id);
            return Task.FromResult(user);

        }

        public Task<List<User>> GetList(string filter = null, int start = 0, int pageLimit = 10)
        {
            List<User> userList = DataRepository.GetList<User>(storeProc, tableName, filter, start, pageLimit);
            return Task.FromResult(userList);
        }

        public Task<bool> UpdateAsync(User user)
        {
            bool result = DataRepository.Update(user) > 0;
            return Task.FromResult(result);
        }

        public Task<User> FindByName(string name)
        {
            string filter = string.Concat("name='", name, "'");
            User user = DataRepository.Find<User>(tableName, filter);
            return Task.FromResult(user);
        }
    }
}
