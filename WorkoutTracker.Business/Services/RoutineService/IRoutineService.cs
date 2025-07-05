using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Routine;

namespace WorkoutTracker.Business.Services.RoutineService
{
    public interface IRoutineService
    {
        Task<RoutineDto> AddRoutine(AddRoutineDto routine, CancellationToken cancellationToken);
        Task DeleteRoutine(long id, CancellationToken cancellationToken);
        Task<RoutineDto> GetRoutine(long id, CancellationToken cancellationToken);
        Task<EntityResult<RoutineDto>> GetRoutines(EntityParameters parameters, CancellationToken cancellationToken);
        Task<RoutineDto> UpdateRoutine(long id, UpdateRoutineDto routine, CancellationToken cancellationToken);
    }
}