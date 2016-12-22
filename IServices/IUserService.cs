using System.Threading.Tasks;
using ViewModels;

namespace IServices
{
    public interface IUserService : IService<User>
    {
        Task<User> GetByName(string Name);
    }
}
