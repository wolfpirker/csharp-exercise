using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpExercise.Contracts;
using Microsoft.Extensions.Logging;

namespace CsharpExercise.Repository
{
    public class SourceFromFile<T> : ISource<FileStream> where T : class
    {
        private readonly ILogger<ILogService> _log;
        private readonly IAppSettingsConfig _config;

        public SourceFromFile(ILogger<ILogService> log, IAppSettingsConfig config)
        {
            this._log = log;
            this._config = config;
        }


        // should return a FileStream
        public FileStream? GetData(out Status stat)
        {
            string? path = String.Format(_config.GetConversionSource()["Path"], Path.DirectorySeparatorChar);
            string? fn = String.Format(_config.GetConversionSource()["Filename"]);

            try
            {
                stat = Status.Undefined;
                return new FileStream(Path.Combine(path, fn), FileMode.Open);
            }
            catch (Exception ex)
            {
                _log.LogError(0, ex, ex.Message);
                stat = Status.Error;
                return null;
            }
            stat = Status.Error;
            return null;
        }
    }
}