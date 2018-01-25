using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging.File;

namespace Application.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                        .UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var env = hostingContext.HostingEnvironment;
                            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                            config.AddEnvironmentVariables();
                            
                            Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File("log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

                        })
                        .ConfigureLogging((hostingContext, logging) =>
                        {
                            logging.AddSerilog(Log.Logger,true);
                            logging.AddConsole();
                        })
                        .UseStartup<Startup>()
                        .Build();
        }
    }
}
