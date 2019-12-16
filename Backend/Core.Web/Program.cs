using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Framework.Web;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new BaseProgram().GetHost<Startup>();
            host.Run();
        }
    }
}
