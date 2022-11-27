namespace CsharpExercise.Contracts
{
    public interface ISource
    {
        // interface to be used for sources like files, http, etc.
        // and it should be used to deserialize the data;
        // so it 
        MemoryStream GetData(out Status stat);
    }
}