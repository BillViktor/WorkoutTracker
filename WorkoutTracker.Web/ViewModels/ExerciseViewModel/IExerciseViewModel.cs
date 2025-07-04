using WorkoutTracker.Shared.Dto.Exercise;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Web.Models;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    /// <summary>
    /// ViewModel interface for managing exercises.
    /// </summary>
    public interface IExerciseViewModel : IEntityViewModel<ExerciseDto>
    {
        ExerciseParameters ExerciseParameters { get; set; }

        Task<ExerciseDto> Add(AddExerciseClientDto exercise);
        Task<bool> Delete(ExerciseDto exercise);
        Task GetEntities();
        Task<ExerciseDto> GetExercise(long id);
        Task<ExerciseDto> Update(long id, UpdateExerciseClientDto exercise);
    }
}