namespace WorkoutTracker.Shared.Dto.Auth
{
    /// <summary>
    /// Represents a role claim in the authentication system.
    /// </summary>
    public class RoleClaimDto
    {
        public string? Issuer { get; set; }
        public string? OriginalIssuer { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
    }
}
