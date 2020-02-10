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
            {
                
            }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await FindAll().ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await FindByCondition(user => user.Id.Equals(id))
                .DefaultIfEmpty(new User())
                .SingleOrDefaultAsync();
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await FindByCondition(user => user.Username.Equals(username))
                .SingleOrDefaultAsync();
        }
        public async Task<int> GetUserAvailableBalanceAsync(Guid id)
        {
            return await FindByCondition(u => u.Id.Equals(id))
                .Select(user => user.AvailableBalance)
                .FirstOrDefaultAsync();
        }
        public async Task CreateUserAsync(User user)
        {
            Create(user);
            await SaveAsync();
        }
        public async Task UpdateUserAsync(User dbUser, User user)
        {
            Update(user);
            await SaveAsync();
        }
        public async Task DeleteUserAsync(User user)
        {
            Delete(user);
            await SaveAsync();
        }
    }
}
