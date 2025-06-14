﻿using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Data.Repository
{
    public interface IWorkoutTrackerRepository
    {
        Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken);
        Task DeleteAsync<T>(T entity, CancellationToken cancellationToken);
        Task<EntityResult<T>> GetEntities<T>(EntityParameters entityParameters, CancellationToken cancellationToken) where T : class;
        Task<T> GetEntity<T>(long id, CancellationToken cancellationToken) where T : BaseEntity;
        Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken);
    }
}