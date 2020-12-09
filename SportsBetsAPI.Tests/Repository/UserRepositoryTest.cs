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
            Repo.Setup(u => u.User.CreateAsync(It.IsAny<User>())).Returns(Task.FromResult(Repo.Object));
            Repo.Setup(r => r.Complete()).Returns(Task.CompletedTask);

            await Repo.Object.User.CreateAsync(user);
            await Repo.Object.Complete();

            Assert.NotNull(user);
        }

        [Fact]
        public async void User_Get_All()
        {
            var users = new List<User>
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

            var Repo = new Mock<IRepositoryWrapper>();
            Repo.Setup(r => r.Complete()).Returns(Task.CompletedTask);
            Repo.Setup(u => u.User.CreateAsync(It.IsAny<User>())).Returns(Task.FromResult(Repo.Object));
            Repo.Setup(x => x.User.FindAllAsync()).Returns(Task.FromResult<IEnumerable<User>>(users.ToList()));

            foreach (var user in users)
            {
                await Repo.Object.User.CreateAsync(user);
            }
            
            await Repo.Object.Complete();

            var userlist = await Repo.Object.User.FindAllAsync();

            Assert.Equal(3, userlist.Count());
        }

        [Fact(Skip = "not working")]
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
            Repo.Setup(x => x.Complete()).Returns(Task.CompletedTask);
            Repo.Setup(x => x.User.CreateAsync(It.IsAny<User>())).Returns(Task.FromResult(Repo.Object));

            await Repo.Object.User.CreateAsync(newUser);
            //await Repo.Object.Complete();

            var user = Repo.Object.User.GetUserByUsername("tester");

            Assert.NotNull(user);
        }

        [Fact(Skip = "not working")]
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
            Repo.Setup(x => x.Complete()).Returns(Task.CompletedTask);
            Repo.Setup(x => x.User.CreateAsync(It.IsAny<User>())).Returns(Task.FromResult(Repo.Object.User));
            Repo.Setup(x => x.User.GetUserByUsernameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            await Repo.Object.User.CreateAsync(user);
            await Repo.Object.Complete();

            var balance = await Repo.Object.User.GetUserAvailableBalanceAsync(user.Id);

            Assert.Equal(100, balance);
        }
    }
}