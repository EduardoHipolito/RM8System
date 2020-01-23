using Framework.Domain;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.DataAccess
{
    public class DataAccessBase
    {
        protected DbContext _dbContext;

        //public void Dispose()
        //{
        //    this._dbContext.Dispose();
        //    this._dbContext = null;
        //}

        public void SetDbContext(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }
    }
    public class DataAccessBase<TEntity, TContext> : DataAccessBase<TEntity>, IDataAccessBase<TEntity> where TEntity : EntityBase where TContext : DbContext
    {
        public TContext Context
        {
            get
            {
                return _dbContext as TContext;
            }
        }
    }
    public class DataAccessBase<TEntity> : DataAccessBase where TEntity : EntityBase
    {

        public DbSet<TEntity> Entity { get { return _dbContext.Set<TEntity>(); } }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> Predicate)
        {
            return Entity.Where(Predicate).AsNoTracking();
        }

        public virtual void Add(TEntity obj)
        {
            Entity.Add(obj);
        }

        public virtual void AddAll(IEnumerable<TEntity> obj)
        {
            foreach (var entity in obj)
            {
                Add(entity);
            }
        }

        public virtual void DeleteAll(IEnumerable<TEntity> obj)
        {
            foreach (var entity in obj)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(TEntity obj)
        {
            if (!Entity.Any(x => x.Id == obj.Id))
            {
                throw new Exception("Não encontrado");
            }
            Entity.Remove(obj);
        }

        public virtual void Delete(int id)
        {
            var del = Get(id);
            if (del == null)
            {
                throw new Exception("Não encontrado");
            }
            Delete(del);
        }

        public virtual TEntity Get(int id, int IdCompany = 0)
        {
            return GetAll(IdCompany).Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
        }

        public virtual TEntity First(int IdCompany = 0)
        {
            return GetAll(IdCompany).AsNoTracking().FirstOrDefault();
        }

        public virtual IQueryable<TEntity> GetAll(int IdCompany = 0)
        {
            if (IdCompany > 0)
            {
                return Entity.Where(x => x.IdCompanyPermition == IdCompany || x.IdCompanyPermition == null).AsNoTracking();
            }
            return Entity.AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAllGrid(int IdCompany = 0)
        {
            if (IdCompany > 0)
            {
                return Entity.Where(x => x.IdCompanyPermition == IdCompany || x.IdCompanyPermition == null).AsNoTracking();
            }
            return Entity.AsNoTracking();
        }

        public virtual void Update(TEntity obj)
        {
            if (!Entity.Any(x => x.Id == obj.Id))
            {
                throw new Exception("Não encontrado");
            }
            obj.ModifiedDate = DateTime.Now;
            this._dbContext.Entry(obj).State = EntityState.Modified;
        }

        public virtual void AddOrUpdate(TEntity obj)
        {
            if (obj.Id > 0)
                Update(obj);
            else
                Add(obj);
        }

        public virtual void Commit()
        {
            var error = Validate();
            if (!string.IsNullOrEmpty(error))
            {
                var ex = new Exception(error);
                Log.Instance.ErrorLog(ex);
                throw ex;
            }
            else
            {
                try
                {
                    _dbContext.SaveChanges();

                }
                catch (Exception e)
                {
                    Log.Instance.ErrorLog(e);
                }
            }
        }

        private string Validate()
        {
            var entities = (from entry in _dbContext.ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);
            string errors = "";
            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    errors += string.Concat(validationResults.Select(s => " " + s.ErrorMessage + " -"));
                }
            }

            if (!string.IsNullOrEmpty(errors))
            {
                errors = errors.Substring(errors[errors.Length - 1], 1).Trim();
            }

            return errors;
        }

        public virtual void DisableAll(IEnumerable<TEntity> obj)
        {
            foreach (var item in obj)
            {
                Disable(item);
            }
        }

        public virtual void Disable(TEntity obj)
        {
            if (!Entity.Any(x => x.Id == obj.Id))
            {
                throw new Exception("Não encontrado");
            }
            obj.Status = Domain.Enum.EntityType.Inactive;
        }

        public virtual void Enable(TEntity obj)
        {
            if (!Entity.Any(x => x.Id == obj.Id))
            {
                throw new Exception("Não encontrado");
            }
            obj.Status = Domain.Enum.EntityType.Active;
        }

        public virtual void EnableAll(IEnumerable<TEntity> obj)
        {
            foreach (var item in obj)
            {
                Enable(item);
            }
        }
    }

}
