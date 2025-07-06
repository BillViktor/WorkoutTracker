using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Business.Services.RoutineDayExerciseService
{
    public interface IRoutineDayExerciseService
    {
        Task<RoutineDayExerciseDto> AddRoutineDay(AddRoutineDayExerciseDto exercise, CancellationToken cancellationToken);
        Task DeleteRoutineDayExercise(long id, CancellationToken cancellationToken);
        Task<RoutineDayExerciseDto> UpdateRoutineDay(long id, UpdateRoutineDayExerciseDto exercise, CancellationToken cancellationToken);
    }
}