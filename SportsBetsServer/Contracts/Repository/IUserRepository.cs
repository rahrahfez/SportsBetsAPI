using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByGuidAsync(Guid id);
        Task<int> GetUserAvailableBalanceAsync(Guid id);
    }
}