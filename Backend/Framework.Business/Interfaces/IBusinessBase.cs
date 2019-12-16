using Framework.Business.Request;
using Framework.Business.Response;
using Framework.DataAccess;
using Framework.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Framework.Business.Interfaces
{
    public interface IBusinessBase<TEntity> where TEntity : EntityBase 
    {
        bool Add(RequestBase<TEntity> request);

        bool AddAll(RequestBase<IEnumerable<TEntity>> request);

        bool DeleteAll(RequestBase<IEnumerable<TEntity>> request);

        bool Delete(RequestBase<TEntity> request);

        bool Delete(RequestById request);

        bool Disable(RequestBase<TEntity> obj);

        bool DisableAll(RequestBase<IEnumerable<TEntity>> request);

        bool Enable(RequestBase<TEntity> request);

        bool EnableAll(RequestBase<IEnumerable<TEntity>> request);

        TEntity Get(RequestById request);

        IList<TEntity> GetAll(RequestBase request);

        IList<TEntity> GetAllByIdList(RequestByIdList request);

        bool Update(RequestBase<TEntity> request);

        ResponseGrid<TEntity> GetAllGrid(RequestGrid<JObject, JObject> request, IQueryable<TEntity> query = null);
    }
}
