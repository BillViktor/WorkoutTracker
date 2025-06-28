using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Business.Services.MuscleService
{
    /// <summary>
    /// Interface for muscle service operations.
    /// </summary>
    public interface IMuscleService
    {
        Task<List<MuscleDto>> GetMuscles(CancellationToken cancellationToken);
        Task<MuscleDto> UpdateMuscle(MuscleDto muscle, CancellationToken cancellationToken);
    }
}