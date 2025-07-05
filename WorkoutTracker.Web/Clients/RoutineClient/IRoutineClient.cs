using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.Routine;

namespace WorkoutTracker.Web.Clients.RoutineClient
{
    public interface IRoutineClient
    {
        Task<ResultModel<RoutineDto>> AddRoutine(AddRoutineDto routine);
        Task<ResultModel> DeleteRoutine(long id);
        Task<ResultModel<RoutineDto>> GetRoutine(long id);
        Task<ResultModel<EntityResult<RoutineDto>>> GetRoutines(EntityParameters parameters);
        Task<ResultModel<RoutineDto>> UpdateRoutine(long id, UpdateRoutineDto routine);
    }
}