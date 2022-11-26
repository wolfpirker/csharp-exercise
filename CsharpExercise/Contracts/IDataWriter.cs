using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CsharpExercise.Contracts
{
    public interface IDataWriter<T> where T : class
    {
        // Status enum indicates whether an operation 
        // succeeded or there was an error;
        // T: could be the object returned from DataReader
        void Write(T serializableObject, out Status stat);
    }
}