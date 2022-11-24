using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace csharp_exercise.contracts
{
    public interface IDataReader
    {
        // TODO: decide which format to use 
        // by Read(); perhaps always decide to
        // convert to JSON, since it is easily 
        // readable and common; or think about nested Dictionaries

        JsonDocument Read(string connection); // not yet sure about Return type!
                                              // connection, could not only be a file path, but 
                                              // instead be used a connection string for databases
    }
}