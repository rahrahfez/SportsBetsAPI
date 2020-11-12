using System;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Extensions;
using SportsBetsServer.Contracts.Services;

namespace SportsBetsServer.Services
{
    public class WagerService : IWagerService
    {
        public Wager CreateWager(WagerToCreate wager)
        {
            return new Wager 
            {                
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                Status = Status.open,
                WinCondition = wager.WinCondition,
                Amount = wager.Amount,
            };
        }
        public Wager AcceptWager(Wager wager)
        {
            if (wager.Id == null)
            {
                return null;
            }
            return new Wager
            {
                Id = wager.Id,
                DateCreated = DateTime.Now,
                Status = Status.pending,
                WinCondition = wager.WinCondition,
                Amount = wager.Amount
            };
        }
    }
}