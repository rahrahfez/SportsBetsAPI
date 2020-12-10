using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Repository;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;

namespace SportsBetsAPI.Tests.Repository
{
    public class UserRepositoryTest
    {
        public UserRepositoryTest() { }
        [Fact]
        public async void User_CreateAndInsert_VerifyCreation()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };
            var Repo = new Mock<IRepositoryWrapper>();
            Repo.Setup(u => u.User.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask).Verifiable();
            Repo.Setup(r => r.Complete()).Returns(Task.CompletedTask);

            await Repo.Object.User.AddAsync(user);
            await Repo.Object.Complete();

            Repo.VerifyAll();
        }

        [Fact]
        public async void User_Get_All()
        {
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };
            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester2",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };
            var user3 = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester3",
                AvailableBalance = 100,
                DateCreated = DateTime.Now,
                UserRole = "User"
            };
            var listOfUsers = new List<User>
            {
                user1,
                user2,
                user3
            };

            var Repo = new Mock<IRepositoryWrapper>();
            Repo.Setup(r => r.Complete()).Returns(Task.CompletedTask);
            Repo.Setup(u => u.User.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask).Verifiable();
            Repo.Setup(x => x.User.GetAllUsersAsync()).Returns(Task.FromResult<IEnumerable<User>>(listOfUsers)).Verifiable();

            await Repo.Object.User.AddAsync(user1);
            await Repo.Object.User.AddAsync(user2);
            await Repo.Object.User.AddAsync(user3);
            await Repo.Object.Complete();

            var userlist = await Repo.Object.User.GetAllUsersAsync();

            Repo.VerifyAll();

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

            var Repo = new Mock<IRepositoryWrapper>();
            Repo.Setup(x => x.User.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(newUser)).Verifiable();

            var user = await Repo.Object.User.GetUserByUsernameAsync("tester");

            Repo.VerifyAll();
            Assert.Equal("tester", user.Username);
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

            var Repo = new Mock<IRepositoryWrapper>();
            Repo.Setup(x => x.User.GetUserAvailableBalanceAsync(It.IsAny<Guid>())).Returns(Task.FromResult(user.AvailableBalance));

            var balance = await Repo.Object.User.GetUserAvailableBalanceAsync(user.Id);

            Repo.VerifyAll();
            Assert.Equal(100, balance);
        }
    }
}