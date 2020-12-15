using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Services;

namespace SportsBetsAPI.Tests.Services
{
    public class UserServiceTest
    {
        private readonly IUserService _service;
        public UserServiceTest()
        {
            var config = new Mock<IConfiguration>();
            _service = new UserService(config.Object);
        }

        [Fact]
        public void UserService_CreateUser()
        {
            var userCredentials = new UserCredentials
            {
                Username = "tester",
                Password = "_"
            };

            var availableBalance = 100;

            var user = _service.CreateUser(userCredentials);

            Assert.NotNull(user);
            Assert.Equal("tester", user.Username);
            Assert.Equal(availableBalance, user.AvailableBalance);
            Assert.Equal("User", user.UserRole);            
        }

        [Fact]
        public void UserService_GetUserByUsername()
        {

        }
        [Fact]
        public void UserService_UpdateUserBalance()
        {

        }
    }
}