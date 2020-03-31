using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repositoryContext;
        private IUserRepository _user;
        private IWagerRepository _wager;
        private IAuthRepository _auth;
        public RepositoryWrapper(RepositoryContext repositoryContext) 
        {
            _repositoryContext = repositoryContext;
        }
        public IUserRepository User 
        {
            get 
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repositoryContext);
                }

                return _user;
            }
        }
        public IWagerRepository Wager
        {
            get
            {
                if (_wager == null)
                {
                    _wager = new WagerRepository(_repositoryContext);
                }

                return _wager;
            }
        }
        public IAuthRepository Auth
        {
            get
            {
                if (_auth == null)
                {
                    _auth = new AuthRepository(_repositoryContext);
                }
                
                return _auth;
            }
        }
        public async Task Complete()
        {
            await _repositoryContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _repositoryContext.Dispose();
        }
    }
}