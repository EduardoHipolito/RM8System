using System;
using Framework.Business.Response;
using Framework.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            #region Handle Exception
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    Log.Instance.ErrorLog(error.Error);
                    //when authorization has failed, should retrun a json message to client
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult
                        {
                            State = ResponseState.NotAuth,
                            Msg = "token expired"
                        }));
                    }
                    //when orther error, retrun a error message json to client
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult
                        {
                            State = ResponseState.Failed,
                            Msg = error.Error.Message
                        }));
                    }
                    //when no error, do next.
                    else await next();
                });
            });
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            bool IsProd = false;
            string IsDocker = Environment.GetEnvironmentVariable("IsDocker");
            string jsonRoute = IsProd ? "smarterRoutes.json" : IsDocker == "1" ? "routesDocker.json " : "routes.json";
            Router router = new Router(jsonRoute, IsProd);
            app.Run(async (context) =>
            {
                await router.RouteRequest(context);
            });
        }
    }
}