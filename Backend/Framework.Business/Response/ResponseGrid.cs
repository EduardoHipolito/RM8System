using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Response
{
    public class ResponseGrid<TEntity> where TEntity : EntityBase
    {
        public int TotalRecords { get; set; }
        public List<TEntity> DataList { get; set; }
    }

}
