namespace WorkoutTracker.API.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when a user is not found in the system.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() { }
    }
}
