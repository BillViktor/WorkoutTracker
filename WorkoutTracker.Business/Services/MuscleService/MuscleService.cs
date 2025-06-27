using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;

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

        /// <summary>
        /// Returns a list of all muscles wrapped in a ResultModel.
        /// </summary>
        public async Task<List<MuscleDto>> GetMuscles(CancellationToken cancellationToken)
        {
            var muscles = await workoutTrackerRepository.GetEntities<Muscle>(cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            muscles.ForEach(x => x.ImageUrl = $"{sBaseUrl}{x.ImageUrl}");

            return muscles.Select(x => new MuscleDto
            {
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl
            }).OrderBy(x => x.Name).ToList();
        }
    }
}
