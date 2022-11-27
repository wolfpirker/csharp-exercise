using CsharpExercise.Contracts;
using CsharpExercise.ConversionHelpers;
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
                return StringMemoryStreamHelper.GenerateStreamFromString(readFormat);
            }
            catch (Exception ex)
            {
                _log.LogError(0, ex, ex.Message);
                stat = Status.Error;
            }
            return null;
        }
    }
}