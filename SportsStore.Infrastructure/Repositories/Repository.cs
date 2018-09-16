using SportsStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SportsStore.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDbSet<T> dbSet;
        protected readonly SportsStoreContext dbContext;

        public Repository(SportsStoreContext dbContext)
        {
            dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate) => dbSet.Where(predicate).ToList();

        public IEnumerable<T> GetAll() => dbSet.ToList();

        public IEnumerable<T> GetNoTracking(Expression<Func<T, bool>> predicate) =>
            dbSet.AsNoTracking().Where(predicate).ToList();

        public IEnumerable<T> GetAllNoTracking() => dbSet.AsNoTracking().ToList();

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}