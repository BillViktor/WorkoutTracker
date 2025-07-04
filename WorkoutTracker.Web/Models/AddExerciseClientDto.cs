using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Web.Models
{
    /// <summary>
    /// Data Transfer Object for adding a new exercise in the workout tracker application.
    /// </summary>
    public class AddExerciseClientDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Instructions { get; set; }

        public IBrowserFile? Image { get; set; }

        [Required]
        public long? PrimaryMuscleId { get; set; }
    }
}
