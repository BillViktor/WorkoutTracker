using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients.Muscle
{
    public interface IMuscleClient
    {
        Task<ResultModel<List<MuscleDto>>> GetMuscles();
    }
}