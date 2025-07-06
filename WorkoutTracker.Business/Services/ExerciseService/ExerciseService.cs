using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkoutTracker.Business.Models;
using WorkoutTracker.Business.Services.Image;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto.Exercise;
using WorkoutTracker.Shared.Dto.Pagination;

namespace WorkoutTracker.Business.Services.ExerciseService
{
    /// <summary>
    /// Service for managing exercises in the workout tracker application.
    /// </summary>
    public class ExerciseService : IExerciseService
    {
        private readonly IWorkoutTrackerRepository workoutTrackerRepository;
        private readonly IImageService imageService;
        private readonly IConfiguration configuration;

        public ExerciseService(IWorkoutTrackerRepository workoutTrackerRepository, IImageService imageService, IConfiguration configuration)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
            this.imageService = imageService;
            this.configuration = configuration;
        }

        /// <summary>
        /// Returns a paginated, sorted and filtered list of exercises
        /// </summary>
        public async Task<EntityResult<ExerciseDto>> GetExercises(ExerciseParameters parameters, CancellationToken cancellationToken)
        {
            string? filter = null;

            //Filter by exercise name
            if (!string.IsNullOrWhiteSpace(parameters.ExerciseName))
                filter = $"Name.Contains(\"{parameters.ExerciseName}\")";

            //Filter by primary muscle
            if (!string.IsNullOrWhiteSpace(parameters.PrimaryMuscle))
            {
                if (filter != null)
                    filter += " && ";

                filter += $"PrimaryMuscle.Name.Equals(\"{parameters.PrimaryMuscle}\")";
            }

            var exercises = await workoutTrackerRepository.GetEntitiesPaginated<Exercise>(
                parameters.Page, 
                parameters.PageCount, 
                cancellationToken,
                whereFilter: filter,
                orderBy: x => x.Name, 
                includes: query => query.Include(x => x.PrimaryMuscle));

            return new EntityResult<ExerciseDto>
            {
                Count = exercises.Count,
                List = exercises.List.Select(x => new ExerciseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Instructions = x.Instructions,
                    ImageUrl = x.ImageUrl != null ? $"{configuration["WorkoutTrackerApiBaseUrl"]}{x.ImageUrl}" : "",
                    PrimaryMuscle = x.PrimaryMuscle.Name
                }).ToList(),
            };
        }

        /// <summary>
        /// Returns the exercise with the specified id
        /// </summary>
        public async Task<ExerciseDto> GetExercise(long id, CancellationToken cancellationToken)
        {
            var exercise = await workoutTrackerRepository.GetEntity<Exercise>(id, cancellationToken, query => query.Include(x => x.PrimaryMuscle));

            return new ExerciseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                Instructions = exercise.Instructions,
                ImageUrl = exercise.ImageUrl != null ? $"{configuration["WorkoutTrackerApiBaseUrl"]}{exercise.ImageUrl}" : "",
                PrimaryMuscle = exercise.PrimaryMuscle.Name
            };
        }

        /// <summary>
        /// Adds a new exercise to the database
        /// </summary>
        public async Task<ExerciseDto> AddExercise(AddExerciseDto exercise, CancellationToken cancellationToken)
        {
            Exercise newExercise = new Exercise
            {
                Name = exercise.Name,
                Description = exercise.Description,
                Instructions = exercise.Instructions,
                PrimaryMuscleId = exercise.PrimaryMuscleId,
            };

            //Upload the image if provided
            if (exercise.Image != null)
            {
                string imagePath = $"images/exercises/exercise_{Guid.NewGuid()}{Path.GetExtension(exercise.Image.FileName)}";

                await imageService.SaveImage(exercise.Image, imagePath, cancellationToken);

                newExercise.ImageUrl = imagePath;
            }

            await workoutTrackerRepository.AddAsync<Exercise>(newExercise, cancellationToken);

            return await GetExercise(newExercise.Id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing exercise in the database
        /// </summary>
        public async Task<ExerciseDto> UpdateExercise(long id, UpdateExerciseDto exercise, CancellationToken cancellationToken)
        {
            var existingExercise = await workoutTrackerRepository.GetEntity<Exercise>(id, cancellationToken);

            //Update the image if provided
            if (exercise.Image != null)
            {
                string imagePath = $"images/exercises/{Guid.NewGuid()}{Path.GetExtension(exercise.Image.FileName)}";

                await imageService.SaveImage(exercise.Image, imagePath, cancellationToken);

                //Delete the old image if it was not overwritten
                if (existingExercise.ImageUrl != null && existingExercise.ImageUrl != imagePath)
                {
                    imageService.DeleteImage(existingExercise.ImageUrl);
                }

                existingExercise.ImageUrl = imagePath;
            }
            else
            {
                //If no new image is provided, check if the image should be deleted
                if (exercise.DeleteImage && existingExercise.ImageUrl != null)
                {
                    imageService.DeleteImage(existingExercise.ImageUrl);
                    existingExercise.ImageUrl = null; //Set to null if the image is deleted
                }
            }

            //Update the other fields
            existingExercise.Name = exercise.Name;
            existingExercise.Description = exercise.Description;
            existingExercise.Instructions = exercise.Instructions;
            existingExercise.PrimaryMuscleId = exercise.PrimaryMuscleId;

            var updatedEntity = await workoutTrackerRepository.UpdateAsync(existingExercise, cancellationToken);

            return await GetExercise(existingExercise.Id, cancellationToken);
        }

        /// <summary>
        /// Deletes the exercise with the given id
        /// </summary>
        public async Task DeleteExercise(long id, CancellationToken cancellationToken)
        {
            //Get exercise
            var exercise = await workoutTrackerRepository.GetEntity<Exercise>(id, cancellationToken);

            //Delete image
            if (exercise.ImageUrl != null)
            {
                imageService.DeleteImage(exercise.ImageUrl);
            }

            //Delete exercise
            await workoutTrackerRepository.DeleteAsync(exercise, cancellationToken);
        }
    }
}
