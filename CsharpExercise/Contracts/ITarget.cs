using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsharpExercise.Contracts
{
    public interface ITarget<T> where T : class
    {
        // Status enum indicates whether an operation 
        // succeeded or there was an error;
        // T: should be the serializable object returned 
        // from XmlReader or equivalent class
        MemoryStream Write(T serializableObject, out Status stat);
    }
}