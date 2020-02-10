using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IWagerRepository : IRepositoryBase<Wager>
    {
         Task<Wager> GetWagerByIdAsync(Guid id);
         Task<IEnumerable<Wager>> GetAllWagersAsync();
         Task CreateWagerAsync(Wager wager);
         Task UpdateWagerAsync(Wager dbWager, Wager wager);
         Task DeleteWagerAsync(Wager wager);
    }
}