using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Models.Routine;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto.RoutineDay;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Business.Services.RoutineDayService
{
    /// <summary>
    /// Service for managing routine operations.
    /// </summary>
    public class RoutineDayService : IRoutineDayService
    {
        private readonly IWorkoutTrackerRepository workoutTrackerRepository;
        private readonly IConfiguration configuration;

        public RoutineDayService(IWorkoutTrackerRepository workoutTrackerRepository, IConfiguration configuration)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the day with the given id
        /// </summary>
        private async Task<RoutineDayDto> GetRoutineDay(long id, CancellationToken cancellationToken)
        {
            var day = await workoutTrackerRepository.GetEntity<WorkoutRoutineDay>(id, cancellationToken,
                includes: q => q.Include(d => d.Exercises).ThenInclude(e => e.Exercise));

            return new RoutineDayDto
            {
                Id = day.Id,
                Name = day.Name,
                SortOrder = day.SortOrder,
                Exercises = day.Exercises.Select(x => new RoutineDayExerciseDto
                {
                    Id = x.Id,
                    ExerciseId = x.ExerciseId,
                    ExerciseName = x.Exercise.Name,
                    ExerciseImageUrl = x.Exercise.ImageUrl != null ? $"{configuration["WorkoutTrackerApiBaseUrl"]}{x.Exercise.ImageUrl}" : "",
                    Sets = x.Sets,
                    Reps = x.Reps,
                    SortOrder = x.SortOrder
                }).ToList()
            };
        }

        /// <summary>
        /// Adds a new day to the database
        /// </summary>
        public async Task<RoutineDayDto> AddRoutineDay(AddRoutineDayDto day, CancellationToken cancellationToken)
        {
            WorkoutRoutineDay newDay = new WorkoutRoutineDay
            {
                Name = day.Name,
                SortOrder = day.SortOrder,
                RoutineId = day.RoutineId,
            };

            var result = await workoutTrackerRepository.AddAsync<WorkoutRoutineDay>(newDay, cancellationToken);

            return await GetRoutineDay(result.Id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing day in the database
        /// </summary>
        public async Task<RoutineDayDto> UpdateRoutineDay(long id, UpdateRoutineDayDto day, CancellationToken cancellationToken)
        {
            var existingDay = await workoutTrackerRepository.GetEntity<WorkoutRoutineDay>(id, cancellationToken);

            //Update the other fields
            existingDay.Name = day.Name;
            existingDay.SortOrder = day.SortOrder;

            await workoutTrackerRepository.UpdateAsync(existingDay, cancellationToken);

            return await GetRoutineDay(id, cancellationToken);
        }

        /// <summary>
        /// Deletes the day with the given id
        /// </summary>
        public async Task DeleteRoutineDay(long id, CancellationToken cancellationToken)
        {
            //Get day
            var day = await workoutTrackerRepository.GetEntity<WorkoutRoutineDay>(id, cancellationToken);

            //Delete day
            await workoutTrackerRepository.DeleteAsync(day, cancellationToken);
        }
    }
}
