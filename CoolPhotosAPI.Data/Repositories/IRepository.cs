using CoolPhotosAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoolPhotosAPI.Data.Repositories
{
    public interface IRepository<TType> where TType: Entity
    {
        void Create(TType entity);

        TType Get(Guid guid);

        TType GetSingle(Expression<Func<TType, bool>> predicate);

        IEnumerable<TType> GetAll();

        IEnumerable<TType> GetAll(Expression<Func<TType, bool>> predicate);

        IEnumerable<TType> GetAllIncluding<TRelatedEntityType>(Expression<Func<TType, TRelatedEntityType>> includeExpression);

        void Update(TType entity);

        void Remove(TType entity);

        void RemoveSingle(Expression<Func<TType, bool>> predicate);

        void RemoveAll(Expression<Func<TType, bool>> predicate);
    }
}
