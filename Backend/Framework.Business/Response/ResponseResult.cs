using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Response
{
    public class ResponseResult
    {
        public ResponseState State { get; set; }
        public string Msg { get; set; }
        public string Type { get { return Data != null ? Data.GetType().FullName : null; } }
        public Object Data { get; set; }
    }

    public enum ResponseState
    {
        Failed = -1,
        NotAuth = 0,
        Success = 1
    }
}
