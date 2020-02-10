using System.Threading.Tasks;
using Entities.Models;

namespace Contracts.Repository
{
    public interface IBetRepository
    {
        Task<Bet> GetBetByIdAsync(int id);
        Task CreateBetAsync(Bet bet);
        Task UpdateBetAsync(Bet b1, Bet b2);
        Task DeleteBetAsync(Bet bet);
    }
}