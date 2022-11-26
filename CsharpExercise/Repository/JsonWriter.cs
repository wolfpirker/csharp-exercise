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
    public class JsonWriter<T> : ITarget<T> where T : class
    {
        private readonly ILogger<ILogService> _log;

        public JsonWriter(ILogger<ILogService> log)
        {
            this._log = log;
        }

        public MemoryStream Write(T serializableOutput, out Status stat)
        {
            try
            {
                // Note: T must be of the right Formats class like the source! Even just for Xml 
                // there could be several different formats;
                string readFormat = JsonConvert.SerializeObject(serializableOutput);
                stat = Status.Success;
                return GenerateStreamFromString(readFormat);
            }
            catch (Exception ex)
            {
                _log.LogError(0, ex, ex.Message);
                stat = Status.Error;
            }
            return null;
        }

        // if this method would be of use in other classes;
        // we could put it to a own class
        private static MemoryStream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}