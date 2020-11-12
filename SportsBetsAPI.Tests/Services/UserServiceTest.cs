using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Entities;

namespace SportsBetsAPI.Tests.Services
{
    public class UserServiceTest
    {
        private DbContextOptions<RepositoryContext> _options;
        private readonly RepositoryContext _repo;
        public UserServiceTest()
        {
            _options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        [Fact]
        public void UserExistTest()
        {
            
        }
    }
}