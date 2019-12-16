using Gateway.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Framework.Business.Response;

namespace Gateway
{
    public class Router
    {

        public List<Route> Routes { get; set; }
        public Destination AuthenticationService { get; set; }

        public bool IsProd { get; set; }

        public Router(string routeConfigFilePath, bool isProd)
        {
            dynamic router = JsonLoader.LoadFromFile<dynamic>(routeConfigFilePath);

            Routes = JsonLoader.Deserialize<List<Route>>(Convert.ToString(router.routes));
            AuthenticationService = JsonLoader.Deserialize<Destination>(Convert.ToString(router.authenticationService));

        }

        public async Task RouteRequest(HttpContext context)
        {
            //if (context.Request.Method.ToUpper() == "OPTIONS")
            //{
            //    context.Response.StatusCode = 204;
            //    return;
            //}
            string path = context.Request.Path.ToString();
            var param = IsProd ? 2 : 1;
            string basePath = '/' + path.Split('/')[param];

            Destination destination;
            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
            }
            catch
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(await ConstructErrorMessage("The path could not be found.", ResponseState.Failed).Content.ReadAsStringAsync());
                return;
            }

            int IdCompany, UserId;
            if (ValidateAuthentication(destination.RequiresAuthentication, context, out IdCompany,out UserId))
            {
                await destination.SendRequest(context, IdCompany, UserId);
                return;
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(await ConstructErrorMessage("Não autenticado.", ResponseState.Success).Content.ReadAsStringAsync());
                return;
            }
        }


        private bool ValidateAuthentication(bool requiresAuthentication, HttpContext context, out int IdCompany, out int UserId)
        {
            if (requiresAuthentication && !StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "OPTIONS"))
            {
                var content = AuthenticationService.ValidateAuthentication(context).Result;
                if (content.Item1)
                {
                    IdCompany = content.Item2;
                    UserId = content.Item3;
                    return true;
                }
                else
                {
                    IdCompany = 0;
                    UserId = 0;
                    return false;
                }
            }
            else
            {
                IdCompany = 0;
                UserId = 0;
                return true;
            }
        }

        private HttpResponseMessage ConstructErrorMessage(string error, ResponseState state)
        {
            var response = JsonConvert.SerializeObject(new ResponseResult
            {
                State = state,
                Msg = error
            });

            HttpResponseMessage errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response)
            };

            return errorMessage;
        }

    }
}
