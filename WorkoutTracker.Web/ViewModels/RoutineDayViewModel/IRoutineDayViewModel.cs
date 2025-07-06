using WorkoutTracker.Shared.Dto.RoutineDay;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.RoutineDayViewModel
{
    public interface IRoutineDayViewModel : IBaseViewModel
    {
        Task<RoutineDayDto> Add(AddRoutineDayDto day);
        Task<bool> Delete(RoutineDayDto day);
        Task<RoutineDayDto> Update(long id, UpdateRoutineDayDto day);
    }
}