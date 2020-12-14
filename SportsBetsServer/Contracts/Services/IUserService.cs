using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;

namespace SportsBetsServer.Contracts.Services
{
    public interface IUserService
    {
        bool UserExists(string username);
        User CreateUser(UserCredentials user);
        void UpdateUserBalance(User user, int newBalance);
    }
}