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
        public void UserService_CheckForExistingUsername_ReturnsNullIfTrue()
        {
            string username = "tester";
            var ServiceMock = new Mock<IUserService>();
            ServiceMock.Setup(x => x.UserExists(It.IsAny<string>())).Returns(true);
            var userService = ServiceMock.Object;

            var userExists = userService.UserExists(username);

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

            var id = Guid.NewGuid();
            var dateCreated = DateTime.Now;
            var availableBalance = 100;

            var testUser = new User
            {
                Id = id,
                Username = "tester",
                AvailableBalance = availableBalance,
                DateCreated = dateCreated,
                UserRole = "User",
                HashedPassword = "_"
            };

            var ServiceMock = new Mock<IUserService>();
            ServiceMock.Setup(x => x.CreateUser(It.IsAny<UserCredentials>())).Returns(testUser);
            var userService = ServiceMock.Object;

            var user = userService.CreateUser(userCredentials);

            Assert.NotNull(user);
            Assert.Equal("tester", user.Username);
            Assert.Equal(availableBalance, user.AvailableBalance);
            Assert.Equal(id, user.Id);
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