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
        private readonly DbContextOptions<RepositoryContext> _options;
        private readonly RepositoryContext _context;
        public UserRepositoryTest()
        {
            _context = new RepositoryContext(_options);
        }
        [Fact(Skip = "Not fully implemented")]
        public async void GetAllUsers()
        {
            using var repo = new RepositoryWrapper(_context);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

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

            Assert.Equal(3, await _context.User.CountAsync());
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