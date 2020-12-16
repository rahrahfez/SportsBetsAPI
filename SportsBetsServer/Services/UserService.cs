using System;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities.Models.Extensions;
using SportsBetsServer.Entities;

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
            User createdUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                AvailableBalance = 100,
                Role = Role.User
             };
            
            return createdUser;
        }
        public void UpdateUserBalance(User user, int newBalance)
        {
            user.AvailableBalance = newBalance;
        }
    }
}