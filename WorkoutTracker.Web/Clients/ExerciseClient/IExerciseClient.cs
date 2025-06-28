using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    /// <summary>
    /// Interface for managing exercises in the Workout Tracker application.
    /// </summary>
    public interface IExerciseClient
    {
        Task<ResultModel<ExerciseDto>> AddExercise(ExerciseDto exercise);
        Task<ResultModel> DeleteExercise(long id);
        Task<ResultModel<EntityResult<ExerciseDto>>> GetExercises(ExerciseParameters parameters);
        Task<ResultModel<ExerciseDto>> UpdateExercise(ExerciseDto exercise);
    }
}
