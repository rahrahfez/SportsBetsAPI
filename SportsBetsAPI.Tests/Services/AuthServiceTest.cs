using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Services;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Repository;

namespace SportsBetsAPI.Tests.Services
{
    public class AuthServiceTest
    {
        private readonly IAuthService _authService;
        private DbContextOptions<RepositoryContext> _options;
        private IRepositoryWrapper _repo;
        public AuthServiceTest()
        {
            _options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "_")
                .Options;
            using (var context = new RepositoryContext(_options))
            {
                _repo = new RepositoryWrapper(context);
                _authService = new AuthService(_repo);
            }
        }
        [Fact]
        public void CreatePasswordHashTest()
        {
            byte[] passHash;
            byte[] passSalt;

            _authService.CreatePasswordHash("password", out passHash, out passSalt);

            Assert.NotNull(passHash);
            Assert.NotNull(passSalt);
        }
    }
}
