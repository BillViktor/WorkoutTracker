using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.EntityViewModel
{
    /// <summary>
    /// Base ViewModel for managing entities in the application.
    /// </summary>
    public abstract class EntityViewModel<T> : BaseViewModel, IEntityViewModel<T> where T : class
    {
        public EntityResult<T> Entities { get; set; }
        public abstract Task GetEntities();
    }
}
