using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients.MuscleClient
{
    /// <summary>
    /// Interface for the MuscleClient, defining methods to interact with muscle data.
    /// </summary>
    public interface IMuscleClient
    {
        Task<ResultModel<List<MuscleDto>>> GetMuscles();
        Task<ResultModel<MuscleDto>> UpdateMuscle(MuscleDto muscle);
    }
}