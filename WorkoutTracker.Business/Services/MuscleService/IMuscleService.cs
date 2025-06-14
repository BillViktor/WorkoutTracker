using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Business.Services.MuscleService
{
    public interface IMuscleService
    {
        Task<EntityResult<Muscle>> GetMuscles(EntityParameters entityParameters, CancellationToken cancellationToken);
    }
}