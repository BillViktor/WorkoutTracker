using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.EntityViewModel
{
    /// <summary>
    /// Interface for ViewModels that manage entities.
    /// </summary>
    public interface IEntityViewModel<T> : IBaseViewModel where T : class
    {
        EntityResult<T> Entities { get; set; }

        Task GetEntities();
    }
}