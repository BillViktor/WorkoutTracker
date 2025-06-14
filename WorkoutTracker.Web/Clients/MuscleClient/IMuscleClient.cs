using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Web.Clients.Muscle
{
    public interface IMuscleClient
    {
        Task<List<MuscleDto>> GetMuscles();
    }
}