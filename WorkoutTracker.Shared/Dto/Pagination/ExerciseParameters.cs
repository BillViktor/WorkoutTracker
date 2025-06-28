namespace WorkoutTracker.Shared.Dto.Pagination
{
    /// <summary>
    /// Represents parameters for paginated entity queries.
    /// </summary>
    public class ExerciseParameters
    {
        public int Page { get; set; } = 0;
        public int PageCount { get; set; } = 20;
        public string? ExerciseName { get; set; }
        public string? PrimaryMuscle { get; set; }
    }
}
