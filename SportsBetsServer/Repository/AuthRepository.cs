using System;
using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Repository
{
    public class AuthRepository : RepositoryBase<Credential>, IAuthRepository
    {
        private readonly RepositoryContext _repoContext;
        public AuthRepository(RepositoryContext repositoryContext)
            : base (repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task<Credential> GetCredentialByUserId(Guid id)
        {
            return await FindByCondition(c => c.User.Id.Equals(id))
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
        public async Task CreateCredentialAsync(Credential cred)
        {
            Create(cred);
            await SaveAsync();
        }
        public async Task UpdateCredentialAsync(Credential credToUpdate, Credential updatedCred)
        {
            Update(updatedCred);
            await SaveAsync();
        }
        public async Task DeleteCredentialAsync(Credential cred)
        {
            Delete(cred);
            await SaveAsync();
        }
    }
}