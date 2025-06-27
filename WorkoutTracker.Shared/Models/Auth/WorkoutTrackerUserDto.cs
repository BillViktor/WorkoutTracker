namespace WorkoutTracker.Shared.Models.Auth
{
    public class WorkoutTrackerUserDto
    {
        public string? ProfilePictureUrl { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public List<RoleClaimModel> RoleClaims { get; set; } = new List<RoleClaimModel>();
    }
}
