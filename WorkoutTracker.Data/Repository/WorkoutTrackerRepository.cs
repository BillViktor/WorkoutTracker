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
        public async Task<EntityResult<T>> GetEntitiesPaginated<T>(
            int page,
            int pageCount,
            CancellationToken cancellationToken,
            string? whereFilter = null,
            Expression<Func<T, bool>>? expressionFilter = null,
            Expression<Func<T, object>>? orderBy = null,
            bool orderByDescending = false,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            if(page < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Page number cannot be less than zero.");
            }

            if(pageCount > 100 || pageCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageCount), "Page count must be between 1 and 100");
            }

            IQueryable<T> query = workoutTrackerDbContext.Set<T>().IncludeProperties(includes);

            // Filtering
            if (expressionFilter is not null)
            {
                query = query.Where(expressionFilter);
            }
            if(!string.IsNullOrWhiteSpace(whereFilter))
            {
                query = query.Where(whereFilter);
            }

            // Sorting
            if (orderBy is not null)
            {
                query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }

            var count = await query.CountAsync(cancellationToken);

            var list = await query
                .Skip(page * pageCount)
                .Take(pageCount)
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
