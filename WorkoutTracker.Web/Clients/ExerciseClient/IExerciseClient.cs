using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    public interface IExerciseClient
    {
        Task<ResultModel<ExerciseDto>> AddExercise(ExerciseDto exercise);
        Task<ResultModel> DeleteExercise(long id);
        Task<ResultModel<EntityResult<ExerciseDto>>> GetExercises(EntityParameters entityParameters);
        Task<ResultModel<ExerciseDto>> UpdateExercise(ExerciseDto exercise);
    }
}
