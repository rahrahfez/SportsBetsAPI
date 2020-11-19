using System;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;

namespace SportsBetsServer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repo;
        public UserService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public async Task<bool> UserExists(string username)
        {
            var user = await _repo.User.GetUserByUsernameAsync(username);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public User Map(User u1, User u2)
        {
            u1.Id = u2.Id;
            u1.Username = u2.Username;
            u1.AvailableBalance = u2.AvailableBalance;
            return u1;
        }
        public async Task<User> CreateUserAsync(UserCredentials user)
        {
            User createdUser = new User()
                {
                    Id = Guid.NewGuid(), 
                    Username = user.Username,
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now,
                };

            await _repo.User.CreateAsync(createdUser);

            return createdUser;
        }
    }
}