using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Web.ViewModels
{
    public interface IEntityViewModel<T> where T : class
    {
        EntityResult<T> Entities { get; set; }
        EntityParameters EntityParameters { get; set; }

        Task<bool> Add(T entity);
        Task<bool> Delete(T entity);
        Task GetEntities();
        Task<bool> Update(T entity);
    }
}