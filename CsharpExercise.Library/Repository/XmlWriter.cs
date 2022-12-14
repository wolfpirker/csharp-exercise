using CsharpExercise.Contracts;
using Microsoft.Extensions.Logging;

namespace CsharpExercise.Repository
{
    public class XmlWriter<T> : ITarget<T> where T : class
    {
        private readonly ILogger<ILogService> _log;
        private readonly IAppSettingsConfig _config;

        public XmlWriter(ILogger<ILogService> log, IAppSettingsConfig config)
        {
            this._log = log;
            this._config = config;
        }
        public MemoryStream Write(T serializableOutput, out Status stat)
        {
            throw new NotImplementedException();
        }
    }
}