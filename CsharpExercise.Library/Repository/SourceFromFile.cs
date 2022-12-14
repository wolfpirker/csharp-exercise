using CsharpExercise.Contracts;
using Microsoft.Extensions.Logging;

namespace CsharpExercise.Repository
{
    public class SourceFromFile : ISource
    {
        private readonly ILogger<ILogService> _log;
        private readonly IAppSettingsConfig _config;

        public SourceFromFile(ILogger<ILogService> log, IAppSettingsConfig config)
        {
            this._log = log;
            this._config = config;
        }


        public MemoryStream? GetData(out Status stat)
        {
            string? path = String.Format(_config.GetConversionSource()["Path"], Path.DirectorySeparatorChar);
            string? fn = String.Format(_config.GetConversionSource()["Filename"]);

            try
            {
                var fs = new FileStream(Path.Combine(path, fn), FileMode.Open);
                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                stat = Status.Success;
                return ms;
            }
            catch (Exception ex)
            {
                _log.LogError(0, ex, ex.Message);
                stat = Status.Error;
                return null;
            }
        }
    }
}