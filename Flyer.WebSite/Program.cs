using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Flyer.WebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("enter continue");
            Console.ReadLine();
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.UseKestrel();
            webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            webHostBuilder.UseIISIntegration();
            webHostBuilder.UseStartup<Startup>();
            webHostBuilder.UseUrls("http://localhost:5001");
            var host = webHostBuilder.Build();

            host.Run();
        }
    }
}
