namespace WorkoutTracker.Shared.Dto.RoutineDayExercise
{
    public class RoutineDayExerciseDto
    {
        public long Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int SortOrder { get; set; }

        public string ExerciseName { get; set; }
        public string? ExerciseImageUrl { get; set; }
    }
}
