using WorkoutTracker.Business.Models;
using WorkoutTracker.Shared.Dto.Exercise;
using WorkoutTracker.Shared.Dto.Pagination;

namespace WorkoutTracker.Business.Services.ExerciseService
{
    /// <summary>
    /// Interface for exercise service operations.
    /// </summary>
    public interface IExerciseService
    {
        Task<ExerciseDto> AddExercise(AddExerciseDto exercise, CancellationToken cancellationToken);
        Task DeleteExercise(long id, CancellationToken cancellationToken);
        Task<ExerciseDto> GetExercise(long id, CancellationToken cancellationToken);
        Task<EntityResult<ExerciseDto>> GetExercises(ExerciseParameters parameters, CancellationToken cancellationToken);
        Task<ExerciseDto> UpdateExercise(long id, UpdateExerciseDto exercise, CancellationToken cancellationToken);
    }
}