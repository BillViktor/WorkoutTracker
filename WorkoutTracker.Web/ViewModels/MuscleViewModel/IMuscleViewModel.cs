using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Web.ViewModels.Muscle
{
    public interface IMuscleViewModel
    {
        List<MuscleDto> Muscles { get; set; }

        Task GetMuscles();
    }
}
