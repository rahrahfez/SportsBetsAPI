using SportsBetsServer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Entities
{ 
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) {}
        public DbSet<User> User { get; set; }
        public DbSet<Wager> Wager { get; set; }
    }
}
