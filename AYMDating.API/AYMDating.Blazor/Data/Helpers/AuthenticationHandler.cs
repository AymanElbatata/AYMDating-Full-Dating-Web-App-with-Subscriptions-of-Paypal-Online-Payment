using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace AYMDating.Blazor.Data.Helpers
{
    //public class AuthenticationHandler : DelegatingHandler
    //{
    //    private readonly ILocalStorageService _localStorageService;
    //    private readonly AuthenticationStateProvider _authStateProvider;

    //    public AuthenticationHandler(ILocalStorageService localStorageService, AuthenticationStateProvider authStateProvider)
    //    {
    //        _localStorageService = localStorageService;
    //        _authStateProvider = authStateProvider;
    //    }

    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        // Get the authentication state
    //        var authState = await _authStateProvider.GetAuthenticationStateAsync();
    //        var user = authState.User;

    //        if (user.Identity.IsAuthenticated)
    //        {
    //            // Retrieve the token from local storage
    //            var token = await _localStorageService.GetItemAsync<string>("AYMauthToken");
    //            if (!string.IsNullOrEmpty(token))
    //            {
    //                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    //            }
    //        }

    //        // Log or take action based on authentication state
    //        //Console.WriteLine($"User is authenticated: {user.Identity.IsAuthenticated}");

    //        return await base.SendAsync(request, cancellationToken);
    //    }
    //}
}
