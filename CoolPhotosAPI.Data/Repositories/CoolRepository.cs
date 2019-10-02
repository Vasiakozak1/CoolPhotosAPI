using CoolPhotosAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoolPhotosAPI.Data.Repositories
{
    public class CoolRepository<TType> : IRepository<TType> where TType : Entity
    {
        protected DbContext _context;
        public CoolRepository(DbContext context)
        {
            _context = context;
        }
        public void Create(TType entity)
        {
            _context.Add(entity);
        }

        public TType Get(Guid guid)
        {
            return _context.Set<TType>()
                .Find(guid);
        }

        public IEnumerable<TType> GetAll()
        {
            return _context.Set<TType>()
                .ToArray();
        }

        public IEnumerable<TType> GetAll(Expression<Func<TType, bool>> predicate)
        {
            return _context.Set<TType>()
                .Where(predicate);
        }

        public IEnumerable<TType> GetAllIncluding<TRelatedEntityType>(Expression<Func<TType, TRelatedEntityType>> includeExpression)
        {
            return _context.Set<TType>()
                .Include(includeExpression);
        }

        public TType GetSingle(Expression<Func<TType, bool>> predicate)
        {
            return _context.Set<TType>()
                .SingleOrDefault(predicate);
        }

        public void Remove(TType entity)
        {
            _context.Set<TType>()
                .Remove(entity);
        }

        public void RemoveAll(Expression<Func<TType, bool>> predicate)
        {
            _context.Set<TType>()
                .Where(predicate)
                .ForEachAsync(entity => 
                    _context.Set<TType>().Remove(entity));
        }

        public void RemoveSingle(Expression<Func<TType, bool>> predicate)
        {
            _context.Set<TType>()
                .Remove(_context.Set<TType>().Single(predicate));
        }

        public void Update(TType entity)
        {
            _context.Set<TType>()
                .Remove(entity);
        }
    }
}
