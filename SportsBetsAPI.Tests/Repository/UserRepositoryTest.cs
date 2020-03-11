using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;

namespace SportsBetsAPI.Tests.Repository
{
    public class UserRepositoryTest
    {
        private DbContextOptions _options;
        public UserRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "_")
                .Options;
        }
        [Fact]
        public async void GetAllUsers()
        {            
            using (var context = new RepositoryContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var repo = new RepositoryWrapper(context);
                
                await repo.User.CreateUserAsync(new User
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester1",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });
                await repo.User.CreateUserAsync(new User
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester2",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });
                await repo.User.CreateUserAsync(new User
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester3",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });                
            }

            using (var context = new RepositoryContext(_options))
            {
                Assert.Equal(3, await context.User.CountAsync());
            }
        }
        [Fact]
        public async void GetUserByUsername()
        {
            using (var context = new RepositoryContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var repo = new RepositoryWrapper(context);

                await repo.User.CreateUserAsync(new User
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester1",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });

                var user = await repo.User.GetUserByUsernameAsync("Tester1");

                Assert.Equal("Tester1", user.Username);
            }
        }
        [Fact]
        public async void GetUserAvailableBalance()
        {            
            using (var context = new RepositoryContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var repo = new RepositoryWrapper(context);

                await repo.User.CreateUserAsync(new User
                { 
                    Id = Guid.NewGuid(),
                    Username = "Tester1",
                    AvailableBalance = 100,
                    DateCreated = DateTime.Now       
                });

                var user = await repo.User.GetUserByUsernameAsync("Tester1");

                var balance = await repo.User.GetUserAvailableBalanceAsync(user.Id);

                Assert.Equal(100, balance);
            }
        }
    }
}