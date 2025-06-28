namespace WorkoutTracker.Shared.Dto.Pagination
{
    public class EntityParameters
    {
        public int Page { get; set; } = 0;
        public int PageCount { get; set; } = 20;
        public string? SortBy { get; set; }
        public string? SearchString { get; set; }
    }
}
