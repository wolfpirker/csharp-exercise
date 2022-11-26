using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csharp_exercise.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;
using csharp_exercise.Formats;

namespace csharp_exercise.Repository
{
    public class XmlReader<T> : IDataReader<T> where T : class
    {
        private readonly ILogger<LogService> _log;
        private readonly IConfiguration _config;


        public XmlReader(ILogger<LogService> log, IConfiguration config)
        {
            this._log = log;
            this._config = config;
        }

        public T? Read(out Status stat)
        {
            // this is when we read XML from file, how about the implementation from HTTP etc?
            // still have to think it through!
            string? path = String.Format(_config.GetValue<string>("ConversionSource:Path"), Path.DirectorySeparatorChar);
            string? fn = _config.GetValue<string>("ConversionSource:Filename");

            if (File.Exists(Path.Combine(path, fn)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlTitleText));

                try
                {
                    using (FileStream fs = new FileStream(Path.Combine(path, fn), FileMode.Open))
                    {
                        // Call the Deserialize method to restore the object's state.
                        var obj = serializer.Deserialize(fs);
                        stat = Status.Success;
                        return (T?)Convert.ChangeType(obj, typeof(T));
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(0, ex, ex.Message);
                    stat = Status.Error;
                    return null;
                }
            }
            else
            {
                _log.LogError("Source File not found");
                stat = Status.Error;
                return null;
            }
        }
    }
}