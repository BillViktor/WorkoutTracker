using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WorkoutTracker.Web.Clients.AuthClient;

namespace WorkoutTracker.Web.Identity
{
    public class CookieAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthClient authClient;
        private bool authenticated = false;
        private readonly ClaimsPrincipal unauthenticated = new(new ClaimsIdentity());

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

            var sUser = unauthenticated;
            try
            {
                var sUserResponse = await authClient.Info();

                if (sUserResponse.Success)
                {
                    var sClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, sUserResponse.ResultObject.UserName),
                        new Claim(ClaimTypes.Email, sUserResponse.ResultObject.Email)
                    };

                    foreach (var sRole in sUserResponse.ResultObject.RoleClaims)
                    {
                        if (!string.IsNullOrEmpty(sRole.Type) && !string.IsNullOrEmpty(sRole.Value))
                        {
                            sClaims.Add(new Claim(sRole.Type, sRole.Value, sRole.ValueType, sRole.Issuer, sRole.OriginalIssuer));
                        }
                    }

                    var sId = new ClaimsIdentity(sClaims, nameof(CookieAuthenticationStateProvider));
                    sUser = new ClaimsPrincipal(sId);
                    authenticated = true;
                }
            }
            catch { }

            return new AuthenticationState(sUser);
        }
    }
}
