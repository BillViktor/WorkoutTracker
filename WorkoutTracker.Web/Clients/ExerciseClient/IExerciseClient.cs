using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    public interface IExerciseClient
    {
        Task<ResultModel<Exercise>> AddExercise(Exercise exercise);
        Task<ResultModel> DeleteExercise(long id);
        Task<ResultModel<EntityResult<Exercise>>> GetExercises(EntityParameters entityParameters);
        Task<ResultModel<Exercise>> UpdateExercise(Exercise exercise);
    }
}
