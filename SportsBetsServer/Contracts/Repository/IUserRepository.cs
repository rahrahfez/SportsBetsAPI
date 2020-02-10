using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<int> GetUserAvailableBalanceAsync(Guid id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User dbUser, User user);
        Task DeleteUserAsync(User user);
    }
}