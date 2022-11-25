using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace csharp_exercise.contracts
{
    public interface IDataReader<T> where T : class
    {
        // Status enum indicates whether an operation 
        // succeeded or there was an error
        Status Read(string connection, ref T serializadInput);
    }
}