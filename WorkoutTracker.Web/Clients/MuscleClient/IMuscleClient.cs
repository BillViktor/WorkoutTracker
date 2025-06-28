using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients.MuscleClient
{
    public interface IMuscleClient
    {
        Task<ResultModel<List<MuscleDto>>> GetMuscles();
        Task<ResultModel<MuscleDto>> UpdateMuscle(MuscleDto muscle);
    }
}