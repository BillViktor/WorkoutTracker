namespace WorkoutTracker.Shared.Dto.Pagination
{
    public class EntityResult<T> where T : class
    {
        public int Count { get; set; }
        public List<T> List { get; set; }
    }
}
