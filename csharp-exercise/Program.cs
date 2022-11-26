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
using csharp_exercise.Formats;

namespace Csharp.Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Status stat;
            var host = AppStartup();

            // so we can resuse our log service easily
            var logService = ActivatorUtilities.CreateInstance<LogService>(host.Services);
            logService.Connect();

            var xmlReader = ActivatorUtilities.CreateInstance<XmlReader<XmlTitleText>>(host.Services);
            XmlTitleText serializable = xmlReader.Read(out stat);

            // like this I have to convert between the serialized objects; seems like not optimal solution
            var jsonWriter = ActivatorUtilities.CreateInstance<JsonWriter<XmlTitleText>>(host.Services);
            jsonWriter.Write(serializable, out stat);

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
                            // builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                            services.AddTransient(typeof(IDataReader<>), typeof(XmlReader<>));
                            services.AddTransient(typeof(IDataWriter<>), typeof(JsonWriter<>));
                        })
                        .UseSerilog()
                        .Build();
            return host;
        }
    }
}
