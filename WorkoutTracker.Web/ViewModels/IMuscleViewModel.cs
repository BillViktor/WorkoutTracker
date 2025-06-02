
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Web.ViewModels
{
    public interface IExerciseViewModel
    {
        List<MuscleDto> Muscles { get; set; }

        Task GetMuscles();
    }
}
