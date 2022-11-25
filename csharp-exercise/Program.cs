using System.Reflection.PortableExecutable;
using System.IO.Enumeration;
using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using csharp_exercise.Contracts;
using csharp_exercise.Repository;

namespace Csharp.Exercise
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            // so we can resuse our log service easily
            var logService = ActivatorUtilities.CreateInstance<LogService>(host.Services);
            logService.Connect();
        }

        static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        static IHost AppStartup()
        {
            var builder = new ConfigurationBuilder();
            ConfigSetup(builder);

            // defining Serilog configs
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            // initiated the Dependency Injection container
            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            services.AddTransient<ILogService, LogService>();
                        })
                        .UseSerilog()
                        .Build();
            return host;
        }
    }
}
