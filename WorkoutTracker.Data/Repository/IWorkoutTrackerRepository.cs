using System.Linq.Expressions;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto.Pagination;

namespace WorkoutTracker.Data.Repository
{
    /// <summary>
    /// Interface for the Workout Tracker repository.
    /// </summary>
    public interface IWorkoutTrackerRepository
    {
        Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken);
        Task<List<T>> AddRangeAsync<T>(List<T> entity, CancellationToken cancellationToken);
        Task DeleteAsync<T>(T entity, CancellationToken cancellationToken);
        Task<List<T>> GetEntities<T>(CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes) where T : class;
        Task<EntityResult<T>> GetEntitiesPaginated<T>(
            int page,
            int pageCount,
            CancellationToken cancellationToken,
            string? whereFilter = null,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool orderByDescending = false,
            params Expression<Func<T, object>>[] includes) where T : class;
        Task<T> GetEntity<T>(long id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes) where T : BaseEntity;
        Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken);
    }
}