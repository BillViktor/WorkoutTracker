namespace WorkoutTracker.Shared.Models.Routine
{
    /// <summary>
    /// Represents a day in a workout routine.
    /// </summary>
    public class WorkoutRoutineDay : BaseEntity
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }

        //Navigation property
        public long RoutineId { get; set; }
        public virtual WorkoutRoutine Routine { get; set; }

        public virtual ICollection<WorkoutRoutineDayExercise> Exercises { get; set; }
    }
}
