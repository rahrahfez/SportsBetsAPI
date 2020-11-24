using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;

namespace SportsBetsServer.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        User Map(User u1, User u2);
        User CreateUser(UserCredentials user);
    }
}