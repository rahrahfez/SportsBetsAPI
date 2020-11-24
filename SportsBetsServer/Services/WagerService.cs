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
                Id = wager.Id,
                UserId = wager.UserId,
                DateCreated = DateTime.Now,
                Status = Status.open,
                Amount = wager.Amount,
            };
        }
    }
}