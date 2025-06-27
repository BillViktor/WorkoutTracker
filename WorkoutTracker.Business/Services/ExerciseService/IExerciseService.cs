using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Business.Services.ExerciseService
{
    public interface IExerciseService
    {
        Task<Exercise> AddExercise(Exercise exercise, CancellationToken cancellationToken);
        Task DeleteExercise(long id, CancellationToken cancellationToken);
        Task<EntityResult<Exercise>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken);
        Task<Exercise> UpdateExercise(Exercise exercise, CancellationToken cancellationToken);
    }
}