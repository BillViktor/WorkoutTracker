using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Shared.Dto.Routine
{
    public class RoutineDayDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public List<RoutineDayExerciseDto> Exercises { get; set; } = new List<RoutineDayExerciseDto>();
    }
}
