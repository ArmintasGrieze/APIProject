using ProjectAPI.Models;
using System.Threading.Tasks;

namespace ProjectAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> Register(User user);

        Task<User> GetUserByUsername(User user);
    }
}
