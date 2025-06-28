namespace WorkoutTracker.Shared.Models
{
    public class Muscle : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        //Navigation property
        public virtual ICollection<Exercise> Exercises { get; set; } = null!;
    }
}
