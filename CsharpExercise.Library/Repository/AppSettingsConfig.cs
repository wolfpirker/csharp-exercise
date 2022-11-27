using CsharpExercise.Contracts;
using Microsoft.Extensions.Configuration;

namespace CsharpExercise.Repository
{
    public class AppSettingsConfig : IAppSettingsConfig
    {
        private readonly IConfiguration _config;


        public AppSettingsConfig(IConfiguration config)
        {
            this._config = config;
        }

        public Dictionary<string, Dictionary<string, string>> GetSerilogSettings()
        {
            // skipped implementation and test of Serilog logging
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetConversionSource()
        {
            var d = new Dictionary<string, string>();
            d["Path"] = _config.GetValue<string>("ConversionSource:Path");
            d["Filename"] = _config.GetValue<string>("ConversionSource:Filename");
            d["ConnectionString"] = _config.GetValue<string>("ConversionSource:ConnectionString");
            return d;
        }

        public Dictionary<string, string> GetConversionTarget()
        {
            var d = new Dictionary<string, string>();
            d["Path"] = _config.GetValue<string>("ConversionTarget:Path");
            d["Filename"] = _config.GetValue<string>("ConversionTarget:Filename");
            d["ConnectionString"] = _config.GetValue<string>("ConversionTarget:ConnectionString");
            return d;
        }
    }
}