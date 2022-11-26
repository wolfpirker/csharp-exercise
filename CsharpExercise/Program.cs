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
using CsharpExercise.Contracts;
using CsharpExercise.Repository;
using CsharpExercise.Formats;

namespace Csharp.Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var xmlReader = ActivatorUtilities.CreateInstance<XmlReader<XmlTitleText>>(host.Services);
            XmlTitleText serializable = xmlReader.Read(out Status stat);

            // like this I have to convert between the serialized objects; seems like not optimal solution
            if (stat == Status.Success)
            {
                var jsonWriter = ActivatorUtilities.CreateInstance<JsonWriter<XmlTitleText>>(host.Services);
                jsonWriter.Write(serializable, out stat);
                if (stat == Status.Success)
                {
                    Console.WriteLine("Conversion succeeded");
                }
                else
                {
                    Console.WriteLine("Conversion failed, during writing file to target format");
                }
            }
            else
            {
                Console.WriteLine("Conversion failed, while Reading or Parsing file");
            }


        }

        static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
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
                            services.AddTransient<IAppSettingsConfig, AppSettingsConfig>();
                            services.AddTransient(typeof(IDataReader<>), typeof(XmlReader<>));
                            services.AddTransient(typeof(IDataWriter<>), typeof(JsonWriter<>));
                        })
                        .UseSerilog()
                        .Build();
            return host;
        }
    }
}
