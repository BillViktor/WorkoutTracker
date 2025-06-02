using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Data.Repository
{
    public interface IWorkoutTrackerRepository
    {
        Task<T> AddAsync<T>(T aEntity, CancellationToken aCancellationToken);
        Task<T> DeleteAsync<T>(T aEntity, CancellationToken aCancellationToken);
        Task<List<MuscleDto>> GetMuscles();
        Task<T> UpdateAsync<T>(T aEntity, CancellationToken aCancellationToken);
    }
}