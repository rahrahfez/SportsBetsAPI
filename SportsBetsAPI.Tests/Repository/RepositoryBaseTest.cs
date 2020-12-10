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

            var repo = new Mock<IRepositoryBase<TestStub>>();
            repo.Setup(x => x.Add(It.IsAny<TestStub>())).Verifiable();
            repo.Setup(x => x.Complete()).Returns(Task.CompletedTask);

            repo.Object.Add(stub);
            repo.Object.Complete();

            repo.Verify();           
        }
    }
}