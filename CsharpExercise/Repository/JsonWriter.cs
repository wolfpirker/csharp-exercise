using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using CsharpExercise.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CsharpExercise.Repository
{
    public class JsonWriter<T> : IDataWriter<T> where T : class
    {
        private readonly ILogger<ILogService> _log;
        private readonly IAppSettingsConfig _config;

        public JsonWriter(ILogger<ILogService> log, IAppSettingsConfig config)
        {
            this._log = log;
            this._config = config;
        }
        public void Write(T serializableOutput, out Status stat)
        {
            string? path = String.Format(_config.GetConversionTarget()["Path"], Path.DirectorySeparatorChar);
            string? fn = String.Format(_config.GetConversionTarget()["Filename"]);

            try
            {
                // Note: T must be of the right Formats class like the source! Even just for Xml 
                // there could be several different formats;
                string readFormat = JsonConvert.SerializeObject(serializableOutput);

                using (StreamWriter fs = File.CreateText(Path.Combine(path, fn)))
                {
                    fs.Write(readFormat);
                }
                stat = Status.Success;
            }
            catch (Exception ex)
            {
                _log.LogError(0, ex, ex.Message);
                stat = Status.Error;
            }
        }
    }
}