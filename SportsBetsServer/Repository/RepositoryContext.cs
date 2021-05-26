using SportsBetsServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options) 
        {}
        public virtual DbSet<Account> Account { get; set; }
        //public DbSet<Wager> Wager { get; set; }
        public virtual DbSet<Counter> Counter { get; set; }
    }
}
