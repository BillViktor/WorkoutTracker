using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Shared.Dto.Exercise;

namespace WorkoutTracker.Web.Models
{
    /// <summary>
    /// Data Transfer Object for updating a muscle in the workout tracker application.
    /// </summary>
    public class UpdateExerciseClientDto
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Instructions { get; set; }

        public IBrowserFile? Image { get; set; }

        [Required]
        public long? PrimaryMuscleId { get; set; }

        public bool DeleteImage { get; set; }

        public UpdateExerciseClientDto(ExerciseDto exercise, long primaryMuscleId)
        {
            Name = exercise.Name;
            Description = exercise.Description;
            Instructions = exercise.Instructions;
            PrimaryMuscleId = primaryMuscleId;
            DeleteImage = false; // Default to false, can be set to true if the image should be deleted
        }
    }
}
