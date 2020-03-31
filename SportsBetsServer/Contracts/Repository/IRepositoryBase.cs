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
        Task<T> FindById(int id);
        Task<IEnumerable<T>> FindAll();
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Delete(T entity);
    }
}