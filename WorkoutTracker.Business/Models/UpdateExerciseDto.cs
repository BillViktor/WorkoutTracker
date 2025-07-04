using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Shared.Dto.Exercise;

namespace WorkoutTracker.Business.Models
{
    /// <summary>
    /// Data Transfer Object for updating a muscle in the workout tracker application.
    /// </summary>
    public class UpdateExerciseDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Instructions { get; set; }

        public IFormFile? Image { get; set; }

        [Required]
        public long PrimaryMuscleId { get; set; }

        public bool DeleteImage { get; set; }
    }
}
