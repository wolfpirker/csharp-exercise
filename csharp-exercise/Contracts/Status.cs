namespace csharp_exercise.Contracts
{
    public enum Status
    {
        Undefined, // Status not set
        Success, // operation completed with success
        Error // operation resulted in an error
              // in this case, there should be an exception logged

    }
}