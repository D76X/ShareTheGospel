using System.Net.Http.Json;
using System.Security.Claims;
using Client.StaticWebAppsAuthenticationExtensions.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client.StaticWebAppsAuthenticationExtensions
{
    /// <summary>
    ///
    /// Authenticating in Azure Static Web Apps - Stacy Cashmore
    /// https://www.youtube.com/watch?v=KjSY9vmGz24
    ///
    /// blazor-auth-static-web-apps by Anthony Chu
    /// https://github.com/anthonychu/blazor-auth-static-web-apps/tree/main
    /// </summary>
    public class StaticWebAppsAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IConfiguration config;
        private readonly HttpClient http;

        public StaticWebAppsAuthenticationStateProvider(IConfiguration config, IWebAssemblyHostEnvironment environment)
        {
            this.config = config;
            this.http = new HttpClient { BaseAddress = new Uri(environment.BaseAddress) };
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var authDataUrl = config.GetValue<string>("StaticWebAppsAuthentication:AuthenticationDataUrl", "/.auth/me");
                var data = await http.GetFromJsonAsync<AuthenticationData>(authDataUrl);

                var principal = data.ClientPrincipal;
                principal.UserRoles = principal.UserRoles.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);

                if (!principal.UserRoles.Any())
                {
                    return new AuthenticationState(new ClaimsPrincipal());
                }

                var identity = new ClaimsIdentity(principal.IdentityProvider);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
                identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
                identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }
    }
}
