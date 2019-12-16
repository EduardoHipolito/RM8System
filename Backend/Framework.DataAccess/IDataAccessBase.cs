using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Framework.Domain;

namespace Framework.DataAccess
{
    public interface IDataAccessBase<TEntity>
        where TEntity : EntityBase
    {
        void Add(TEntity obj);
        void AddAll(IEnumerable<TEntity> obj);
        void AddOrUpdate(TEntity obj);
        void Enable(TEntity obj);
        void EnableAll(IEnumerable<TEntity> obj);
        void Commit();
        void Delete(int id);
        void Delete(TEntity obj);
        void DeleteAll(IEnumerable<TEntity> obj);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> Predicate);
        TEntity First(int IdCompany = 0);
        TEntity Get(int id, int IdCompany = 0);
        IQueryable<TEntity> GetAll(int IdCompany = 0);
        void Disable(TEntity obj);
        void DisableAll(IEnumerable<TEntity> obj);
        void Update(TEntity obj);
    }
}