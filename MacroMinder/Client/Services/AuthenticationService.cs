namespace MacroMinder.Client.Services;

using Blazored.LocalStorage;
using MacroMinder.Client.Authentication;
using MacroMinder.Shared.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public class AuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    private readonly HttpClient _httpClient;

    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<LoginResultDTO> Login(LoginDTO loginModel)
    {
        string loginAsJson = JsonSerializer.Serialize(loginModel);
        HttpResponseMessage response = await _httpClient.PostAsync("api/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

        string loginResultStr = await response.Content.ReadAsStringAsync();

        LoginResultDTO? loginResult = JsonSerializer.Deserialize<LoginResultDTO>(loginResultStr);

        if (loginResult == null)
        {
            return new LoginResultDTO
            {
                Successful = false,
                Error = $"Unable to deserialize the login result. Result = {loginResultStr}",
            };
        }

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await _localStorage.SetItemAsync("authToken", loginResult.Token);

        (_authenticationStateProvider as ApiAuthenticationStateProvider)?.MarkUserAsAuthenticated(loginModel.UserName);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

        return loginResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        (_authenticationStateProvider as ApiAuthenticationStateProvider)?.MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<RegisterResultDTO> Register(RegisterDTO registerModel)
    {
        HttpResponseMessage result = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);

        string strResult = await result.Content.ReadAsStringAsync();

        RegisterResultDTO? registerResult = JsonSerializer.Deserialize<RegisterResultDTO>(strResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        return registerResult
            ?? new RegisterResultDTO
               {
                   Successful = false,
                   Errors = new[]
                   {
                       $"Unable to deserialize the register result. result = {strResult}",
                   },
               };
    }
}