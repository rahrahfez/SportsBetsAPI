using Microsoft.EntityFrameworkCore;

namespace SportsBetsAPI.UnitTests.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext()
        { }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) 
        : base(options)
        { }
    }
}