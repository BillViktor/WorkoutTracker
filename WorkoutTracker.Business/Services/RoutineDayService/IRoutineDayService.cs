using WorkoutTracker.Shared.Dto.RoutineDay;

namespace WorkoutTracker.Business.Services.RoutineDayService
{
    public interface IRoutineDayService
    {
        Task<RoutineDayDto> AddRoutineDay(AddRoutineDayDto day, CancellationToken cancellationToken);
        Task DeleteRoutineDay(long id, CancellationToken cancellationToken);
        Task<RoutineDayDto> UpdateRoutineDay(long id, UpdateRoutineDayDto day, CancellationToken cancellationToken);
    }
}