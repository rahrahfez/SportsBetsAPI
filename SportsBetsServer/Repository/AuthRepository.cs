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
        private readonly RepositoryContext _repositoryContext;
        public AuthRepository(RepositoryContext repositoryContext)
            : base (repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
    }
}