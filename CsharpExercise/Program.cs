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
    static class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var sourceFromFile = ActivatorUtilities.CreateInstance<SourceFromFile<MemoryStream>>(host.Services);
            var xmlReader = ActivatorUtilities.CreateInstance<XmlReader<XmlTitleText>>(host.Services);
            sourceFromFile.GetData(out Status stat);
            if (stat == Status.Error)
            {
                Console.WriteLine("Not possible getting StreamData");
                return;
            }

            XmlTitleText serializable = xmlReader.GetData(out stat);

            if (stat == Status.Success)
            {
                var jsonWriter = ActivatorUtilities.CreateInstance<JsonWriter<XmlTitleText>>(host.Services);
                var ms = jsonWriter.Write(serializable, out stat);
                if (stat == Status.Success)
                {
                    var targetToFile = ActivatorUtilities.CreateInstance<TargetToFile<MemoryStream>>(host.Services);
                    Console.WriteLine("Continue with writing file...");
                    targetToFile.Write(ms, out stat);
                    if (stat == Status.Success) Console.WriteLine("File saved");
                    else Console.WriteLine("Error while saving file!");
                }
                else
                {
                    Console.WriteLine("Conversion failed, during conversion to target format");
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
                            services.AddTransient(typeof(ISource<>), typeof(XmlReader<>));
                            services.AddTransient(typeof(ITarget<>), typeof(JsonWriter<>));
                            services.AddTransient(typeof(ISource<>), typeof(SourceFromFile<>));
                            services.AddTransient(typeof(ITarget<>), typeof(TargetToFile<>));
                            // issues possible when there are several 
                        })
                        .UseSerilog()
                        .Build();
            return host;
        }
    }
}
