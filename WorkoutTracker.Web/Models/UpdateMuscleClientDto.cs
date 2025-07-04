using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Web.Models;

/// <summary>
/// Data Transfer Object for updating a muscle in the workout tracker application.
/// </summary>
public class UpdateMuscleClientDto
{

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

    public IBrowserFile? Image { get; set; }

    public bool DeleteImage { get; set; }

    public UpdateMuscleClientDto() { }

    public UpdateMuscleClientDto(MuscleDto muscle)
    {
        Description = muscle.Description;
        DeleteImage = false; // Default to not deleting the image
    }
}
