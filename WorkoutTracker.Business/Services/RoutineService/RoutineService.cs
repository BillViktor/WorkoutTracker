using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Models.Routine;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Routine;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Business.Services.RoutineService
{
    /// <summary>
    /// Service for managing routine operations.
    /// </summary>
    public class RoutineService : IRoutineService
    {
        private readonly IWorkoutTrackerRepository workoutTrackerRepository;

        public RoutineService(IWorkoutTrackerRepository workoutTrackerRepository)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
        }

        /// <summary>
        /// Returns a paginated, sorted and filtered list of workout routines.
        /// </summary>
        public async Task<EntityResult<RoutineDto>> GetRoutines(EntityParameters parameters, CancellationToken cancellationToken)
        {
            string? filter = null;

            //Filter by exercise name
            if (!string.IsNullOrWhiteSpace(parameters.SearchString))
                filter = $"Name.Contains(\"{parameters.SearchString}\")";

            var routines = await workoutTrackerRepository.GetEntitiesPaginated<WorkoutRoutine>(
                parameters.Page,
                parameters.PageCount,
                cancellationToken,
                whereFilter: filter,
                orderBy: x => x.Name);

            return new EntityResult<RoutineDto>
            {
                Count = routines.Count,
                List = routines.List.Select(x => new RoutineDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).ToList()
            };
        }

        /// <summary>
        /// Retrieves a specific workout routine by its ID, including its days and exercises.
        /// </summary>
        public async Task<RoutineDto> GetRoutine(long id, CancellationToken cancellationToken)
        {
            var routine = await workoutTrackerRepository.GetEntity<WorkoutRoutine>(
                id,
                cancellationToken,
                includes: x => x.Include(y => y.Days).ThenInclude(z => z.Exercises).ThenInclude(x => x.Exercise));
            if (routine == null)
                throw new KeyNotFoundException($"Routine with ID {id} not found.");
            return new RoutineDto
            {
                Id = routine.Id,
                Name = routine.Name,
                Description = routine.Description,
                Days = routine.Days.Select(day => new RoutineDayDto
                {
                    Id = day.Id,
                    Name = day.Name,
                    Exercises = day.Exercises.Select(exercise => new RoutineDayExerciseDto
                    {
                        Id = exercise.Id,
                        Sets = exercise.Sets,
                        Reps = exercise.Reps,
                        SortOrder = exercise.SortOrder,
                        ExerciseName = exercise.Exercise.Name,
                        ExerciseImageUrl = exercise.Exercise.ImageUrl
                    }).ToList()
                }).ToList()
            };
        }

        /// <summary>
        /// Adds a new routine to the database
        /// </summary>
        public async Task<RoutineDto> AddRoutine(AddRoutineDto routine, CancellationToken cancellationToken)
        {
            WorkoutRoutine newRoutine = new WorkoutRoutine
            {
                Name = routine.Name,
                Description = routine.Description,
            };

            var result = await workoutTrackerRepository.AddAsync<WorkoutRoutine>(newRoutine, cancellationToken);

            return await GetRoutine(result.Id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing routine in the database
        /// </summary>
        public async Task<RoutineDto> UpdateRoutine(long id, UpdateRoutineDto routine, CancellationToken cancellationToken)
        {
            var existingRoutine = await workoutTrackerRepository.GetEntity<WorkoutRoutine>(id, cancellationToken);

            //Update the other fields
            existingRoutine.Name = routine.Name;
            existingRoutine.Description = routine.Description;

            await workoutTrackerRepository.UpdateAsync(existingRoutine, cancellationToken);

            return await GetRoutine(existingRoutine.Id, cancellationToken);
        }

        /// <summary>
        /// Deletes the routine with the given id
        /// </summary>
        public async Task DeleteRoutine(long id, CancellationToken cancellationToken)
        {
            //Get exercise
            var routine = await workoutTrackerRepository.GetEntity<WorkoutRoutine>(id, cancellationToken);

            //Delete routine
            await workoutTrackerRepository.DeleteAsync(routine, cancellationToken);
        }
    }
}
