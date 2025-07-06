using WorkoutTracker.Shared.Dto.Routine;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.RoutineDayExerciseViewModel
{
    public interface IRoutineDayExerciseViewModel : IBaseViewModel
    {
        Task<RoutineDayExerciseDto> Add(AddRoutineDayExerciseDto exercise);
        Task<bool> Delete(RoutineDayExerciseDto day);
        Task<RoutineDayExerciseDto> Update(long id, UpdateRoutineDayExerciseDto exercise);
    }
}