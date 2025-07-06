namespace WorkoutTracker.Shared.Dto.RoutineDayExercise
{
    /// <summary>
    /// Data Transfer Object for updating a routine day exercise.
    /// </summary>
    public class UpdateRoutineDayExerciseDto
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int SortOrder { get; set; }
        public int RestTimeSeconds { get; set; }

        public long ExerciseId { get; set; }
    }
}
