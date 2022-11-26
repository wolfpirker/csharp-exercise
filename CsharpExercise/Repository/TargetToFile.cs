using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpExercise.Contracts;
using Microsoft.Extensions.Logging;

namespace CsharpExercise.Repository
{
    public class TargetToFile<T> : ITarget<MemoryStream> where T : class
    {
        private readonly ILogger<ILogService> _log;
        private readonly IAppSettingsConfig _config;

        public TargetToFile(ILogger<ILogService> log, IAppSettingsConfig config)
        {
            this._log = log;
            this._config = config;
        }

        public MemoryStream Write(MemoryStream serializableOutput, out Status stat)
        {
            string? path = String.Format(_config.GetConversionTarget()["Path"], Path.DirectorySeparatorChar);
            string? fn = String.Format(_config.GetConversionTarget()["Filename"]);

            try
            {
                using (StreamReader reader = new StreamReader(serializableOutput))
                {
                    using (StreamWriter fs = File.CreateText(Path.Combine(path, fn)))
                    {
                        // Note: possibly issues with large files;
                        // in that case reading line by line, would make sense
                        fs.Write(reader.ReadToEnd());
                    }
                }

                stat = Status.Success;
            }
            catch (Exception ex)
            {
                _log.LogError(0, ex, ex.Message);
                stat = Status.Error;
            }
            return serializableOutput;
        }
    }
}