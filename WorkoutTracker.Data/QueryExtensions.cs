using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WorkoutTracker.Data
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeProperties<T>(
            this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes == null || includes.Length == 0)
                return query;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
