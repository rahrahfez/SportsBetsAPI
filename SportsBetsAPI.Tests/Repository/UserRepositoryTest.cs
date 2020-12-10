using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Moq;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities.Models;

namespace SportsBetsAPI.Tests.Repository
{
    public class UserRepositoryTest
    {
        public UserRepositoryTest() { }
        [Fact]
        public async void User_AddUser_VerifyCreation()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };
            var RepoMock = new Mock<IRepositoryWrapper>();
            RepoMock.Setup(u => u.User.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask).Verifiable();
            var repo = RepoMock.Object;

            await repo.User.AddAsync(user);

            RepoMock.VerifyAll();
        }

        [Fact]
        public async void User_Get_All()
        {
            var listOfUsers = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Tester1",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now,
                    UserRole = "User"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Tester2",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now,
                    UserRole = "User"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Tester3",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now,
                    UserRole = "User"
                }
            };

            var RepoMock = new Mock<IRepositoryWrapper>();
            RepoMock.Setup(x => x.User.GetAllUsersAsync()).Returns(Task.FromResult<IEnumerable<User>>(listOfUsers)).Verifiable();
            var repo = RepoMock.Object;

            var userlist = await repo.User.GetAllUsersAsync();

            RepoMock.VerifyAll();
            Assert.Equal(3, userlist.Count());
        }

        [Fact]
        public async void User_FindBy_Username()
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "tester",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };

            var RepoMock = new Mock<IRepositoryWrapper>();
            RepoMock.Setup(x => x.User.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(newUser)).Verifiable();
            var repo = RepoMock.Object;

            var user = await repo.User.GetUserByUsernameAsync("tester");

            RepoMock.VerifyAll();
            Assert.Equal("tester", user.Username);
        }

        [Fact]
        public async void User_FindUserById()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };

            var RepoMock = new Mock<IRepositoryWrapper>();
            RepoMock.Setup(x => x.User.GetUserByGuidAsync(It.IsAny<Guid>())).Returns(Task.FromResult(user)).Verifiable();
            var repo = RepoMock.Object;

            var testuser = await repo.User.GetUserByGuidAsync(user.Id);

            RepoMock.VerifyAll();
            Assert.NotNull(testuser);
        }

        [Fact]
        public async void User_GetAvailableBalance_ById()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };            

            var RepoMock = new Mock<IRepositoryWrapper>();
            RepoMock.Setup(x => x.User.GetUserAvailableBalanceAsync(It.IsAny<Guid>())).Returns(Task.FromResult(user.AvailableBalance));
            var repo = RepoMock.Object;

            var balance = await repo.User.GetUserAvailableBalanceAsync(user.Id);

            RepoMock.VerifyAll();
            Assert.Equal(100, balance);
        }
        [Fact]
        public void User_RemovesUser()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };

            var RepoMock = new Mock<IRepositoryWrapper>();
            RepoMock.Setup(x => x.User.Remove(It.IsAny<User>())).Verifiable();
            var repo = RepoMock.Object;

            repo.User.Remove(user);

            RepoMock.VerifyAll();
        }
    }
}