
using System.Net.Http;
using System.Threading.Tasks;

namespace YHMIS.AvaloniaUI.Services.API.Auth;

internal sealed class AuthApiClient(HttpClient httpClient)
{/*
    public async Task<> LoginAsync(
        LoginRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(
            "api/auth/login",
            request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }*/
}