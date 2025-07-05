using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.Routine
{
    /// <summary>
    /// Data Transfer Object for adding a new workout routine.
    /// </summary>
    public class AddRoutineDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
