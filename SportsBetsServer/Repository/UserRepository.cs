using System;
using System.Collections.Generic;
using System.Linq;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
            { }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await this.RepositoryContext.Set<User>().FindAsync(username);
        }
        public async Task<int> GetUserAvailableBalanceAsync(Guid id)
        {
            var user = await FindByGuid(id);

            var balance = (user == null) ? -1 : user.AvailableBalance;
            
            return balance;
        }
    }
}
