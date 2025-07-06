using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.RoutineDay;

namespace WorkoutTracker.Web.Clients.RoutineDayClient
{
    public interface IRoutineDayClient
    {
        Task<ResultModel<RoutineDayDto>> AddRoutineDay(AddRoutineDayDto routine);
        Task<ResultModel> DeleteRoutineDay(long id);
        Task<ResultModel<RoutineDayDto>> UpdateRoutineDay(long id, UpdateRoutineDayDto routine);
    }
}