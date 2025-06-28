namespace WorkoutTracker.Shared.Dto.Pagination
{
    /// <summary>
    /// Represents a paginated result containing a list of entities and the total count of entities.
    /// </summary>
    public class EntityResult<T> where T : class
    {
        public int Count { get; set; }
        public List<T> List { get; set; }
    }
}
