using Chat.DataAccess.Database;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Helper;
using Chat.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EFCore.BulkExtensions;

namespace Chat.DataAccess.Implementation.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ChatContext _context;

        public BaseRepository(ChatContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            entity.Created = entity.LastUpdated = DateTime.UtcNow;

            _context.Set<T>().Add(entity);
        }

        public void AddRange(IList<T> entites)
        {
            foreach (var entity in entites)
            {
                entity.Created = entity.LastUpdated = DateTime.UtcNow;
            }

            //_context.BulkInsert(entites);
            _context.Set<T>().AddRange(entites);
        }

        public T Get(Guid Id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == Id);
        }

        public IQueryable<T> GetAll(PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
                return _context.Set<T>().AsNoTracking();

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return _context.Set<T>().Skip(skip).Take(paginationFilter.PageSize).AsNoTracking();
        }

        public T GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefault(expression);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IList<T> entites)
        {
            _context.BulkDelete(entites);
        }

        public void Update(T entity)
        {
            entity.LastUpdated = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression, PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
                return _context.Set<T>().Where(expression);

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return _context.Set<T>().Where(expression).Skip(skip).Take(paginationFilter.PageSize).AsNoTracking();
        }
    }
}
