using System;
using System.Collections.Generic;
using DataModels;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetList(string filter = null, int start = 0, int pageLimit = 10);

        Task<User> FindByName(string name);

        Task<bool> AddAsync(User user);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(Guid id);

        Task<User> FindByIdAsync(Guid id);

    }
}
