using Microsoft.EntityFrameworkCore;
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
        public async Task<EntityResult<T>> GetEntities<T>(EntityParameters entityParameters, CancellationToken cancellationToken) where T : class
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

        public async Task<T> GetEntity<T>(long id, CancellationToken cancellationToken)  where T : BaseEntity
        {
            return await workoutTrackerDbContext.Set<T>().FirstAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken)
        {
            await workoutTrackerDbContext.AddAsync(entity, cancellationToken);
            await workoutTrackerDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken)
        {
            workoutTrackerDbContext.Update(entity);
            await workoutTrackerDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync<T>(T entity, CancellationToken cancellationToken)
        {
            workoutTrackerDbContext.Remove(entity);
            await workoutTrackerDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
