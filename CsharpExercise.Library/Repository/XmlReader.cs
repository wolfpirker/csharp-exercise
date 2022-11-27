using CsharpExercise.Contracts;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace CsharpExercise.Repository
{
    public class XmlReader<T> : IConverter<T> where T : class
    {
        private readonly ILogger<ILogService> _log;

        public XmlReader(ILogger<ILogService> log)
        {
            this._log = log;

        }

        public T? GetData(MemoryStream fs, out Status stat)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            try
            {
                // _decoratedSource is about the source, file, or http etc.;
                // it is expected to return a MemoryStream, when it is
                // the first to get the source; however XmlReader, JsonReader etc. 
                // should return deserialized data with a class found in Formats namespace
                fs.Position = 0;
                var obj = serializer.Deserialize(fs);
                stat = Status.Success;
                return (T?)Convert.ChangeType(obj, typeof(T));
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