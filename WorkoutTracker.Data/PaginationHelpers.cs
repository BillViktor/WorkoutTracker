using System.Linq.Expressions;
using System.Reflection;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Data
{
    public static class PaginationHelpers
    {
        /// <summary>
        /// Applies sorting to the IQueryable based on the EntityParameters provided.
        /// </summary>
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, EntityParameters entityParameters)
        {
            if (string.IsNullOrWhiteSpace(entityParameters.SortBy))
                return source;

            var parameter = Expression.Parameter(typeof(T), "x");

            try
            {
                var property = Expression.PropertyOrField(parameter, entityParameters.SortBy);
                var lambda = Expression.Lambda(property, parameter);

                string methodName = entityParameters.SortDescending ? "OrderByDescending" : "OrderBy";

                var result = typeof(Queryable).GetMethods()
                    .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), property.Type)
                    .Invoke(null, new object[] { source, lambda });

                return (IQueryable<T>)result!;
            }
            catch
            {
                // Invalid sort field - return unsorted query
                return source;
            }
        }

        /// <summary>
        /// Applies filtering to the IQueryable based on the EntityParameters provided.
        /// </summary>
        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> source, EntityParameters entityParameters)
        {
            if (string.IsNullOrWhiteSpace(entityParameters.SearchString))
                return source;

            var parameter = Expression.Parameter(typeof(T), "x");
            var searchLower = Expression.Constant(entityParameters.SearchString.ToLower());

            Expression? predicate = null;

            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                           .Where(p => p.PropertyType == typeof(string)))
            {
                var propertyAccess = Expression.Property(parameter, prop);
                var toLowerCall = Expression.Call(propertyAccess, nameof(string.ToLower), null);
                var containsCall = Expression.Call(toLowerCall, nameof(string.Contains), null, searchLower);

                predicate = predicate == null
                    ? (Expression)containsCall
                    : Expression.OrElse(predicate, containsCall);
            }

            if (predicate == null)
                return source;

            var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
            return source.Where(lambda);
        }
    }
}
