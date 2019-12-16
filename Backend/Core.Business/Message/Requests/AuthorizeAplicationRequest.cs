using Framework.Business.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Message.Requests
{
    public class AuthorizeAplicationRequest : RequestBase
    {
        public string OperationType { get; set; }
        public string ModuleCode { get; set; }
        public string AplicationCode { get; set; }
    }
}
