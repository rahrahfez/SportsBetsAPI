using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IBetRepository : IRepositoryBase<Bet>
    { 
        Task<Bet> FindById(int id);
    }
}