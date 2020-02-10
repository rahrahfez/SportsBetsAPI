using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}