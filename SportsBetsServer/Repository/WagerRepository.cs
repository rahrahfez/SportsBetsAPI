using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Repository
{
    public class WagerRepository : RepositoryBase<Wager>, IWagerRepository
    {
        public WagerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
            { }
        public async Task<IEnumerable<Wager>> GetAllWagersAsync()
        {
            return await RepositoryContext.Wager.ToListAsync();
        }
        public async Task<Wager> GetWagerByIdAsync(Guid Id)
        {
            return await RepositoryContext.Wager
                    .Where(x => x.Id.Equals(Id)).SingleOrDefaultAsync();
        }
    }
}