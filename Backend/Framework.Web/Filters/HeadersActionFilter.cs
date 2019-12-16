using Framework.Business.Request;
using Framework.Business.Response;
using Framework.Helpers;
using Framework.Web.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace Framework.Web.Filtes
{
    public class HeadersActionFilter : ActionFilterAttribute
    {
        WebAPIRequestHelper webAPIRequestHelper = new WebAPIRequestHelper();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                if (context.Result is ObjectResult || context.ActionArguments.FirstOrDefault().Value is RequestBase)
                {
                    RequestBase objResult;
                    if (context.Result != null)
                    {
                        objResult = (RequestBase)((ObjectResult)context.Result).Value;
                    }
                    else
                    {
                        objResult = (RequestBase)context.ActionArguments.FirstOrDefault().Value;
                    }
                    var requestHeaders = webAPIRequestHelper.GetHeadersFromRequest(context);
                    objResult.UserId = requestHeaders.Where(x => x.Key == "UserId").Select(x => Convert.ToInt32(x.Value ?? "0")).FirstOrDefault();
                    objResult.IdCompany = requestHeaders.Where(x => x.Key == "IdCompany").Select(x => Convert.ToInt32(x.Value ?? "0")).FirstOrDefault();
                }
            }

            base.OnActionExecuting(context);
        }

    }
}
