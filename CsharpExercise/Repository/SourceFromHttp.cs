using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpExercise.Contracts;

namespace CsharpExercise.Repository
{
    public class SourceFromHttp<T> : ISource<T> where T : class
    {
        public T? GetData(out Status stat)
        {
            throw new NotImplementedException();
        }
    }
}