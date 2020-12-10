using System;
using Xunit;
using SportsBetsServer.Entities.Models.Extensions;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Services;

namespace SportsBetsAPI.Tests.Services
{
    public class WagerServiceTest
    {
        private readonly IWagerService _wagerService;
        public WagerServiceTest()
        {
            _wagerService = new WagerService();
        }
        [Fact]
        public void CreateWagerTest()
        {
            var wager = _wagerService.CreateWager(
                new WagerToCreate
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Amount = 100
                });

            Assert.NotNull(wager);
            Assert.Equal(100, wager.Amount);
            Assert.Equal("open", wager.Status.ToString());
        }
    }
}