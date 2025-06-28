namespace WorkoutTracker.API.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when an email is already confirmed.
    /// </summary>
    public class EmailAlreadyConfirmedException : Exception
    {
        public EmailAlreadyConfirmedException() : base() { }
    }
}
