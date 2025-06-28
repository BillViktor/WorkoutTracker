namespace WorkoutTracker.Shared.Dto.Auth
{
    /// <summary>
    /// Represents a user in the Workout Tracker application.
    /// </summary>
    public class WorkoutTrackerUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; } = new List<RoleClaimDto>();

        /// <summary>
        /// Gets the list of roles associated with the user based on their role claims.
        /// </summary>
        public List<string> Roles
        {
            get => RoleClaims.Select(rc => rc.Value).ToList();
        }
    }
}
