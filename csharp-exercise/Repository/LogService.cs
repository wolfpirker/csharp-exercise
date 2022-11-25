using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csharp_exercise.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace csharp_exercise.Repository
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _log;
        private readonly IConfiguration _config;

        public LogService(ILogger<LogService> log, IConfiguration config)
        {
            this._log = log;
            this._config = config;

        }
        public void Connect()
        {
            // Reading connection string
            var cs = _config.GetValue<string>("ConnectionStrings:DefaultConnection");
            _log.LogInformation("Connection String {cs}", cs);
        }
    }
}