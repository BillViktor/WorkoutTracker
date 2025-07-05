using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Routine;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.RoutineViewModel
{
    public interface IRoutineViewModel : IEntityViewModel<RoutineDto>
    {
        EntityParameters Parameters { get; set; }

        Task<RoutineDto> Add(AddRoutineDto routine);
        Task<bool> Delete(RoutineDto routine);
        Task GetEntities();
        Task<RoutineDto> GetRoutine(long id);
        Task<RoutineDto> Update(long id, UpdateRoutineDto routine);
    }
}