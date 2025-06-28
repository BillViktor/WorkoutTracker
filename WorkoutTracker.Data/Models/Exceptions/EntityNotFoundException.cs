namespace WorkoutTracker.Data.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when an entity is not found in the database.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }
    }
}
