using System;
using Xunit;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Services;

namespace SportsBetsAPI.Tests.Services
{
    public class WagerServiceTest
    {
        private IWagerService _wagerService;
        public WagerServiceTest()
        {
            _wagerService = new WagerService();
        }
        [Fact]
        public void CreateWagerTest()
        {
            var wager = _wagerService.CreateWager(new Wager { WinCondition = "greater than" });

            Assert.NotNull(wager);
            Assert.Equal("greater than", wager.WinCondition);
        }
        [Fact]
        public void AcceptWagerTest()
        {
            var wager = _wagerService.CreateWager(new Wager { WinCondition = "less than" });
            
            wager = _wagerService.AcceptWager(wager);
            
            Assert.NotNull(wager);
        }
    }
}