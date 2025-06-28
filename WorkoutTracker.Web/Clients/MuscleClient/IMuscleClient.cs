using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients.MuscleClient
{
    public interface IMuscleClient
    {
        Task<ResultModel<List<MuscleDto>>> GetMuscles();
        Task<ResultModel<EntityResult<Muscle>>> GetMuscles(EntityParameters entityParameters);
        Task<ResultModel<Muscle>> UpdateMuscle(Muscle muscle);
    }
}