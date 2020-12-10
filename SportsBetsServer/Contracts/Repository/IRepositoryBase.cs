using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryBase<T>
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task Complete();
    }
}