using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace csharp_exercise.contracts
{
    public interface IDataWriter
    {
        void Write(JsonDocument data);
    }
}