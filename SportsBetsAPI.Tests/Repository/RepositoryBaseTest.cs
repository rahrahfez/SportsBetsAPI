using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;

namespace SportsBetsAPI.Tests.Repository
{
    public class TestStub
    {
        public string Text { get; set; }
    }
    public class RepositoryBaseTest
    {
        public RepositoryBaseTest() { }

        [Fact]
        public void Add_Object_To_Database()
        {
            var stub = new TestStub { Text = "this is a test." };

            var repoBase = new Mock<IRepositoryBase<TestStub>>();
            repoBase.Setup(x => x.Add(It.IsAny<TestStub>())).Verifiable();
            var repo = new Mock<IRepositoryWrapper>();
            repo.Setup(x => x.Complete()).Returns(Task.CompletedTask);

            repoBase.Object.Add(stub);
            repo.Object.Complete();

            repoBase.Verify();           
            
        }
    }
}