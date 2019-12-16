using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Business.Request
{

    public class RequestBaseIdentity : RequestBase
    {
        public IdentityModel Identity{ get; set; }
    }
}
