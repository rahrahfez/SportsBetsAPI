using System;
using System.Collections.Generic;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsBetsServer.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
            { }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await RepositoryContext.User.ToListAsync();
        }
        public async Task<User> GetUserByGuidAsync(Guid id)
        {
            return await RepositoryContext.User
                .Where(u => u.Id.Equals(id)).SingleOrDefaultAsync();
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await RepositoryContext.User
                .Where(u => u.Username.Equals(username)).SingleOrDefaultAsync();
        }
        public User GetUserByUsername(string username)
        {
            return RepositoryContext.User
                .Where(u => u.Username.Equals(username)).SingleOrDefault();
        }
        public int GetUserAvailableBalanceAsync(Guid id)
        {
            var user = RepositoryContext.User
                .Where(u => u.Id.Equals(id)).SingleOrDefault();

            var balance = (user == null) ? -1 : user.AvailableBalance;
            
            return balance;
        }
    }
}
