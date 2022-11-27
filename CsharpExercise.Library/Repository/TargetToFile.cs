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
                using (StreamReader reader = new StreamReader(serializableOutput, System.Text.Encoding.UTF8, true))
                {
                    using (StreamWriter fs = File.CreateText(Path.Combine(path, fn)))
                    {
                        fs.WriteLine(reader.ReadToEnd());
                    }
                }
                stat = Status.Success;
                _log.LogInformation($"JSON File saved with succcess, path {Path.GetFullPath(path)}");
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