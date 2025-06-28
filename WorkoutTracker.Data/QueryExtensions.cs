using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WorkoutTracker.Data
{
    /// <summary>
    /// Extension methods for IQueryable to simplify including related entities.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Includes multiple related entities in a queryable collection.
        /// </summary>
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
