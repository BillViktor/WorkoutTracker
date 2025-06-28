using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;

namespace WorkoutTracker.Business.Services.ExerciseService
{
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
        public async Task<EntityResult<ExerciseDto>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            entityParameters.SortBy = "Name asc";
            entityParameters.SearchString = $"Name.Contains(\"{entityParameters.SearchString}\")";

            var exercises = await workoutTrackerRepository.GetEntitiesPaginated<Exercise>(entityParameters, cancellationToken, e => e.PrimaryMuscle);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            return new EntityResult<ExerciseDto>
            {
                Count = exercises.Count,
                List = exercises.List.Select(x => new ExerciseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = $"{sBaseUrl}images/exercises/{x.ImageUrl}",
                    PrimaryMuscle = new MuscleDto
                    {
                        Name = x.PrimaryMuscle.Name,
                        Description = x.PrimaryMuscle.Description,
                    },
                }).ToList(),
            };
        }

        /// <summary>
        /// Returns the exercise with the specified id
        /// </summary>
        public async Task<ExerciseDto> GetExercise(long id, CancellationToken cancellationToken)
        {
            var exercise = await workoutTrackerRepository.GetEntity<Exercise>(id, cancellationToken);

            return new ExerciseDto
            {
                Id = exercise.Id,
                Description = exercise.Description,
                Name = exercise.Name,
                ImageUrl = $"{configuration["WorkoutTrackerApiBaseUrl"]}images/exercises/{exercise.ImageUrl}",
                PrimaryMuscle = new MuscleDto
                {
                    Name = exercise.PrimaryMuscle.Name,
                    Description = exercise.PrimaryMuscle.Description,
                }
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
