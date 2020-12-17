using System;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models.Extensions;
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