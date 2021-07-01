using SportsBetsServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options) 
        {}
        public DbSet<Account> Account { get; set; }
        //public DbSet<Wager> Wager { get; set; }
        public DbSet<Counter> Counter { get; set; }
    }
}
