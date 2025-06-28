using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.MuscleViewModel
{
    public interface IMuscleViewModel : IEntityViewModel<Muscle>
    {
        List<MuscleDto> Muscles { get; set; }

        Task GetMuscles();
    }
}
