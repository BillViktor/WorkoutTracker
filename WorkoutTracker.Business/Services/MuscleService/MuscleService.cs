using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto;

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
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl
            }).OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Updates an existing muscle in the database
        /// </summary>
        public async Task<MuscleDto> UpdateMuscle(MuscleDto muscle, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            throw new NotImplementedException();

        }
    }
}
