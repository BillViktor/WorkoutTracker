using WorkoutTracker.Shared.Dto.RoutineDay;

namespace WorkoutTracker.Shared.Dto.Routine
{
    /// <summary>
    /// Data Transfer Object for a workout routine.
    /// </summary>
    public class RoutineDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RoutineDayDto> Days { get; set; } = new List<RoutineDayDto>();
    }
}
