using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Models.Routine;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Business.Services.RoutineDayExerciseService
{
    /// <summary>
    /// Service for managing routine day exercise operations.
    /// </summary>
    public class RoutineDayExerciseService : IRoutineDayExerciseService
    {
        private readonly IWorkoutTrackerRepository workoutTrackerRepository;
        private readonly IConfiguration configuration;

        public RoutineDayExerciseService(IWorkoutTrackerRepository workoutTrackerRepository, IConfiguration configuration)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the exercise with the given id
        /// </summary>
        private async Task<RoutineDayExerciseDto> GetRoutineDayExercise(long id, CancellationToken cancellationToken)
        {
            var exercise = await workoutTrackerRepository.GetEntity<WorkoutRoutineDayExercise>(id, cancellationToken,
                includes: q => q.Include(d => d.Exercise));

            return new RoutineDayExerciseDto
            {
                Id = exercise.Id,
                ExerciseId = exercise.ExerciseId,
                ExerciseName = exercise.Exercise.Name,
                ExerciseImageUrl = exercise.Exercise.ImageUrl != null ? $"{configuration["ApiBaseUrl"]}{exercise.Exercise.ImageUrl}" : null,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                SortOrder = exercise.SortOrder
            };
        }

        /// <summary>
        /// Adds a new exercise to the database
        /// </summary>
        public async Task<RoutineDayExerciseDto> AddRoutineDay(AddRoutineDayExerciseDto exercise, CancellationToken cancellationToken)
        {
            WorkoutRoutineDayExercise newExercise = new WorkoutRoutineDayExercise
            {
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                SortOrder = exercise.SortOrder,
                ExerciseId = exercise.ExerciseId,
                RestTimeSeconds = exercise.RestTimeSeconds,
                WorkoutRoutineDayId = exercise.RoutineDayId
            };

            var result = await workoutTrackerRepository.AddAsync<WorkoutRoutineDayExercise>(newExercise, cancellationToken);

            return await GetRoutineDayExercise(result.Id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing exercise in the database
        /// </summary>
        public async Task<RoutineDayExerciseDto> UpdateRoutineDay(long id, UpdateRoutineDayExerciseDto exercise, CancellationToken cancellationToken)
        {
            var existingExercise = await workoutTrackerRepository.GetEntity<WorkoutRoutineDayExercise>(id, cancellationToken);

            //Update the other fields
            existingExercise.Sets = exercise.Sets;
            existingExercise.Reps = exercise.Reps;
            existingExercise.SortOrder = exercise.SortOrder;
            existingExercise.RestTimeSeconds = exercise.RestTimeSeconds;

            await workoutTrackerRepository.UpdateAsync(existingExercise, cancellationToken);

            return await GetRoutineDayExercise(id, cancellationToken);
        }

        /// <summary>
        /// Deletes the exercise with the given id
        /// </summary>
        public async Task DeleteRoutineDayExercise(long id, CancellationToken cancellationToken)
        {
            //Get exercise
            var exercise = await workoutTrackerRepository.GetEntity<WorkoutRoutineDayExercise>(id, cancellationToken);

            //Delete exercise
            await workoutTrackerRepository.DeleteAsync(exercise, cancellationToken);
        }
    }
}
