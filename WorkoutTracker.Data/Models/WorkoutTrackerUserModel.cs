using Microsoft.AspNetCore.Identity;

namespace WorkoutTracker.Data.Models
{
    public class WorkoutTrackerUserModel : IdentityUser
    {
        public string? ProfilePicturePath { get; set; }
    }
}
