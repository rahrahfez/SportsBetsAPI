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
        private readonly RepositoryContext _context;
        public RepositoryBase(RepositoryContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context), "missing context");
        }
        public T Get(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }
        public IEnumerable<T> GetAllBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .Where(predicate).ToList();
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}
