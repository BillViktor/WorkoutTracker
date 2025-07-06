using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Web.Clients.RoutineDayExerciseClient
{
    public interface IRoutineDayExerciseClient
    {
        Task<ResultModel<RoutineDayExerciseDto>> AddRoutineDayExercise(AddRoutineDayExerciseDto exercise);
        Task<ResultModel> DeleteRoutineDayExercise(long id);
        Task<ResultModel<RoutineDayExerciseDto>> UpdateRoutineDayExercise(long id, UpdateRoutineDayExerciseDto exercise);
    }
}