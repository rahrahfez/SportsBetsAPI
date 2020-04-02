using System;
using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IAuthRepository : IRepositoryBase<Credential>
    { }
}