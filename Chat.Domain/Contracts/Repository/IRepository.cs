using Chat.Domain.Helper;
using Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Chat.Domain.Contracts.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(PaginationFilter paginationFilter = null);
        IQueryable<T> Where(Expression<Func<T, bool>> expression, PaginationFilter paginationFilter = null);
        T GetByCondition(Expression<Func<T, bool>> expression);
        T Get(Guid Id);
        void Add(T entity);
        void AddRange(IList<T> entites);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IList<T> entites);
    }
}
