using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Business.Models
{
    public class UpdateMuscleDto
    {

        [Required]
        public string Description { get; set; }

        public IFormFile? Image { get; set; }

        public bool DeleteImage { get; set; }
    }
}
