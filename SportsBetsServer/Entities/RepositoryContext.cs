using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{ 
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> User { get; set; } 
        public DbSet<Wager> Wager { get; set; }
        public DbSet<Bet> Bet { get; set; }
        public DbSet<Credential> Credential { get; set; }
    }
}
