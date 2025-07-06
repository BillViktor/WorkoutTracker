namespace WorkoutTracker.Shared.Dto.RoutineDayExercise
{
    /// <summary>
    /// Data Transfer Object for adding an exercise to a routine day.
    /// </summary>
    public class AddRoutineDayExerciseDto
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int SortOrder { get; set; }
        public int RestTimeSeconds { get; set; }
        public long RoutineDayId { get; set; }

        public long ExerciseId { get; set; }
    }
}
