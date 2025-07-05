using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.Routine
{
    /// <summary>
    /// Data Transfer Object for updating a routine.
    /// </summary>
    public class UpdateRoutineDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public UpdateRoutineDto() { }

        public UpdateRoutineDto(RoutineDto routine)
        {
            Name = routine.Name;
            Description = routine.Description;
        }
    }
}
