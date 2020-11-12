using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<T> FindByGuidAsync(Guid id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        void Create(T entity);
        void Delete(T entity);
    }
}