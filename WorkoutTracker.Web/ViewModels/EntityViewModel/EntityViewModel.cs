using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.EntityViewModel
{
    public abstract class EntityViewModel<T> : BaseViewModel, IEntityViewModel<T> where T : class
    {
        public EntityParameters EntityParameters { get; set; } = new EntityParameters();
        public EntityResult<T> Entities { get; set; }
        public abstract Task GetEntities();
        public abstract Task<bool> Add(T entity);
        public abstract Task<bool> Update(T entity);
        public abstract Task<bool> Delete(T entity);
    }
}
