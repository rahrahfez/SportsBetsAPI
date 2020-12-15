using System;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;
using SportsBetsServer.Repository;

namespace SportsBetsServer.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        public UserService(IConfiguration config)
        {
            _authService = new AuthService(config);
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