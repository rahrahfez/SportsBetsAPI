using System;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Services;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using SportsBetsServer.Repository;
using AutoMapper;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using SportsBetsAPI.Tests.Repository;
using System.Threading.Tasks;

namespace SportsBetsAPI.Tests.Services
{
    public class AccountServiceTest : SqliteInMemory
    {
        public AccountServiceTest()
        {}
        [Fact]
        public async Task DatabaseIsAvailable()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }
        [Fact]
        public void AccountService_GetAllUsers_ReturnsUsers()
        {
            var accounts = new List<Account>
            {
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Username = "test",
                    HashedPassword = "aownecioinmaokm134f1",
                    AvailableBalance = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    LastLoginAt = DateTime.Now
                }
            };
            using (DbContext)
            {
                var mapper = new Mock<IMapper>();
                var logger = new Mock<ILoggerManager>();
                var config = new Mock<IConfiguration>();
                AccountService _ = new AccountService(
                    DbContext,
                    mapper.Object,
                    logger.Object,
                    config.Object);

                DbContext.Add(accounts);
                DbContext.SaveChanges();

                var users = _.GetAllUsers();

                Assert.NotNull(users);
            }
        }
        [Fact]
        public void AccountService_CreatePasswordHashTest()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            var config = new Mock<IConfiguration>();
            AccountService _accountService = new AccountService(
                DbContext,
                mapper.Object,
                logger.Object,
                config.Object);
            string password = "password";
            string hashedPassword = _accountService.CreatePasswordHash(password);

            Assert.NotNull(hashedPassword);
        }
        [Fact]
        public void AccountService_VerifyPasswordTest_ReturnsBoolean()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            var config = new Mock<IConfiguration>();

            AccountService _accountService = new AccountService(
                DbContext,
                mapper.Object,
                logger.Object,
                config.Object);

            string password = "password";
            string hashedPassword = _accountService.CreatePasswordHash(password);
            var result = _accountService.VerifyPassword(password, hashedPassword);

            Assert.True(result);

            password = "wrong";
            result = _accountService.VerifyPassword(password, hashedPassword);

            Assert.False(result);
            
        }
        [Fact]
        public void AccountService_CreateJsonTokenTest()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester",
                AvailableBalance = 100,
            };
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(x => x.Value).Returns("xecretKeywqe239901");
            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection("AppSettings:Token")).Returns(configSection.Object);
            config.Setup(x => x.GetSection("Jwt:Issuer")).Returns(configSection.Object);
            config.Setup(x => x.GetSection("Jwt:Audience")).Returns(configSection.Object);

            AccountService _accountService = new AccountService(
                DbContext,
                mapper.Object,
                logger.Object,
                config.Object);

            var token = _accountService.CreateJsonToken(user);

            Assert.NotNull(token);

        }
        [Fact]
        public void AccountService_GenerateNewUserClaim()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Tester",
                AvailableBalance = 100,
            };
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            var config = new Mock<IConfiguration>();

            AccountService _accountService = new AccountService(
                DbContext,
                mapper.Object,
                logger.Object,
                config.Object);

            Claim[] claims = _accountService.GenerateNewUserClaim(user);

            Assert.NotNull(claims);
        }
    }
}
