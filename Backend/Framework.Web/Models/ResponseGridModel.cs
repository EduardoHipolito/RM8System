using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Models
{
    public class ResponseGridModel<TEntity> where TEntity : BaseModel
    {
        public int TotalRecords { get; set; }
        public List<TEntity> DataList { get; set; }
    }
}
