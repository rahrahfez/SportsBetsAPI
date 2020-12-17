using System;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Services;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;

namespace SportsBetsAPI.Tests.Services
{
    public class AccountServiceTest
    {
        private readonly IAccountService _accountService;
        public AccountServiceTest()
        {
            var config = new Mock<IConfiguration>();
            var configSection = new Mock<IConfigurationSection>();

            configSection.Setup(t => t.Value).Returns("testValue");
            config.Setup(t => t.GetSection("AppSettings:Token")).Returns(configSection.Object);
            configSection.Setup(t => t.Value).Returns("https://localhost:5000/");
            config.Setup(t => t.GetSection("Jwt:Issuer")).Returns(configSection.Object);
            config.Setup(t => t.GetSection("Jwt:Audience")).Returns(configSection.Object);

            _accountService = new AccountService(config.Object);

        }
        [Fact]
        public void AccountService_CreatePasswordHashTest()
        {
            string password = "password";
            string hashedPassword = _accountService.CreatePasswordHash(password);
            
            Assert.NotNull(hashedPassword);
        }
        [Fact]
        public void AccountService_VerifyPasswordTest_ReturnsBoolean()
        {
            string password = "password";
            string hashedPassword = _accountService.CreatePasswordHash(password);
            var result = _accountService.VerifyPassword(password, hashedPassword);

            Assert.True(result);

            password = "wrong";
            result = _accountService.VerifyPassword(password, hashedPassword);

            Assert.False(result);
        }
        [Fact]
        public void AccountService_CreateJsonTokenTest()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester",
                AvailableBalance = 100,
                Role = Role.User
            };

            var token = _accountService.CreateJsonToken(user);

            Assert.NotNull(token);
        }
        [Fact]
        public void AccountService_ValidateJsonToken_ReturnsBoolean()
        {

        }
    }
}
