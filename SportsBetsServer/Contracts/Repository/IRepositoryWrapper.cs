using System;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryWrapper : IDisposable
    {
         IUserRepository User { get; }
         IWagerRepository Wager { get; }         
    }
}