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
        private readonly IUserRepository _repo;
        private readonly IAuthService _authService;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
            _authService = new AuthService();
        }
        public bool UserExists(string username)
        {
            var user = _repo.GetUserByUsername(username);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public User CreateUser(UserCredentials user)
        {
            string hashedPassword = _authService.CreatePasswordHash(user.Password);
            User createdUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                HashedPassword = hashedPassword,
                UserRole = "User"
             };
            
            return createdUser;
        }
        public void UpdateUserBalance(User user, int newBalance)
        {
            user.AvailableBalance = newBalance;
        }
    }
}