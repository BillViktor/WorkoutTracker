using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    public interface IExerciseClient
    {
        Task<bool> DeleteExercise(long id);
        Task<EntityResult<Exercise>> GetExercises(EntityParameters entityParameters);
    }
}
