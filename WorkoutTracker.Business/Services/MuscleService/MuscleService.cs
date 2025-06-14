using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Business.Services.MuscleService
{
    public class MuscleService : IMuscleService
    {
        private readonly IWorkoutTrackerRepository workoutTrackerRepository;
        private readonly IConfiguration configuration;

        public MuscleService(IWorkoutTrackerRepository workoutTrackerRepository, IConfiguration configuration)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
            this.configuration = configuration;
        }
        public async Task<EntityResult<Muscle>> GetMuscles(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            var muscles = await workoutTrackerRepository.GetEntities<Muscle>(entityParameters, cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            muscles.List.ForEach(x => x.ImageUrl = $"{sBaseUrl}{x.ImageUrl}");

            return muscles;
        }
    }
}
