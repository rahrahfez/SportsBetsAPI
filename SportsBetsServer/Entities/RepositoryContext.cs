using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace SportsBetsServer.Entities
{
    public class RepositoryContext : DbContext, IRepositoryContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options) 
        {}
        public DbSet<User> User { get; set; }
        public DbSet<Wager> Wager { get; set; }
        private IDbContextTransaction _transation;

        public void BeginTransaction()
        {
            _transation = Database.BeginTransaction();
        }
        public void Commit()
        {
            try
            {
                SaveChanges();
                _transation.Commit();
            }
            finally
            {
                _transation.Dispose();
            }
        }
        public void Rollback()
        {
            _transation.Rollback();
            _transation.Dispose();
        }
    }
}
