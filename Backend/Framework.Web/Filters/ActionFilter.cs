using Framework.Business.Request;
using Framework.Business.Response;
using Framework.Helpers;
using Framework.Web.Auth;
using Microsoft.AspNetCore.Authorization;
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
    public class ActionFilter : ActionFilterAttribute
    {
        WebAPIRequestHelper webAPIRequestHelper = new WebAPIRequestHelper();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                    .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
                if (!isDefined)
                {
                    if (context.Result is ObjectResult || context.ActionArguments.FirstOrDefault().Value is RequestBase)
                    {
                        RequestBaseIdentity objResult;
                        if (context.Result != null)
                        {
                            objResult = (RequestBaseIdentity)((ObjectResult)context.Result).Value;
                        }
                        else
                        {
                            objResult = (RequestBaseIdentity)context.ActionArguments.FirstOrDefault().Value;
                        }
                        var claimsIdentity = webAPIRequestHelper.AuthenticationUserGet(context.HttpContext);
                        if (claimsIdentity != null)
                        {
                            objResult.Identity = new Domain.IdentityModel()
                            {
                                Actor = claimsIdentity.Actor,
                                ClainName = claimsIdentity.Name,
                                IsAuthenticated = claimsIdentity.IsAuthenticated,
                                Claims = claimsIdentity.Claims != null ? claimsIdentity.Claims.ToList() : null,
                                AuthenticationType = claimsIdentity.AuthenticationType,
                                IdCompany = Convert.ToInt32(claimsIdentity.FindFirst("IdCompany").Value),
                                Id = Convert.ToInt32(claimsIdentity.FindFirst("UserId").Value)
                            };
                        }
                    }
                }
            }

            base.OnActionExecuting(context);
        }

        //public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        //{
        //    var controllerActionDescriptor = actionExecutedContext.ActionDescriptor as ControllerActionDescriptor;
        //    if (controllerActionDescriptor != null)
        //    {
        //        var isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
        //            .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
        //        if (!isDefined)
        //        {
        //            var requestHeaders = webAPIRequestHelper.GetHeadersFromRequest(actionExecutedContext);

        //            if (requestHeaders != null && requestHeaders.Count > 0)
        //            {
        //                var refresh_token = requestHeaders.Where(x => x.Key == "refresh_token").Select(x => x.Value).FirstOrDefault();

        //                if (refresh_token != null && refresh_token.Count() > 0)
        //                {
        //                    var requestAt = DateTime.Now;
        //                    var identity = actionExecutedContext.HttpContext.User.Identity as ClaimsIdentity;
        //                    identity.FindFirst("");
        //                    var user = new UserLoginViewModel()
        //                    {
        //                        Id = Convert.ToInt32(identity.FindFirst("UserId").Value),
        //                        UserName = identity.Name
        //                    };
        //                    var _tokenAuthOption = new TokenAuthOption();
        //                    var expiresIn = requestAt + _tokenAuthOption.ExpiresSpan;
        //                    actionExecutedContext.HttpContext.Response.Headers.Add("access_token", _tokenAuthOption.GenerateToken(user, expiresIn, "access_token"));
        //                    actionExecutedContext.HttpContext.Response.Headers.Add("refresh_token", _tokenAuthOption.GenerateToken(user, expiresIn.AddSeconds(10), "refresh_token"));

        //                }


        //            }
        //        }
        //    }
        //    base.OnActionExecuted(actionExecutedContext);

        //}
    }
}
