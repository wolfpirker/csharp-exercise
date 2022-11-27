using CsharpExercise.Contracts;
using Microsoft.Extensions.Logging;

namespace CsharpExercise.Repository
{
    public class SourceFromHttp<T> : ISource
    {
        private readonly ILogger<ILogService> _log;
        private readonly IAppSettingsConfig _config;

        public SourceFromHttp(ILogger<ILogService> log, IAppSettingsConfig config)
        {
            this._log = log;
            this._config = config;
        }
        public MemoryStream? GetData(out Status stat)
        {
            // first read settings including the connection string
            // to know on which URL to access the content

            // next use HttpClient() to get data of the URL,
            // convert string content to Memorystream;            
            throw new NotImplementedException();
        }
    }
}