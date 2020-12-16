using System.Linq;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Repository
{
    public static class AccountRepository 
    { 
        public static Account GetUserByUsername(this IRepositoryBase<Account> account, string username)
        {
            return account.GetAll().Where(u => u.Username.Equals(username)).SingleOrDefault();
        }
    }
}
