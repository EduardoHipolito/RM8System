using Framework.Business.Exceptions;
using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using Framework.Business.Response;
using Framework.DataAccess;
using Framework.Domain;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Framework.Business
{

    public abstract class BusinessBase<TEntity, TDataAccess> : IBusinessBase<TEntity> where TEntity : EntityBase where TDataAccess : DataAccessBase<TEntity>
    {
        public TDataAccess _dataAccess { get; private set; }

        public BusinessBase()
        {
            _dataAccess = FactoryDataAccess<TEntity, TDataAccess>();
        }

        private ConcurrentDictionary<string, ContextBase> _dbContextList = new ConcurrentDictionary<string, ContextBase>();

        protected TDAccess FactoryDataAccess<TEtt, TDAccess>() where TEtt : EntityBase where TDAccess : DataAccessBase<TEtt>
        {
            var dataAccessType = typeof(TDAccess);

            var genericTypes = dataAccessType.BaseType.GetGenericArguments();

            if (genericTypes == null ||
                  genericTypes.Length < 2)
            {
                throw new TypeLoadException();
            }

            var instance = Activator.CreateInstance<TDAccess>();
            instance.SetDbContext(this.GetDataContext(genericTypes[1]));

            return instance as TDAccess;
        }

        private ContextBase GetDataContext(Type dataContextType)
        {
            if (dataContextType == null)
            {
                throw new ArgumentNullException();
            }

            var typeName = dataContextType.FullName;
            ContextBase currentDBContext;

            if (_dbContextList.ContainsKey(typeName))
            {
                if (_dbContextList.TryGetValue(typeName, out currentDBContext))
                {
                    return currentDBContext;
                }

                return Activator.CreateInstance(dataContextType) as ContextBase;
            }

            currentDBContext = Activator.CreateInstance(dataContextType) as ContextBase;
            _dbContextList.TryAdd(typeName, currentDBContext);

            //currentDBContext.Database.Migrate();

            return currentDBContext;
        }

        public abstract bool ValidatePermition(int UserId, int IdCompany);
        public abstract bool IsFullUser(int UserId);

        public virtual bool Add(RequestBase<TEntity> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                if (request.IdCompany > 0)
                {
                    request.Parameter.IdCompanyPermition = request.IdCompany;
                }
                request.Parameter.CreateDate = DateTime.Now;
                _dataAccess.Add(request.Parameter);
                _dataAccess.Commit();

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
            return true;
        }

        public virtual bool AddAll(RequestBase<IEnumerable<TEntity>> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                if (request.Parameter.Count() < 0)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                if (request.IdCompany > 0)
                {
                    foreach (var item in request.Parameter)
                    {
                        item.IdCompanyPermition = request.IdCompany;
                    }
                }
                foreach (var item in request.Parameter)
                {
                    if (request.IdCompany > 0)
                    {
                        item.IdCompanyPermition = request.IdCompany;
                    }
                    item.CreateDate = DateTime.Now;
                }
                _dataAccess.AddAll(request.Parameter);
                _dataAccess.Commit();

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            return true;
        }

        public virtual bool Enable(RequestBase<TEntity> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                request.Parameter.ModifiedDate = DateTime.Now;

                _dataAccess.Enable(request.Parameter);
                _dataAccess.Commit();

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            return true;
        }

        public virtual bool EnableAll(RequestBase<IEnumerable<TEntity>> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                if (request.Parameter.Count() < 0)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }

                foreach (var item in request.Parameter)
                {
                    item.ModifiedDate = DateTime.Now;
                }
                _dataAccess.EnableAll(request.Parameter);
                _dataAccess.Commit();

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            return true;
        }

        public virtual bool Delete(RequestById request)
        {
            try
            {

                _dataAccess.Delete(request.Id);
                _dataAccess.Commit();

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            return true;
        }

        public virtual bool Delete(RequestBase<TEntity> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                _dataAccess.Delete(request.Parameter);
                _dataAccess.Commit();

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            return true;
        }

        public virtual bool DeleteAll(RequestBase<List<TEntity>> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {

                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                if (request.Parameter.Count <= 0)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }

                _dataAccess.DeleteAll(request.Parameter);
                _dataAccess.Commit();

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

            return true;
        }

        public virtual TEntity Get(RequestById request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            var Return = _dataAccess.Get(request.Id, IsFullUser(request.UserId) ? 0 : request.IdCompany);
            if (Return == null)
            {
                throw new Exception("Não encontrado");
            }
            return Return;
        }

        public virtual IList<TEntity> GetAll(RequestBase request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            var query = _dataAccess.GetAll(IsFullUser(request.UserId) ? 0 : request.IdCompany);

            return query.ToList();
        }

        public virtual bool Disable(RequestBase<TEntity> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                request.Parameter.ModifiedDate = DateTime.Now;
                _dataAccess.Disable(request.Parameter);
                _dataAccess.Commit();


            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
            return true;
        }

        public virtual bool DisableAll(RequestBase<IEnumerable<TEntity>> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                if (request.Parameter.Count() < 0)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                foreach (var item in request.Parameter)
                {
                    item.ModifiedDate = DateTime.Now;
                }
                _dataAccess.DisableAll(request.Parameter);
                _dataAccess.Commit();


            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
            return true;
        }

        public virtual bool Update(RequestBase<TEntity> request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            try
            {
                if (request.Parameter == null)
                {
                    throw new Exception("Preencha os dados obrigatórios");
                }
                request.Parameter.ModifiedDate = DateTime.Now;

                _dataAccess.Update(request.Parameter);
                _dataAccess.Commit();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
            return true;
        }

        public bool DeleteAll(RequestBase<IEnumerable<TEntity>> request)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> GetAllByIdList(RequestByIdList request)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            var query = _dataAccess.GetAll(IsFullUser(request.UserId) ? 0 : request.IdCompany).Where(w => request.IdList.Contains(w.Id));

            return query.ToList();
        }

        public virtual ResponseGrid<TEntity> GetAllGrid(RequestGrid<JObject, JObject> request, IQueryable<TEntity> query = null)
        {
            ValidatePermition(request.UserId, request.IdCompany);
            var response = new ResponseGrid<TEntity>();
            if (query == null)
            {
                query = _dataAccess.GetAllGrid(IsFullUser(request.UserId) ? 0 : request.IdCompany);
            }
            if (request.Parameter != null)
            {
                query = new FilterObjects<TEntity>().Filter(query, request.Parameter);
            }
            if (request.Settings != null)
            {
                if (request.Settings.Filter != null)
                {
                    query = new FilterObjects<TEntity>().Filter(query, request.Settings.Filter);
                }
                if (!string.IsNullOrEmpty(request.Settings.ColumOrder))
                {
                    query = OrderObjects<TEntity>.Instance.OrderBy(query, request.Settings.ColumOrder, request.Settings.OrderDirection);
                }
            }
            response.TotalRecords = query.Count();

            if (request.Settings != null)
            {
                query = query.Skip((request.Settings.CurrentPage - 1) * request.Settings.ItemsPerPage).Take(request.Settings.ItemsPerPage);
            }
            response.DataList = query.ToList();

            return response;
        }
        protected T CreateRequest<T, R>(R request) where T : RequestBase where R : RequestBase
        {
            var newRequest = Activator.CreateInstance(typeof(T)) as T;

            newRequest.UserId = request.UserId;
            newRequest.IdCompany = request.IdCompany;

            return (T)newRequest;
        }

        //public void Dispose()
        //{
        //    this._dataAccess.Dispose();
        //    this._dataAccess = null;
        //}
    }
}
