using System;
using System.Threading.Tasks;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryWrapper : IDisposable
    {
         IUserRepository User { get; }
         IWagerRepository Wager { get; }
         IAuthRepository Auth { get; }
         
         Task Complete();
    }
}