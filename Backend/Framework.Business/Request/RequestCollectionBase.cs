using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Request
{
    public class RequestCollectionBase<TFilter, TParameter> : RequestCollectionBase<TFilter>
    {
        public TParameter Filter { get; set; }
    }

    public class RequestCollectionBase<TFilter> : RequestCollectionBase
    {
        public TFilter Parameters { get; set; }
    }

    public class RequestCollectionBase : RequestBase
    {
        public RequestCollectionSetting Setting { get; set; }
    }
}
