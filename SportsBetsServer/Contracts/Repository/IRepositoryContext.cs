using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryContext 
    {
        DbSet<Account> Account { get; set; }
        DbSet<Wager> Wager { get; set; }
    }
}
