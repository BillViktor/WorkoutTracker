namespace WorkoutTracker.Data.Models
{
    /// <summary>
    /// Represents a muscle in the workout tracker system.
    /// </summary>
    public class Muscle : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        //Navigation properties
        public virtual ICollection<Exercise> PrimaryExercises { get; set; } = null!;
    }
}
