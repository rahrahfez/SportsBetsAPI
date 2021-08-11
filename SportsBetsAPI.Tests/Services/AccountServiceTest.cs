using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Services;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using SportsBetsServer.Helpers;
using SportsBetsServer.Repository;
using AutoMapper;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using SportsBetsAPI.Tests.Repository;

namespace SportsBetsAPI.Tests.Services
{
    public class AccountServiceTest : SqliteInMemory
    {
        public AccountServiceTest()
        {}
        [Fact]
        public async Task DatabaseIsAvailable()
        {
            Assert.True(await SqlDbContext.Database.CanConnectAsync());
        }
        [Fact]
        public void AccountService_GetAccountByUsername_ReturnsAccount()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            AccountService sut = new AccountService(
                SqlDbContext,
                mapper.Object,
                logger.Object);

            var newAccount = new Account(
                Guid.NewGuid(),
                "test",
                "aownecioinmaokm134f1",
                1,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now)
            {};

            SqlDbContext.Add(newAccount);              
            SqlDbContext.SaveChanges();

            var account = sut.GetAccountByUsername("test");

            Assert.NotNull(account);
            Assert.IsType<Account>(account);
            Assert.Equal("test", account.Username);

            account = sut.GetAccountByUsername("wrong");

            Assert.Null(account);
        }
        [Fact]
        public void AccountService_GetAllUsers_ReturnsListOfUsers()
        {
            var newAccount = new Account(
                Guid.NewGuid(),
                "test",
                "aownecioinmaokm134f1",
                1,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now)
            { };
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<AccountResponseDTO>(newAccount))
                .Returns(new AccountResponseDTO { Id = Guid.NewGuid(), Username = "test", AvailableBalance = 1 });
            var logger = new Mock<ILoggerManager>();
            AccountService sut = new AccountService(
                SqlDbContext,
                mapper.Object,
                logger.Object);

            SqlDbContext.Add(newAccount);
            SqlDbContext.SaveChanges();

            var users = sut.GetAllUsers();
            var user = users.Find(x => x.Username == "test");

            Assert.NotNull(user);
            Assert.Equal("test", user.Username);
        }
        [Fact]
        public async Task AccountService_GetAccountById_ReturnsAccountOrException()
        {
            var accountId = Guid.NewGuid();
            var newAccount = new Account(
                accountId,
                "test",
                "aownecioinmaokm134f1",
                1,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now)
            { };
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            AccountService sut = new AccountService(
                SqlDbContext,
                mapper.Object,
                logger.Object);

            SqlDbContext.Add(newAccount);
            SqlDbContext.SaveChanges();

            var account = await sut.GetAccountById(accountId);

            Assert.NotNull(account);
            Assert.Equal(accountId.ToString(), account.Id.ToString());

            //var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.GetAccountById(Guid.NewGuid()));

            //Assert.IsType<NotFoundException>(ex);
        }
        [Fact]
        public void AccountService_CreatePasswordHash_ReturnsHashedPassword()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            AccountService sut = new AccountService(
                SqlDbContext,
                mapper.Object,
                logger.Object);

            string password = "password";
            string hashedPassword = sut.CreatePasswordHash(password);

            Assert.NotNull(hashedPassword);
        }
        [Fact]
        public void AccountService_VerifyPasswordTest_ReturnsBoolean()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILoggerManager>();
            AccountService sut = new AccountService(
                SqlDbContext,
                mapper.Object,
                logger.Object);

            string password = "password";
            string hashedPassword = sut.CreatePasswordHash(password);
            var result = sut.VerifyPassword(password, hashedPassword);

            Assert.True(result);

            password = "wrong";
            result = sut.VerifyPassword(password, hashedPassword);

            Assert.False(result);           
        }
    }
}
