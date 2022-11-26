using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpExercise.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;
using CsharpExercise.Formats;

namespace CsharpExercise.Repository
{
    public class XmlReader<T> : ISource<T> where T : class
    {
        private readonly ISource<FileStream> _decoratedSource;
        private readonly ILogger<ILogService> _log;

        public XmlReader(ILogger<ILogService> log, ISource<FileStream> decoratedSource)
        {
            this._decoratedSource = decoratedSource;
            this._log = log;

        }

        public T? GetData(out Status stat)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            try
            {
                FileStream fs = _decoratedSource.GetData(out Status stat2);
                if (stat2 != Status.Error)
                {
                    var obj = serializer.Deserialize(fs);
                    stat = Status.Success;
                    return (T?)Convert.ChangeType(obj, typeof(T));
                }
                stat = Status.Error;

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