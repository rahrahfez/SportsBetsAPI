using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;

namespace SportsBetsAPI.UnitTests.Repository
{
    public class UserRepositoryTest
    {
        [Fact]
        public async void GetAllUsers()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;
            
            using (var context = new RepositoryContext(options))
            {
                var repo = new RepositoryWrapper(context);
                
                await repo.User.CreateUserAsync(new User() 
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester1",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });
                await repo.User.CreateUserAsync(new User() 
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester2",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });
                await repo.User.CreateUserAsync(new User() 
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester3",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });                
            }

            using (var context = new RepositoryContext(options))
            {
                Assert.Equal(3, await context.User.CountAsync());
            }
        }
        [Fact]
        public async void GetUserByUsername()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new RepositoryContext(options))
            {
                var repo = new RepositoryWrapper(context);

                var user = await repo.User.GetUserByUsernameAsync("Tester1");

                Assert.Equal("Tester1", user.Username);
            }
        }
        [Fact]
        public async void GetUserAvailableBalance()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;
            
            using (var context = new RepositoryContext(options))
            {
                var repo = new RepositoryWrapper(context);

                var user = await repo.User.GetUserByUsernameAsync("Tester1");

                var balance = await repo.User.GetUserAvailableBalanceAsync(user.Id);

                Assert.Equal(100, balance);
            }
        }
    }
}