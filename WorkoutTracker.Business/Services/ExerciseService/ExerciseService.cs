using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;

namespace WorkoutTracker.Business.Services.ExerciseService
{
    /// <summary>
    /// Service for managing exercises in the workout tracker application.
    /// </summary>
    public class ExerciseService : IExerciseService
    {
        private readonly IWorkoutTrackerRepository workoutTrackerRepository;
        private readonly IConfiguration configuration;

        public ExerciseService(IWorkoutTrackerRepository workoutTrackerRepository, IConfiguration configuration)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// Returns a paginated, sorted and filtered list of exercises
        /// </summary>
        public async Task<EntityResult<ExerciseDto>> GetExercises(ExerciseParameters parameters, CancellationToken cancellationToken)
        {
            string? filter = null;

            //Filter by exercise name
            if (!string.IsNullOrWhiteSpace(parameters.ExerciseName))
                filter = $"Name.Contains(\"{parameters.ExerciseName}\")";

            //Filter by primary muscle
            if (!string.IsNullOrWhiteSpace(parameters.PrimaryMuscle))
            {
                if (filter != null)
                    filter += " && ";

                filter += $"PrimaryMuscle.Name.Contains(\"{parameters.PrimaryMuscle}\")";
            }

            var exercises = await workoutTrackerRepository.GetEntitiesPaginated<Exercise>(
                parameters.Page, 
                parameters.PageCount, 
                cancellationToken,
                whereFilter: filter,
                orderBy: x => x.Name, 
                includes: x => x.PrimaryMuscle);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            return new EntityResult<ExerciseDto>
            {
                Count = exercises.Count,
                List = exercises.List.Select(x => new ExerciseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Instructions = x.Instructions,
                    ImageUrl = $"{sBaseUrl}{x.ImageUrl}",
                    PrimaryMuscle = x.PrimaryMuscle.Name
                }).ToList(),
            };
        }

        /// <summary>
        /// Returns the exercise with the specified id
        /// </summary>
        public async Task<ExerciseDto> GetExercise(long id, CancellationToken cancellationToken)
        {
            var exercise = await workoutTrackerRepository.GetEntity<Exercise>(id, cancellationToken, x => x.PrimaryMuscle);

            return new ExerciseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                Instructions = exercise.Instructions,
                ImageUrl = $"{configuration["WorkoutTrackerApiBaseUrl"]}{exercise.ImageUrl}",
                PrimaryMuscle = exercise.PrimaryMuscle.Name
            };
        }

        /// <summary>
        /// Adds a new exercise to the database
        /// </summary>
        public async Task<Exercise> AddExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing exercise in the database
        /// </summary>
        public async Task<Exercise> UpdateExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            throw new NotImplementedException();

        }

        /// <summary>
        /// Deletes the exercise with the given id
        /// </summary>
        public async Task DeleteExercise(long id, CancellationToken cancellationToken)
        {
            //Get exercise
            var exercise = await workoutTrackerRepository.GetEntity<Exercise>(id, cancellationToken);

            //Delete exercise
            await workoutTrackerRepository.DeleteAsync(exercise, cancellationToken);
        }
    }
}
