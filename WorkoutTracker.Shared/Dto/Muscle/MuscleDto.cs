using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto
{
    /// <summary>
    /// Data Transfer Object for a muscle.
    /// </summary>
    public class MuscleDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public MuscleDto() { }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public MuscleDto(MuscleDto muscle)
        {
            Id = muscle.Id;
            Name = muscle.Name;
            Description = muscle.Description;
            ImageUrl = muscle.ImageUrl;
        }
    }
}
