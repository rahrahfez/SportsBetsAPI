using System;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using System.Threading.Tasks;

namespace SportsBetsServer.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext ?? throw new ArgumentNullException("context");
        }
        public virtual void Add(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }
        public virtual async Task AddAsync(T entity)
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
        }
        public virtual void Remove(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
