using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Data.Repository
{
    public class WorkoutTrackerRepository : IWorkoutTrackerRepository
    {
        private readonly WorkoutTrackerDbContext mWorkoutTrackerDbContext;

        public WorkoutTrackerRepository(WorkoutTrackerDbContext aWorkoutTrackerDbContext)
        {
            mWorkoutTrackerDbContext = aWorkoutTrackerDbContext;
        }

        #region Generic
        public async Task<T> AddAsync<T>(T aEntity, CancellationToken aCancellationToken)
        {
            await mWorkoutTrackerDbContext.AddAsync(aEntity, aCancellationToken);
            await mWorkoutTrackerDbContext.SaveChangesAsync();
            return aEntity;
        }

        public async Task<T> UpdateAsync<T>(T aEntity, CancellationToken aCancellationToken)
        {
            mWorkoutTrackerDbContext.Update(aEntity);
            await mWorkoutTrackerDbContext.SaveChangesAsync();
            return aEntity;
        }

        public async Task<T> DeleteAsync<T>(T aEntity, CancellationToken aCancellationToken)
        {
            mWorkoutTrackerDbContext.Remove(aEntity);
            await mWorkoutTrackerDbContext.SaveChangesAsync();
            return aEntity;
        }
        #endregion


        #region Muscle
        public async Task<List<MuscleDto>> GetMuscles()
        {
            return await mWorkoutTrackerDbContext.Muscles
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Select(x => new MuscleDto
                {
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl
                }).
                ToListAsync();
        }
        #endregion
    }
}
