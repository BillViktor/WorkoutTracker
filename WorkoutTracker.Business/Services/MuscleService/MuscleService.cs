using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto;
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

        /// <summary>
        /// Returns a paginated, sorted and filtered list of muscles
        /// </summary>
        public async Task<EntityResult<Muscle>> GetMuscles(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            var exercises = await workoutTrackerRepository.GetEntitiesPaginated<Muscle>(entityParameters, cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            exercises.List.ForEach(x => x.ImageUrl = $"{sBaseUrl}{x.ImageUrl}");

            return exercises;
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

        /// <summary>
        /// Updates an existing muscle in the database
        /// </summary>
        public async Task<Muscle> UpdateMuscle(Muscle muscle, CancellationToken cancellationToken)
        {
            var muscleToReturn = await workoutTrackerRepository.UpdateAsync(muscle, cancellationToken);

            string sBaseUrl = configuration["WorkoutTrackerApiBaseUrl"];

            muscleToReturn.ImageUrl = $"{sBaseUrl}{muscleToReturn.ImageUrl}";

            return muscleToReturn;

        }
    }
}
