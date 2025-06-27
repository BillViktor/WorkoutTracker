using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    public interface IExerciseClient
    {
        Task<ResultModel> DeleteExercise(long id);
        Task<ResultModel<EntityResult<Exercise>>> GetExercises(EntityParameters entityParameters);
    }
}
