using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Business.Services.MuscleService
{
    public interface IMuscleService
    {
        Task<List<MuscleDto>> GetMuscles(CancellationToken cancellationToken);
        Task<EntityResult<Muscle>> GetMuscles(EntityParameters entityParameters, CancellationToken cancellationToken);
        Task<Muscle> UpdateMuscle(Muscle muscle, CancellationToken cancellationToken);
    }
}