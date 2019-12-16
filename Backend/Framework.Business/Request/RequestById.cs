using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Business.Request
{
    public class RequestById: RequestBase
    {
        public int Id { get; set; }

    }
}
