using Xunit;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsAPI.UnitTests.Repository
{
    public class UserRepositoryTest
    {
        [Fact]
        public void CreateUserTest()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "CreateUserTest")
                .Options;
        }
    }
}