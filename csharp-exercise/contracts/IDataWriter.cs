using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace csharp_exercise.contracts
{
    public interface IDataWriter<T> where T : class
    {
        Status Write(T serializableOutput);
    }
}