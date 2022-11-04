using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryMgmt
{
    public  static class Program
    {
        public async static Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;
            if (isDevelopment) {
                environment = environment.Length > 0 ? "." + environment : "";
                Console.WriteLine($"Current Environment is: { environment}");
            }
            else
            {
                environment = string.Empty;
            }
            //works with builder using multiple calls
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile($"appsettings{environment}.json");
            builder.Build(); 
            //Read Configuration from appsettings
            var config = new ConfigurationBuilder().AddJsonFile($"appsettings{environment}.json").Build();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
