using WorkoutTracker.Shared.Models;

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