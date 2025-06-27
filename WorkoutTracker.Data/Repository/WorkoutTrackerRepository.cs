using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Models.Exceptions;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Data.Repository
{
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
        public async Task<EntityResult<T>> GetEntitiesPaginated<T>(EntityParameters entityParameters, CancellationToken cancellationToken) where T : class
        {
            var count = await workoutTrackerDbContext.Set<T>().CountAsync(cancellationToken);

            var list = await workoutTrackerDbContext.Set<T>()
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
        public async Task<List<T>> GetEntities<T>(CancellationToken cancellationToken) where T : class
        {
            return await workoutTrackerDbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the entity with the given type and id
        /// </summary>
        public async Task<T> GetEntity<T>(long id, CancellationToken cancellationToken)  where T : BaseEntity
        {
            var entity = await workoutTrackerDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

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
