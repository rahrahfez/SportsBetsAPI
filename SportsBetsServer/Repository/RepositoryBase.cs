using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
            RepositoryContext = repositoryContext;
        }
        public async Task<T> FindByGuid(Guid id)
        {
            return await this.RepositoryContext.Set<T>().FindAsync(id);
        }
        public async Task<T> FindById(int id)
        {
            return await this.RepositoryContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAll()
        {
            return await this.RepositoryContext.Set<T>()
                .ToListAsync();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .Where(expression);
        }
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }
        public async Task SaveAsync() 
        {
            await this.RepositoryContext.SaveChangesAsync();
        }
    }
}
