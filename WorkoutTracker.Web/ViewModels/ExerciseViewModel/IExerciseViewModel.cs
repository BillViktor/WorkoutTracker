using WorkoutTracker.Shared.Models;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    public interface IExerciseViewModel : IEntityViewModel<Exercise>
    {
        Task<bool> Add(Exercise exercise);
        Task<bool> Delete(Exercise exercise);
        Task GetEntities();
        Task<bool> Update(Exercise exercise);
    }
}