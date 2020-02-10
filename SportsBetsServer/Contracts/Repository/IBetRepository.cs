using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IBetRepository
    {
        Task<Bet> GetBetByIdAsync(int id);
        Task CreateBetAsync(Bet bet);
        Task UpdateBetAsync(Bet b1, Bet b2);
        Task DeleteBetAsync(Bet bet);
    }
}