using System;
using Xunit;
using SportsBetsServer.Entities.Extensions;
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
                    UserId = Guid.NewGuid(),
                    WinCondition = "greater than",
                    Amount = 100
                });

            Assert.NotNull(wager);
            Assert.Equal("greater than", wager.WinCondition);
            Assert.Equal(100, wager.Amount);   
            Assert.Equal("open", wager.Status.ToString());

        }
        [Fact]
        public void AcceptWagerTest()
        {
            var wager = _wagerService.CreateWager(
                new WagerToCreate 
                { 
                    WinCondition = "less than" 
                });
            
            wager = _wagerService.AcceptWager(wager);
            
            Assert.NotNull(wager);
            Assert.Equal("pending", wager.Status.ToString());
        }
    }
}