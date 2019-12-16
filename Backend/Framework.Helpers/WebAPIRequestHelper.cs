using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Framework.Helpers
{
    public class WebAPIRequestHelper
    {
        public ClaimsIdentity AuthenticationUserGet(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            var IdCompany = "0";

            var requestHeaders = this.GetHeadersFromRequest(context);
            IdCompany = RetornaHeaders(requestHeaders, nameof(IdCompany));

            InsereClainIdCompany(identity, nameof(IdCompany), IdCompany);
            return context.User.Identity as ClaimsIdentity;
        }
        public static void InsereClainIdCompany(ClaimsIdentity claims, string key, string value)
        {
            if (claims != null)
            {
                if (claims.FindFirst(key) != null)
                {
                    claims.RemoveClaim(claims.FindFirst(key));
                }
                claims.AddClaim(new Claim(key, value == null || value == string.Empty ? "0" : value));
            }
        }

        private static string RetornaHeaders(List<KeyValuePair<string, string>> requestHeaders, string key)
        {
            return requestHeaders.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
        }

        public List<KeyValuePair<string, string>> GetHeadersFromRequest(HttpContext context)
        {
            var headersList = new List<KeyValuePair<string, string>>();

            foreach (var header in context.Request.Headers)
            {
                var value = ((string[])(header.Value))[0];
                if (value.Any(a => a == ','))
                {
                    value = value.Split(',')[0];
                }
                headersList.Add(new KeyValuePair<string, string>(header.Key, value));
            }

            return headersList;
        }

        public List<KeyValuePair<string, string>> GetHeadersFromRequest(ActionContext context)
        {
            var headersList = new List<KeyValuePair<string, string>>();

            foreach (var header in context.HttpContext.Request.Headers)
            {
                headersList.Add(new KeyValuePair<string, string>(header.Key, header.Value));
            }

            return headersList;
        }
    }
}
