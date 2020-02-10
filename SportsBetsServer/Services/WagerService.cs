using System;
using Entities.Models;
using Contracts.Services;

namespace Services
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