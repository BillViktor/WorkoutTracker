namespace WorkoutTracker.Shared.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
