using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpExercise.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsharpExercise.Repository
{
    public class XmlWriter<T> : IDataWriter<T> where T : class
    {
        private readonly ILogger<ILogService> _log;
        private readonly IAppSettingsConfig _config;

        public XmlWriter(ILogger<ILogService> log, IAppSettingsConfig config)
        {
            this._log = log;
            this._config = config;
        }
        public void Write(T serializableOutput, out Status stat)
        {
            throw new NotImplementedException();
        }
    }
}