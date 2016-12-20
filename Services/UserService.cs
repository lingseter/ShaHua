using System.Collections.Generic;
using IServices;
using ViewModels;
using Utility;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private IRepository.IUserRepository iUserRepository;

        public UserService(IRepository.IUserRepository iUserRepository)
        {
            this.iUserRepository = iUserRepository;
        }

        public Task<bool> AddAsync(User user)
        {
            return iUserRepository.AddAsync(Common.Mapper<DataModels.User, User>(user));
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return iUserRepository.DeleteAsync(id);
        }

        public Task<User> FindByIdAsync(Guid id)
        {
            DataModels.User userData = iUserRepository.FindByIdAsync(id).Result;
            if (userData != null)
            {
                User user = Common.Mapper<User, DataModels.User>(iUserRepository.FindByIdAsync(id).Result);
                return Task.FromResult(user);
            }
            return Task.FromResult<User>(null);
        }

        public Task<User> FindByName(string name)
        {
            DataModels.User userData = iUserRepository.FindByName(name).Result;
            if (userData != null)
            {
                User user = Common.Mapper<User, DataModels.User>(userData);
                return Task.FromResult(user);
            }
            return Task.FromResult<User>(null);
        }

        public Task<List<User>> GetList(string filter = null, int start = 0, int pageLimit = 10)
        {
            List<User> userList = null;
            List<DataModels.User> userDataList = iUserRepository.GetList(filter, start, pageLimit).Result;
            if (userDataList != null && userDataList.Count > 0)
            {
                userList = new List<User>();
                foreach (var user in userDataList)
                {
                    userList.Add(Common.Mapper<User, DataModels.User>(user));
                }
            }
            return Task.FromResult(userList);
        }

        public Task<bool> UpdateAsync(User user)
        {
            return iUserRepository.UpdateAsync(Common.Mapper<DataModels.User, User>(user));
        }
    }
}
