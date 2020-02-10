using System;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts.Repository
{
    public interface IAuthRepository
    {
        Task<Credential> GetCredentialByUserId(Guid id);
        Task CreateCredentialAsync(Credential cred);
        Task UpdateCredentialAsync(Credential cred1, Credential cred2);
        Task DeleteCredentialAsync(Credential cred);
    }
}