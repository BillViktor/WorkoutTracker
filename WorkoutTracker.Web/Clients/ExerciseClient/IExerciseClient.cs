using WorkoutTracker.Shared.Dto.Exercise;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Web.Models;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    /// <summary>
    /// Interface for managing exercises in the Workout Tracker application.
    /// </summary>
    public interface IExerciseClient
    {
        Task<ResultModel<ExerciseDto>> AddExercise(AddExerciseClientDto exercise);
        Task<ResultModel> DeleteExercise(long id);
        Task<ResultModel<ExerciseDto>> GetExercise(long id);
        Task<ResultModel<EntityResult<ExerciseDto>>> GetExercises(ExerciseParameters parameters);
        Task<ResultModel<ExerciseDto>> UpdateExercise(long id, UpdateExerciseClientDto exercise);
    }
}
