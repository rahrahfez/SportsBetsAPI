using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
        User GetUserByUsername(string username);
        Task<int> GetUserAvailableBalanceAsync(Guid id);
    }
}