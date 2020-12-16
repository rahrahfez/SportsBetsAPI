using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Repository
{
    public class RepositoryContext : DbContext, IRepositoryContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options) 
        {}
        public DbSet<Account> Account { get; set; }
        public DbSet<Wager> Wager { get; set; }
    }
}
