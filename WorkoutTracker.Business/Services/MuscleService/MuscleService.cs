using Microsoft.Extensions.Configuration;
using WorkoutTracker.API.Models.Exceptions;
using WorkoutTracker.Business.Models;
using WorkoutTracker.Business.Services.Image;
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
        private readonly IImageService imageService;
        private readonly IConfiguration configuration;

        public MuscleService(IWorkoutTrackerRepository workoutTrackerRepository, IImageService imageService, IConfiguration configuration)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
            this.imageService = imageService;
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
        /// Updates an existing muscle in the database.
        /// Only allows editing the image and the description
        /// </summary>
        public async Task<MuscleDto> UpdateMuscle(long id, UpdateMuscleDto muscle, CancellationToken cancellationToken)
        {
            var existingMuscle = await workoutTrackerRepository.GetEntity<Muscle>(id, cancellationToken);

            //Update the image if provided
            if (muscle.Image != null)
            {
                string imagePath = $"images/muscles/{Guid.NewGuid()}{Path.GetExtension(muscle.Image.FileName)}";

                await imageService.SaveImage(muscle.Image, imagePath, cancellationToken);

                //Delete the old image if it was not overwritten
                if (existingMuscle.ImageUrl != null && existingMuscle.ImageUrl != imagePath)
                {
                    imageService.DeleteImage(existingMuscle.ImageUrl);
                }

                existingMuscle.ImageUrl = imagePath;
            }
            else
            {
                //If no new image is provided, check if the image should be deleted
                if (muscle.DeleteImage && existingMuscle.ImageUrl != null)
                {
                    imageService.DeleteImage(existingMuscle.ImageUrl);
                    existingMuscle.ImageUrl = null; //Set to null if the image is deleted
                }
            }

            //Update the muscle description
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
