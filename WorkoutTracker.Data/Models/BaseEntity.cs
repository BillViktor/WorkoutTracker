namespace WorkoutTracker.Data.Models
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
