using CsharpExercise.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpExercise.Contracts
{
    public interface IConverter<T> where T : class
    {
        // interface to be used for sources like files, http, etc.
        // and it should be used to deserialize the data;
        // so it 
        T? GetData(MemoryStream ms, out Status stat);
    }
}

