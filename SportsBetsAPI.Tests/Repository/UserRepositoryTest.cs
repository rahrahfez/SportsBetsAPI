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
        public void User_AddUser_VerifyCreation()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };
            var RepoMock = new Mock<IUserRepository>();
            RepoMock.Setup(u => u.Add(It.IsAny<User>())).Verifiable();
            var repo = RepoMock.Object;

            repo.Add(user);

            RepoMock.VerifyAll();
        }

        [Fact]
        public async void User_AddUserAsync_VerifyCreation()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };
            var RepoMock = new Mock<IUserRepository>();
            RepoMock.Setup(u => u.AddAsync(It.IsAny<User>())).Verifiable();
            var repo = RepoMock.Object;

            await repo.AddAsync(user);

            RepoMock.VerifyAll();
        }

        [Fact]
        public void User_Get_All()
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

            var RepoMock = new Mock<IUserRepository>();
            RepoMock.Setup(x => x.GetAll()).Returns(listOfUsers).Verifiable();
            var repo = RepoMock.Object;

            var userlist = repo.GetAll();

            RepoMock.VerifyAll();
            Assert.Equal(3, userlist.Count());
        }

        [Fact]
        public void User_FindBy_Username()
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "tester",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };

            var RepoMock = new Mock<IUserRepository>();
            RepoMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(newUser).Verifiable();
            var repo = RepoMock.Object;

            var user = repo.GetUserByUsername("tester");

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

            var RepoMock = new Mock<IUserRepository>();
            RepoMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(user)).Verifiable();
            var repo = RepoMock.Object;

            var testuser = await repo.GetAsync(user.Id);

            RepoMock.VerifyAll();
            Assert.NotNull(testuser);
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

            var RepoMock = new Mock<IUserRepository>();
            RepoMock.Setup(x => x.Remove(It.IsAny<User>())).Verifiable();
            var repo = RepoMock.Object;

            repo.Remove(user);

            RepoMock.VerifyAll();
        }
    }
}