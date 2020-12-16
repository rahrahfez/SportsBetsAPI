using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities.Models.Extensions;

namespace SportsBetsServer.Contracts.Services
{
    public interface IUserService
    {
        User CreateUser(UserCredentials user);
        void UpdateUserBalance(User user, int newBalance);
    }
}