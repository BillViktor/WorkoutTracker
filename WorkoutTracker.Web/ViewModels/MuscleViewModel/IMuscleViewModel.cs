using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.MuscleViewModel
{
    public interface IMuscleViewModel : IBaseViewModel
    {
        List<MuscleDto> Muscles { get; set; }
        Task GetMuscles();
    }
}
