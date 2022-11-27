using CsharpExercise.Contracts;
using CsharpExercise.Formats;
using CsharpExercise.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Csharp.Exercise
{
    static class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var sourceFromFile = ActivatorUtilities.CreateInstance<SourceFromFile>(host.Services);
            
            MemoryStream ms = sourceFromFile.GetData(out Status stat);
            if (stat == Status.Error)
            {
                Console.WriteLine("Not possible getting StreamData");
                return;
            }

            var xmlReader = ActivatorUtilities.CreateInstance<XmlReader<XmlTitleText>>(host.Services);
            XmlTitleText serializable = xmlReader.GetData(ms, out stat);

            if (stat == Status.Success)
            {
                var jsonWriter = ActivatorUtilities.CreateInstance<JsonWriter<XmlTitleText>>(host.Services);
                ms = jsonWriter.Write(serializable, out stat);
                if (stat == Status.Success)
                {
                    var targetToFile = ActivatorUtilities.CreateInstance<TargetToFile<MemoryStream>>(host.Services);
                    Console.WriteLine("Continue with writing file...");
                    targetToFile.Write(ms, out stat);
                    if (stat == Status.Success) Console.WriteLine("File saved");
                    else Console.WriteLine("Error while saving file!");
                    Console.WriteLine("Press Enter to quit");
                    Console.ReadLine();
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
