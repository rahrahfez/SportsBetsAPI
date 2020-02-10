using System;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Contracts.Services;

namespace SportsBetsServer.Services
{
    public class WagerService : IWagerService
    {
        public Wager CreateNewWager(Guid UserId)
        {
            return new Wager 
            {                
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
            };
        }
    }
}