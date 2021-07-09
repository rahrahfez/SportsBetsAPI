using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Repository;
using System;

namespace SportsBetsAPI.Tests.Repository
{
    public abstract class SqliteInMemory : IDisposable
    {
        private const string ConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly RepositoryContext DbContext;
        protected SqliteInMemory()
        {
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlite(_connection)
                .Options;
            DbContext = new RepositoryContext(options);
            DbContext.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _connection.Close();
        }
    }
}
