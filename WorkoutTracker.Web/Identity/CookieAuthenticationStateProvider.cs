using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Web.Clients.AuthClient;

namespace WorkoutTracker.Web.Identity
{
    public class CookieAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthClient authClient;
        private bool authenticated = false;
        private readonly ClaimsPrincipal unauthenticated = new(new ClaimsIdentity());
        private WorkoutTrackerUserDto userInfo;

        public WorkoutTrackerUserDto UserInfo
        {
            get => userInfo;
        }

        public CookieAuthenticationStateProvider(IAuthClient authClient)
        {
            this.authClient = authClient;
        }
        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            authenticated = false;

            var user = unauthenticated;
            try
            {
                var userResponse = await authClient.Info();

                if (userResponse.Success)
                {
                    var sClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userResponse.ResultObject.UserName),
                        new Claim(ClaimTypes.Email, userResponse.ResultObject.Email)
                    };

                    foreach (var sRole in userResponse.ResultObject.RoleClaims)
                    {
                        if (!string.IsNullOrEmpty(sRole.Type) && !string.IsNullOrEmpty(sRole.Value))
                        {
                            sClaims.Add(new Claim(sRole.Type, sRole.Value, sRole.ValueType, sRole.Issuer, sRole.OriginalIssuer));
                        }
                    }

                    var sId = new ClaimsIdentity(sClaims, nameof(CookieAuthenticationStateProvider));
                    user = new ClaimsPrincipal(sId);
                    userInfo = userResponse.ResultObject;
                    authenticated = true;
                }
            }
            catch { }

            return new AuthenticationState(user);
        }
    }
}
