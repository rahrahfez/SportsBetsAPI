using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<T> FindByGuid(Guid id);
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Delete(T entity);
    }
}