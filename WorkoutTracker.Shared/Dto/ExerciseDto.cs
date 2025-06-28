namespace WorkoutTracker.Shared.Dto
{
    public class ExerciseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public MuscleDto PrimaryMuscle { get; set; }
    }
}
