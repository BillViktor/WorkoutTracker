using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    public interface IExerciseViewModel : IEntityViewModel<ExerciseDto>
    {
        Task<ExerciseDto> Add(ExerciseDto exercise);
        Task<bool> Delete(ExerciseDto exercise);
        Task GetEntities();
        Task<ExerciseDto> Update(ExerciseDto exercise);
    }
}