using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CsharpExercise.Contracts
{
    public interface IDataReader<T> where T : class
    {
        // Status enum indicates whether an operation 
        // succeeded or there was an error
        // T can be any serializable type, which should be returned on success
        // settings, like source file, should come from appsettings.json!
        T? Read(out Status stat);
    }
}