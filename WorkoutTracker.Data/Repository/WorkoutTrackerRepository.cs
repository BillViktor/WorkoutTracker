using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Models.Exceptions;
using WorkoutTracker.Shared.Dto.Pagination;

namespace WorkoutTracker.Data.Repository
{
    /// <summary>
    /// Repository for managing workout tracker entities.
    /// </summary>
    public class WorkoutTrackerRepository : IWorkoutTrackerRepository
    {
        private readonly WorkoutTrackerDbContext workoutTrackerDbContext;

        public WorkoutTrackerRepository(WorkoutTrackerDbContext workoutTrackerDbContext)
        {
            this.workoutTrackerDbContext = workoutTrackerDbContext;
        }

        #region Generic
        /// <summary>
        /// Gets a paginated, sorted and filtered list of the given type
        /// </summary>
        public async Task<EntityResult<T>> GetEntitiesPaginated<T>(EntityParameters entityParameters, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = workoutTrackerDbContext.Set<T>().IncludeProperties(includes);

            //Apply filtering
            if (!string.IsNullOrWhiteSpace(entityParameters.SearchString))
            {
                query = query.Where(entityParameters.SearchString);
            }

            //Apply sorting
            if (!string.IsNullOrWhiteSpace(entityParameters.SortBy))
            {
                query = query.OrderBy(entityParameters.SortBy);
            }

            var count = await query.CountAsync(cancellationToken);

            var list = await query
                .Skip(entityParameters.Page * entityParameters.PageCount)
                .Take(entityParameters.PageCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new EntityResult<T>
            {
                Count = count,
                List = list
            };
        }

        /// <summary>
        /// Gets all entites with the given type
        /// </summary>
        public async Task<List<T>> GetEntities<T>(CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await workoutTrackerDbContext.Set<T>().IncludeProperties(includes).AsNoTracking().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the entity with the given type and id
        /// </summary>
        public async Task<T> GetEntity<T>(long id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)  where T : BaseEntity
        {
            var entity = await workoutTrackerDbContext.Set<T>().IncludeProperties(includes).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if(entity == null)
            {
                throw new EntityNotFoundException($"{typeof(T).Name} with ID {id} not found.");
            }

            return entity;
        }

        /// <summary>
        /// Adds the specified entity
        /// </summary>
        public async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await workoutTrackerDbContext.AddAsync(entity, cancellationToken);
            await workoutTrackerDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Adds the specified entity
        /// </summary>
        public async Task<List<T>> AddRangeAsync<T>(List<T> entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await workoutTrackerDbContext.AddRangeAsync(entity, cancellationToken);
            await workoutTrackerDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Updates the specified entity
        /// </summary>
        public async Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            workoutTrackerDbContext.Update(entity);
            await workoutTrackerDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        public async Task DeleteAsync<T>(T entity, CancellationToken cancellationToken)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            workoutTrackerDbContext.Remove(entity);
            await workoutTrackerDbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
