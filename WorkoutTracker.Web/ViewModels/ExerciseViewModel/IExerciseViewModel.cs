using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    /// <summary>
    /// ViewModel interface for managing exercises.
    /// </summary>
    public interface IExerciseViewModel : IEntityViewModel<ExerciseDto>
    {
        ExerciseParameters ExerciseParameters { get; set; }

        Task<ExerciseDto> Add(ExerciseDto exercise);
        Task<bool> Delete(ExerciseDto exercise);
        Task GetEntities();
        Task<ExerciseDto> GetExercise(long id);
        Task<ExerciseDto> Update(ExerciseDto exercise);
    }
}