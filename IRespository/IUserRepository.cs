using DataModels;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByName(string name);
    }
}
