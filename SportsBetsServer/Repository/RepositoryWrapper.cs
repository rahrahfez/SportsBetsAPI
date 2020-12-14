using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repositoryContext;
        private IUserRepository _user;
        private IWagerRepository _wager;
        public RepositoryWrapper(RepositoryContext repositoryContext) 
        {
            _repositoryContext = repositoryContext;
        }
        public IUserRepository User 
        {
            get
            {
                return _user ??= new UserRepository(_repositoryContext);
            }
        }
        public IWagerRepository Wager
        {
            get
            {
                return _wager ??= new WagerRepository(_repositoryContext);
            }
        }
    }
}