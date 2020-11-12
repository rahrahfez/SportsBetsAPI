using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Repository
{
    public class BetRepository : RepositoryBase<Bet>, IBetRepository
    {
        public BetRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {}
        public async Task<Bet> FindById(int id)
        {
            return await this.RepositoryContext.Set<Bet>().FindAsync(id);
        }
    }
}