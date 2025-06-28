using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Business.Services.MuscleService
{
    public interface IMuscleService
    {
        Task<List<MuscleDto>> GetMuscles(CancellationToken cancellationToken);
        Task<MuscleDto> UpdateMuscle(MuscleDto muscle, CancellationToken cancellationToken);
    }
}