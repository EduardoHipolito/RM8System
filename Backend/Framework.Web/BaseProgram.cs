using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Framework.Web
{
    public class BaseProgram
    {
        public IWebHost GetHost<TStartup>() where TStartup : class
        {
            return new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<TStartup>()
               .Build();
        }
    }
}
