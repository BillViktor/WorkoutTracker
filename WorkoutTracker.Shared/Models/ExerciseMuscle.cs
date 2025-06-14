namespace WorkoutTracker.Shared.Models
{
    public class ExerciseMuscle : BaseEntity
    {
        public long ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public long MuscleId { get; set; }
        public virtual Muscle Muscle { get; set; }
    }
}
