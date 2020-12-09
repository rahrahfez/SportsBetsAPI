﻿using System;
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
        public async Task<T> FindByGuidAsync(Guid id)
        {
            return await RepositoryContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await RepositoryContext.Set<T>()
                .ToListAsync();
        }
        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await RepositoryContext.Set<T>()
                .Where(expression).ToListAsync();
        }
        public async Task CreateAsync(T entity) 
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
        }
        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }
        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
