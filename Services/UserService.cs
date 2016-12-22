using System.Collections.Generic;
using IServices;
using ViewModels;
using Utility;
using System;
using System.Threading.Tasks;
using IRepositories;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserRepository iUserRepository;

        public UserService(IUserRepository iUserRepository)
        {
            this.iUserRepository = iUserRepository;
        }

        public Task<bool> Add(User user)
        {
            return iUserRepository.Add(Common.Mapper<DataModels.User, User>(user));
        }

        public Task<bool> Delete(string id)
        {
            return iUserRepository.Delete(id);
        }

        public Task<User> GetById(string id)
        {
            DataModels.User userData = iUserRepository.GetById(id).Result;
            if (userData != null)
            {
                User user = Common.Mapper<User, DataModels.User>(iUserRepository.GetById(id).Result);
                return Task.FromResult(user);
            }
            return Task.FromResult<User>(null);
        }

        public Task<User> GetByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetList(string filter = null, int start = 0, int pageLimit = 10)
        {
            List<User> userList = null;
            List<DataModels.User> userDataList = iUserRepository.GetList(filter, start, pageLimit).Result as List<DataModels.User>;
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

        public Task<bool> Update(User user)
        {
            return iUserRepository.Update(Common.Mapper<DataModels.User, User>(user));
        }
    }
}
