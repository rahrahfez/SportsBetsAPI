using System;
using System.Threading.Tasks;
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
        public UserRepositoryTest()
        {
        }
        [Fact(Skip = "Not fully implemented")]
        public async void GetAllUsers()
        {
            var repo = new Mock<RepositoryContext>();
            var User = new Mock<DbSet<User>>();
            User.Setup(t => t.AddAsync(It.IsAny<User>())).Verifiable();
            await repo.User.CreateAsync(new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now
            });
            await repo.User.CreateAsync(new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester2",
                AvailableBalance = 100,
                DateCreated = DateTime.Now
            });
            await repo.User.CreateAsync(new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester3",
                AvailableBalance = 100,
                DateCreated = DateTime.Now
            });
            await repo.Complete();

            Assert.Equal(3, await User.CountAsync());
        }
        [Fact(Skip = "Not fully implemented")]
        public async void GetUserByUsername()
        {
            using var context = new RepositoryContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var repo = new RepositoryWrapper(context);
            Guid id = Guid.NewGuid();

            await repo.User.CreateAsync(new User
            {
                Id = id,
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now
            });

            await repo.Complete();

            var user = await repo.User.GetUserByUsernameAsync("Tester1");

            Assert.NotNull(user);
        }
        [Fact(Skip = "Not fully implemented")]
        public async void GetUserAvailableBalance()
        {            
            using var context = new RepositoryContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var repo = new RepositoryWrapper(context);

            await repo.User.CreateAsync(new User
            { 
                Id = Guid.NewGuid(),
                Username = "Tester1",
                AvailableBalance = 100,
                DateCreated = DateTime.Now       
            });

            await repo.Complete();

            var user = await repo.User.GetUserByUsernameAsync("Tester1");

            var balance = await repo.User.GetUserAvailableBalanceAsync(user.Id);

            Assert.Equal(100, balance);

            balance = await repo.User.GetUserAvailableBalanceAsync(Guid.NewGuid());

            Assert.Equal(-1, balance);            
        }
    }
}