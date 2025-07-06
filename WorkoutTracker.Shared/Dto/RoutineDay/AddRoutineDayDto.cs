using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.RoutineDay
{
    /// <summary>
    /// Data Transfer Object for adding a new routine day.
    /// </summary>
    public class AddRoutineDayDto
    {
        [Required]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public long RoutineId { get; set; }
    }
}
