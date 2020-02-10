using Contracts.Repository;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;
        private IUserRepository _user;
        private IWagerRepository _wager;
        private IAuthRepository _auth;
        public RepositoryWrapper(RepositoryContext repositoryContext) 
        {
            _repoContext = repositoryContext;
        }
        public IUserRepository User 
        {
            get 
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
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
                    _wager = new WagerRepository(_repoContext);
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
                    _auth = new AuthRepository(_repoContext);
                }
                
                return _auth;
            }
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}