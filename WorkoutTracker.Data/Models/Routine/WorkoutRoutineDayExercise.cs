namespace WorkoutTracker.Data.Models.Routine
{
    /// <summary>
    /// Represents an exercise in a workout routine day.
    /// </summary>
    public class WorkoutRoutineDayExercise : BaseEntity
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        public int RestTimeSeconds { get; set; }
        public int SortOrder { get; set; }

        //Navigation properties
        public long ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public long WorkoutRoutineDayId { get; set; }
        public virtual WorkoutRoutineDay WorkoutRoutineDay { get; set; }
    }
}
