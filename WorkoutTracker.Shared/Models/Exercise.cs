using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Models
{
    /// <summary>
    /// Represents an exercise.
    /// </summary>
    public class Exercise : BaseEntity
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Primary muscle is required.")]
        public long PrimaryMuscleId { get; set; }
        public Muscle PrimaryMuscle { get; set; }
    }
}
