using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Shared.Dto.RoutineDay
{
    /// <summary>
    /// Data Transfer Object for a routine day, representing a specific day in a workout routine with its associated exercises.
    /// </summary>
    public class RoutineDayDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public List<RoutineDayExerciseDto> Exercises { get; set; } = new List<RoutineDayExerciseDto>();

        /// <summary>
        /// Calculates the estimated duration of the routine in seconds based on the exercises and their rest times.
        /// </summary>
        public int EstimatedDurationMinutes
        {
            get
            {
                return Exercises.Sum(exercise => exercise.RestTimeSeconds * exercise.Sets + exercise.Reps * exercise.Sets * 3)/60;
            }
        }
    }
}
