namespace MacroMinder.Client.Authentication;

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    private readonly ILocalStorageService _localStorage;

    public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? savedToken = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(savedToken))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

        ClaimsIdentity claimsIdentity = new (ParseClaimsFromJwt(savedToken), "jwt");
        ClaimsPrincipal claims = new (claimsIdentity);

        return new AuthenticationState(claims);
    }

    public void MarkUserAsAuthenticated(string email)
    {
        Claim[] claims =
        {
            new (ClaimTypes.Name, email),
        };

        ClaimsIdentity claimsIdentity = new (claims, "apiAuth");

        ClaimsPrincipal authenticatedUser = new (claimsIdentity);
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(authenticatedUser));

        NotifyAuthenticationStateChanged(authState);
    }

    public void MarkUserAsLoggedOut()
    {
        ClaimsPrincipal anonymousUser = new (new ClaimsIdentity());
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        List<Claim> claims = new ();
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);

        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object? values);

            if (values is string roles)
            {
                if (roles.Trim().StartsWith("[", StringComparison.Ordinal))
                {
                    string[]? parsedRoles = JsonSerializer.Deserialize<string[]>(roles);

                    if (parsedRoles != null)
                    {
                        claims.AddRange(parsedRoles.Select(static parsedRole => new Claim(ClaimTypes.Role, parsedRole)));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(static kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty)));
        }

        return claims;
    }
}