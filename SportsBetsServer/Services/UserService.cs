using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Services
{
    public class UserService : IUserService
    {
        public bool UserExists(string username)
        {
            // TODO: Figure out where to put this function, in the service or repo?
            return false;
        }
        public User Map(User u1, User u2)
        {
            u1.Id = u2.Id;
            u1.Username = u2.Username;
            u1.AvailableBalance = u2.AvailableBalance;
            return u1;
        }
    }
}