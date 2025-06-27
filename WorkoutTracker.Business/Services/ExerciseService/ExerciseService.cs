using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

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
        public async Task<EntityResult<Exercise>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            var exercises = await workoutTrackerRepository.GetEntitiesPaginated<Exercise>(entityParameters, cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            exercises.List.ForEach(x => x.ImageUrl = $"{sBaseUrl}{x.ImageUrl}");

            return exercises;
        }

        /// <summary>
        /// Adds a new exercise to the database
        /// </summary>
        public async Task<Exercise> AddExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            var exerciseToReturn = await workoutTrackerRepository.AddAsync(exercise, cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            exerciseToReturn.ImageUrl = $"{sBaseUrl}{exerciseToReturn.ImageUrl}";

            return exerciseToReturn;
        }

        /// <summary>
        /// Updates an existing exercise in the database
        /// </summary>
        public async Task<Exercise> UpdateExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            var exerciseToReturn = await workoutTrackerRepository.UpdateAsync(exercise, cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            exerciseToReturn.ImageUrl = $"{sBaseUrl}{exerciseToReturn.ImageUrl}";

            return exerciseToReturn;

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
