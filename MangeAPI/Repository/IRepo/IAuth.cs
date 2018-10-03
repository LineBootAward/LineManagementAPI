using System.Threading.Tasks;
using MangeAPI.Model;

namespace MangeAPI.Repository.IRepo
{
    interface IAuth
    {
        Task<int> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}