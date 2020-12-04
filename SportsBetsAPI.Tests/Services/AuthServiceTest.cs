using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Services;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;
using System.IO;

namespace SportsBetsAPI.Tests.Services
{
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;
        public AuthServiceTest()
        {
            _authService = new AuthService();

        }
        [Fact]
        public void CreatePasswordHashTest()
        {
            UserCredentials testUser = new UserCredentials()
            {
                Username = "Tester",
                Password = "password"
            };

            string hashedPassword = _authService.CreatePasswordHash(testUser.Password);

            Assert.NotNull(hashedPassword);

        }
        [Fact]
        public void VerifyPasswordTest()
        {
            UserCredentials testUser = new UserCredentials()
            {
                Username = "Tester",
                Password = "password"
            };

            string hashedPassword = _authService.CreatePasswordHash(testUser.Password);

            var result = _authService.VerifyPassword(testUser.Password, hashedPassword);

            Assert.True(result);
        }
        [Fact]
        public void CreateJsonTokenTest()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string hashedPassword = _authService.CreatePasswordHash("password");

            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                HashedPassword = hashedPassword,
                UserRole = "User"
            };

            var token = _authService.CreateJsonToken(config, user);

            Assert.NotNull(token);
        }
    }
}
