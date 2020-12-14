using System.Linq;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Repository
{
    public static class UserRepository
    { 
        public static User GetUserByUsername(this IRepositoryBase<User> users, string username)
        {
            return users.GetAll().Where(u => u.Username.Equals(username)).SingleOrDefault();
        }
    }
}
