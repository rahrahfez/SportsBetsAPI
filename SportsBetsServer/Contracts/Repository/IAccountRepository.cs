using SportsBetsServer.Entities;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Account GetUserByUsername(string username);
    }
}