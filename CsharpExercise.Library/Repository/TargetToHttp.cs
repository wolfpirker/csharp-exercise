using CsharpExercise.Contracts;

namespace CsharpExercise.Repository
{
    public class TargetToHttp<T> : ITarget<T> where T : class
    {
        public MemoryStream Write(T serializableObject, out Status stat)
        {
            throw new NotImplementedException();
        }
    }
}