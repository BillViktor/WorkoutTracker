using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Business.Services
{
    public class MuscleService : IMuscleService
    {
        private readonly IWorkoutTrackerRepository mWorkoutTrackerRepository;
        private readonly IConfiguration mConfiguration;

        public MuscleService(IWorkoutTrackerRepository aWorkoutTrackerRepository, IConfiguration aConfiguration)
        {
            mWorkoutTrackerRepository = aWorkoutTrackerRepository;
            mConfiguration = aConfiguration;
        }

        public async Task<List<MuscleDto>> GetMuscles()
        {
            var sMuscles = await mWorkoutTrackerRepository.GetMuscles();

            string sBaseUrl = mConfiguration["WorkoutTrackerApiBaseUrl"];

            sMuscles.ForEach(x => x.ImageUrl = $"{sBaseUrl}{x.ImageUrl}");

            return sMuscles;
        }
    }
}
