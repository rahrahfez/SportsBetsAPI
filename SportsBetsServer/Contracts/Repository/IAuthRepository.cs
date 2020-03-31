using System;
using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IAuthRepository
    {
        Task<Credential> GetCredentialByUserId(Guid id);
        void CreateCredential(Credential cred);
        void DeleteCredential(Credential cred);
    }
}