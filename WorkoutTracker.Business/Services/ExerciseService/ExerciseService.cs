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

        public async Task<EntityResult<Exercise>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            var exercises = await workoutTrackerRepository.GetEntities<Exercise>(entityParameters, cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            exercises.List.ForEach(x => x.ImageUrl = $"{sBaseUrl}{x.ImageUrl}");

            return exercises;
        }

        public async Task DeleteExercise(long id, CancellationToken cancellationToken)
        {
            //Get exercise
            var exercise = await workoutTrackerRepository.GetEntity<Exercise>(id, cancellationToken);

            //Delete exercise
            await workoutTrackerRepository.DeleteAsync(exercise, cancellationToken);
        }
    }
}
