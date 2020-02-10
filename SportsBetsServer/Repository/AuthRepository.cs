using System;
using System.Threading.Tasks;
using Entities.Models;
using Contracts.Services;
using Contracts.Repository;
using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
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