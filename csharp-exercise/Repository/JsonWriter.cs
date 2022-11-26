using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using csharp_exercise.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace csharp_exercise.Repository
{
    public class JsonWriter<T> : IDataWriter<T> where T : class
    {
        private readonly ILogger<ILogService> _log;
        private readonly IConfiguration _config;

        public JsonWriter(ILogger<ILogService> log, IConfiguration config)
        {
            this._log = log;
            this._config = config;
        }
        public void Write(T serializableOutput, out Status stat)
        {
            string? path = String.Format(_config.GetValue<string>("ConversionTarget:Path"), Path.DirectorySeparatorChar);
            string? fn = _config.GetValue<string>("ConversionTarget:Filename");

            try
            {
                // since a serialized object from XML is different than a object serialized from a JSON
                // it is still quite messy when we add more formats! 
                var fromXml = JsonConvert.SerializeObject(serializableOutput);

                using (StreamWriter fs = File.CreateText(Path.Combine(path, fn)))
                {
                    fs.Write(fromXml);
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