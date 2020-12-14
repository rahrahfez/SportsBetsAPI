using System;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using System.Threading.Tasks;

namespace SportsBetsServer.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext ?? throw new ArgumentNullException("context");
        }
        public T Get(Guid id)
        {
            return RepositoryContext.Set<T>().Find(id);
        }
        public void Add(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
        }
        public void Remove(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
