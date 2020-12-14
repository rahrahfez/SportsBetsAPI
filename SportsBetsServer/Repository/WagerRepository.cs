using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Repository
{
    public class WagerRepository : RepositoryBase<Wager>, IWagerRepository
    {
        public WagerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
            { }
    }
}