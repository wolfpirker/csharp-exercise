using CsharpExercise.Contracts;
using CsharpExercise.Formats;
using CsharpExercise.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Csharp.Exercise
{
    static class Program
    {
        [Benchmark]
        static MemoryStream? GetStreamFromFile(IHost host)
        {
            var sourceFromFile = ActivatorUtilities.CreateInstance<SourceFromFile>(host.Services);
            MemoryStream ms = sourceFromFile.GetData(out Status stat);
            if (stat == Status.Error)
            {
                Console.WriteLine("Not possible getting StreamData");
                return null;
            }
            return ms;
        }

        [Benchmark]
        static MemoryStream? GetStreamFromSerializableXmlText(IHost host, MemoryStream? ms)
        {
            var xmlReader = ActivatorUtilities.CreateInstance<XmlReader<XmlTitleText>>(host.Services);
            XmlTitleText serializable = xmlReader.GetData(ms, out Status stat);

            if (stat == Status.Success)
            {
                var jsonWriter = ActivatorUtilities.CreateInstance<JsonWriter<XmlTitleText>>(host.Services);
                ms = jsonWriter.Write(serializable, out stat);
                // don't mind the stat variable for now
                // expect when returned result is not null, it is alright
                return ms;
            }
            return null;
        }

        [Benchmark]
        static Status WriteJsonAsFile(IHost host, MemoryStream ms)
        {
            var targetToFile = ActivatorUtilities.CreateInstance<TargetToFile<MemoryStream>>(host.Services);
            Console.WriteLine("Continue with writing file...");
            targetToFile.Write(ms, out Status stat);
            return stat;
        }



        static void Main(string[] args)
        {
            var host = AppStartup();
            var ms = GetStreamFromFile(host);

            if (ms == null)
            {
                Console.WriteLine("Conversion failed, while Reading file");
                return;
            }
            ms = GetStreamFromSerializableXmlText(host, ms);

            if (ms == null)
            {
                Console.WriteLine("Conversion failed, while Parsing file");
                return;
            }
            var stat = WriteJsonAsFile(host, ms);
            if (stat == Status.Success)
            {
                Console.WriteLine("File saved");
            }
            else
            {
                Console.WriteLine("Error while saving file!");
                Console.WriteLine("Press Enter to quit");
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
                            services.AddTransient<ISource, SourceFromFile>();
                            services.AddTransient(typeof(IConverter<>), typeof(XmlReader<>));
                            //services.AddTransient(typeof(ITarget<>), typeof(JsonWriter<>));                            
                            services.AddTransient(typeof(ITarget<>), typeof(TargetToFile<>));
                        })
                        .UseSerilog()
                        .Build();
            return host;
        }
    }
}
