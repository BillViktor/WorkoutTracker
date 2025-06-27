using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.EntityViewModel
{
    public interface IEntityViewModel<T> : IBaseViewModel where T : class
    {
        EntityResult<T> Entities { get; set; }
        EntityParameters EntityParameters { get; set; }

        Task<bool> Add(T entity);
        Task<bool> Delete(T entity);
        Task GetEntities();
        Task<bool> Update(T entity);
    }
}