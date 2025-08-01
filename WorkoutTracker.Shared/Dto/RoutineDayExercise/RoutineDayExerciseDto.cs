﻿namespace WorkoutTracker.Shared.Dto.RoutineDayExercise
{
    /// <summary>
    /// Data Transfer Object for a routine day exercise, representing the details of an exercise within a specific routine day.
    /// </summary>
    public class RoutineDayExerciseDto
    {
        public long Id { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int SortOrder { get; set; }
        public int RestTimeSeconds { get; set; }

        public long ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string? ExerciseImageUrl { get; set; }
    }
}
