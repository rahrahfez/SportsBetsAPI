using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Entities.Models;


namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryContext 
    {
        DbSet<User> User { get; set; }
        DbSet<Wager> Wager { get; set; }

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
