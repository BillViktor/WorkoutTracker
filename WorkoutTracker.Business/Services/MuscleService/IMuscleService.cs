using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Business.Services.MuscleService
{
    public interface IMuscleService
    {
        Task<List<Muscle>> GetMuscles(CancellationToken cancellationToken);
    }
}