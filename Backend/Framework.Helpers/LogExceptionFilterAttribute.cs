using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Helpers
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            Log _l = Log.Instance;
            _l.ErrorLog(context.Exception);
        }

    }
}
