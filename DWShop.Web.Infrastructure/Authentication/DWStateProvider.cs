using Blazored.LocalStorage;
using DWShop.Client.Infrastructure.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;

namespace DWShop.Web.Infrastructure.Authentication
{
    public class DWStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IJSRuntime jSRuntime;

        public DWStateProvider(ILocalStorageService localStorageService, IJSRuntime jSRuntime)
        {
            this.localStorageService = localStorageService;
            this.jSRuntime = jSRuntime;
        }

        public void MarkAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

            var authState = Task.FromResult(new AuthenticationState(anonymousUser));


            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            byte[] ParseBase64(string payload)
            {

                payload = payload.Trim().Replace('-', '+')
                        .Replace('_', '/');

                var base64 = payload.PadRight(payload.Length + (4 - payload.Length) % 4, '=');

                return Convert.FromBase64String(base64);
            }

            var claims = new List<Claim>();
            var payload = jwt.Split(".")[1];
            var jsonBytes = ParseBase64(payload);
            var keyvaluesPairs =
                JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyvaluesPairs is not null)
            {
                keyvaluesPairs.TryGetValue(ClaimTypes.Role, out var roles);
                if (roles is not null)
                {
                    if (roles.ToString()!.Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);

                        claims.AddRange(parsedRoles!.Select(x => new Claim(ClaimTypes.Role, x)));
                    }
                    else
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                }
                keyvaluesPairs.Remove(ClaimTypes.Role);

                claims.AddRange(keyvaluesPairs.Select(x => new Claim(x.Key, x.Value.ToString()!)));
            }
            return claims;

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                string savedToken = "";

                if (BaseConfiguration.Token is not null)
                {
                    savedToken = BaseConfiguration.Token;
                }
                else if (BaseConfiguration.Token is null && localStorageService is not null)
                {
                    BaseConfiguration.Token = await localStorageService.GetItemAsync<string>(BaseConfiguration.AuthToken);
                }
               

                if (BaseConfiguration.Token is string strToken)
                {
                    savedToken = strToken;
                }
                else
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                var state = new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity(GetClaimsFromJwt(savedToken), "jwt")));

                var user = state.User;
                string? exp = user.FindFirst(x => x.Type == "exp")?.Value;

                var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));

                var diff = expTime - DateTimeOffset.UtcNow;

                if (diff.TotalMinutes >= 1)
                {
                    return state;
                }

                await localStorageService.RemoveItemAsync(BaseConfiguration.AuthToken);
                BaseConfiguration.Token = null;
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            catch (InvalidOperationException)
            {

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

        }

        public async Task StateChanged()
        {
            var authState = Task.FromResult(await GetAuthenticationStateAsync());

            NotifyAuthenticationStateChanged(authState);
        }
    }
}
