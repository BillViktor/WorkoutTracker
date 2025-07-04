using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Business.Models
{
    /// <summary>
    /// Data Transfer Object for adding a new exercise in the workout tracker application.
    /// </summary>
    public class AddExerciseDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Instructions { get; set; }

        public IFormFile? Image { get; set; }

        [Required]
        public int PrimaryMuscleId { get; set; }
    }
}
