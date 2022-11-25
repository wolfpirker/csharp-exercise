namespace csharp_exercise.contracts
{
    public enum Status
    {
        undefined, // Status not set
        success, // operation completed with success
        error // operation resulted in an error
              // in this case, there should be an exception logged

    }
}