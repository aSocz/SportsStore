using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SportsStore.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetNoTracking(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAllNoTracking();
        void Insert(T entity);
        void Delete(T entity);
    }
}