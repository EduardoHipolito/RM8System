using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Business.Request
{
    public class RequestBase<T> : RequestBase
    {
        public T Parameter { get; set; }
    }

    public class RequestBase
    {
        public int UserId { get; set; }
        public int IdCompany { get; set; }
    }
}
