using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;
using SportsBetsServer.Contracts.Services;

namespace SportsBetsAPI.Tests.Services
{
    public class UserServiceTest
    {
        public UserServiceTest()
        {
        }
        [Fact]
        public async void UserService_CheckForExistingUsername_ReturnsNullIfTrue()
        {
            string username = "tester";
            var ServiceMock = new Mock<IUserService>();
            ServiceMock.Setup(x => x.UserExists(It.IsAny<string>())).Returns(Task.FromResult(true));
            var userService = ServiceMock.Object;

            var userExists = await userService.UserExists(username);

            ServiceMock.VerifyAll();
            Assert.True(userExists);
        }

        [Fact]
        public void UserService_CreateUser()
        {
            var userCredentials = new UserCredentials
            {
                Username = "tester",
                Password = "_"
            };

            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "tester",
                AvailableBalance = 100,
                DateCreated = DateTime.UtcNow,
                UserRole = "User",
                HashedPassword = "_"
            };

            var ServiceMock = new Mock<IUserService>();
            ServiceMock.Setup(x => x.CreateUser(It.IsAny<UserCredentials>())).Returns(testUser);
            var userService = ServiceMock.Object;

            var user = userService.CreateUser(userCredentials);
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