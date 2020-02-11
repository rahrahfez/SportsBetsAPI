using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Services
{
    public interface IUserService
    {
        bool UserExists(string username);
        User Map(User u1, User u2);
    }
}