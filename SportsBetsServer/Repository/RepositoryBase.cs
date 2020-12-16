using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SportsBetsServer.Contracts.Repository;

namespace SportsBetsServer.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext ?? 
                throw new ArgumentNullException(nameof(repositoryContext), "missing context");
        }
        public T Get(Guid id)
        {
            return RepositoryContext.Set<T>().Find(id);
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await RepositoryContext.Set<T>().FindAsync(id);
        }
        public IEnumerable<T> GetAll()
        {
            return RepositoryContext.Set<T>();
        }
        public IEnumerable<T> GetAllBy(Expression<Func<T, bool>> predicate)
        {
            return RepositoryContext.Set<T>()
                .Where(predicate).ToList();
        }
        public void Add(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            RepositoryContext.SaveChanges();
        }
        public async Task AddAsync(T entity)
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
            RepositoryContext.SaveChanges();
        }
        public void Remove(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
            RepositoryContext.SaveChanges();
        }
    }
}
