namespace WorkoutTracker.Shared.Models
{
    /// <summary>
    /// Represents an exercise.
    /// </summary>
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ExerciseMuscle> Muscles { get; set; }
    }
}
