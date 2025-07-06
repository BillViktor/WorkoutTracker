using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.RoutineDay
{
    /// <summary>
    /// Data Transfer Object for updating a routine day.
    /// </summary>
    public class UpdateRoutineDayDto
    {
        [Required]
        public string Name { get; set; }
        public int SortOrder { get; set; }

        //Constructors
        public UpdateRoutineDayDto() { }

        public UpdateRoutineDayDto(RoutineDayDto day)
        {
            Name = day.Name;
            SortOrder = day.SortOrder;
        }
    }
}
