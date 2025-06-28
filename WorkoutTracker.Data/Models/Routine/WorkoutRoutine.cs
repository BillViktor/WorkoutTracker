namespace WorkoutTracker.Data.Models.Routine
{
    /// <summary>
    /// Represents a workout routine.
    /// </summary>
    public class WorkoutRoutine : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<WorkoutRoutineDay> Days { get; set; }
    }
}
