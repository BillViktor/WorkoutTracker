namespace WorkoutTracker.Shared.Dto
{
    /// <summary>
    /// Data Transfer Object for an exercise.
    /// </summary>
    public class ExerciseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public MuscleDto PrimaryMuscle { get; set; }
    }
}
