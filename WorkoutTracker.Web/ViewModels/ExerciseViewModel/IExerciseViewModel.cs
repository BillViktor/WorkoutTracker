using WorkoutTracker.Shared.Models;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    public interface IExerciseViewModel : IEntityViewModel<Exercise>
    {
        Task<Exercise> Add(Exercise exercise);
        Task<bool> Delete(Exercise exercise);
        Task GetEntities();
        Task<Exercise> Update(Exercise exercise);
    }
}