using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;

namespace SportsBetsServer.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        User CreateUser(UserCredentials user);
        Task<User> GetUserByUsernameAsync(string username);
    }
}