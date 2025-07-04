namespace WorkoutTracker.Shared.Dto.Exercise
{
    /// <summary>
    /// Data Transfer Object for an exercise.
    /// </summary>
    public class ExerciseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Instructions { get; set; }
        public string? ImageUrl { get; set; }
        public string PrimaryMuscle { get; set; }
    }
}
