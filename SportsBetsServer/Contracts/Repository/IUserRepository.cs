using SportsBetsServer.Entities;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByUsername(string username);
    }
}