using System;
using Moq;
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
            var config = new Mock<IConfiguration>();
            var configSection = new Mock<IConfigurationSection>();

            configSection.Setup(t => t.Value).Returns("testValue");
            config.Setup(t => t.GetSection("AppSettings:Token")).Returns(configSection.Object);
            configSection.Setup(t => t.Value).Returns("https://localhost:5000/");
            config.Setup(t => t.GetSection("Jwt:Issuer")).Returns(configSection.Object);
            config.Setup(t => t.GetSection("Jwt:Audience")).Returns(configSection.Object);

            _authService = new AuthService(config.Object);

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

            var token = _authService.CreateJsonToken(user);

            Assert.NotNull(token);
        }
    }
}
