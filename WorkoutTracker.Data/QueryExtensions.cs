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
            Func<IQueryable<T>, IQueryable<T>>? includes)
            where T : class
        {
            if (includes == null)
                return query;

            return includes(query);
        }
    }
}
