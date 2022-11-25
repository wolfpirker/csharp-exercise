namespace csharp_exercise.Contracts
{
    public enum Status
    {
        undefined, // Status not set
        success, // operation completed with success
        error // operation resulted in an error
              // in this case, there should be an exception logged

    }
}