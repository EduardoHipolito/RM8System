using Framework.Domain;
using Framework.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Business.Request
{
    public class RequestGrid<TEntity, TParameter> : RequestBase<TParameter> where TEntity : class
    {
        public GridSettings<TEntity> Settings { get; set; }
    }

    public class RequestGrid<TEntity> : RequestBase where TEntity : EntityBase
    {
        public GridSettings<TEntity> Settings { get; set; }
    }

    public class GridSettings<TEntity> where TEntity : class
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public string ColumOrder { get; set; }
        public string OrderDirection { get; set; }
        public TEntity Filter {  get; set; }
        //public TEntity FilterObj
        //{
        //    get
        //    {
        //        return Mapper.Instance.Map<TEntity>(this.Filter);
        //    }
        //}
        //public List<KeyValuePair<string, string>> Filters { get; set; }
    }
}
