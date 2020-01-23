using Framework.Business.Request;
using Framework.Helpers;
using Framework.Web.Filtes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Framework.Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class BaseController : Controller
    {
        protected T CreateRequest<T>() where T : RequestBase
        {
            WebAPIRequestHelper webAPIRequestHelper = new WebAPIRequestHelper();

            var request = Activator.CreateInstance(typeof(T)) as T;

            var requestHeaders = webAPIRequestHelper.GetHeadersFromRequest(this.HttpContext);
            request.UserId = requestHeaders.Where(x => x.Key == "UserId").Select(x => ConverToIntAndRegexHeadersParameters(x.Value)).FirstOrDefault();
            request.IdCompany = requestHeaders.Where(x => x.Key == "IdCompany").Select(x => ConverToIntAndRegexHeadersParameters(x.Value)).FirstOrDefault();

            return (T)request;
        }

        private int ConverToIntAndRegexHeadersParameters(string parameter)
        {
            parameter = Regex.Replace(parameter, "[^0-9]+", string.Empty);

            parameter = parameter == string.Empty ? "0" : parameter;

            return Convert.ToInt32(parameter);
        }
    }
}
