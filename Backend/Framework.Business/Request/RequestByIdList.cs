using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Business.Request
{
    public class RequestByIdList: RequestBase
    {
        public List<int> IdList { get; set; }

    }
}
