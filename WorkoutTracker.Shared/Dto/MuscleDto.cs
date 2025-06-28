namespace WorkoutTracker.Shared.Dto
{
    /// <summary>
    /// Data Transfer Object for a muscle.
    /// </summary>
    public class MuscleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
