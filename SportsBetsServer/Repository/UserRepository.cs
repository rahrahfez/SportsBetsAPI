using System;
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
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await RepositoryContext.User
                .Where(u => u.Username.Equals(username)).FirstOrDefaultAsync();

        }
        public User GetUserByUsername(string username)
        {
            return RepositoryContext.User
                .Where(u => u.Username.Equals(username)).SingleOrDefault();
        }
        public async Task<int> GetUserAvailableBalanceAsync(Guid id)
        {
            var user = await FindByGuidAsync(id);

            var balance = (user == null) ? -1 : user.AvailableBalance;
            
            return balance;
        }
    }
}
