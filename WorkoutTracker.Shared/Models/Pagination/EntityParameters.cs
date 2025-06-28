namespace WorkoutTracker.Shared.Models.Pagination
{
    public class EntityParameters
    {
        public int Page { get; set; } = 0;
        public int PageCount { get; set; } = 20;
        public string? SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;
        public string? SearchString { get; set; } = "Barbell";
    }
}
