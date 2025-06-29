using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Business.Services.MuscleService
{
    /// <summary>
    /// Service for managing muscle-related operations.
    /// </summary>
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

            muscles.ForEach(x => x.ImageUrl = $"{configuration["WorkoutTrackerApiBaseUrl"]}{x.ImageUrl}");

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
            var existingMuscle = await workoutTrackerRepository.GetEntity<Muscle>(muscle.Id, cancellationToken);

            existingMuscle.Name = muscle.Name;
            existingMuscle.Description = muscle.Description;

            var updatedEntity = await workoutTrackerRepository.UpdateAsync(existingMuscle, cancellationToken);

            return new MuscleDto
            {
                Id = updatedEntity.Id,
                Name = updatedEntity.Name,
                Description = updatedEntity.Description,
                ImageUrl = $"{configuration["WorkoutTrackerApiBaseUrl"]}{updatedEntity.ImageUrl}"
            };
        }
    }
}
