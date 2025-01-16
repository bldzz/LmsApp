using LMS.Shared.DTOs;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace LMS.Blazor.Services;

// This service provides in-memory storage for tokens and a mechanism for refreshing them.
public class TokenStorageService : ITokenStorage
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static readonly ConcurrentDictionary<string, TokenDto> _tokenStore = new();

    public TokenStorageService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task StoreTokensAsync(string userId, TokenDto tokens)
    {
        _tokenStore[userId] = tokens;
        return Task.CompletedTask;
    }

    public Task<TokenDto?> GetTokensAsync(string userId)
    {
        _tokenStore.TryGetValue(userId, out var tokens);
        return Task.FromResult(tokens);
    }

    public Task<bool> RemoveTokensAsync(string userId)
    {
        return Task.FromResult(_tokenStore.Remove(userId, out _));
    }

    public Task<string?> GetAccessTokenAsync(string userId)
    {
        _tokenStore.TryGetValue(userId, out var tokens);
        return Task.FromResult(tokens?.AccessToken);
    }

    public async Task<string?> GetRefreshTokenAsync(string userId)
    {
        _tokenStore.TryGetValue(userId, out var tokens);
        return tokens?.RefreshToken;
    }

    public async Task<string?> RefreshAccessTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new InvalidOperationException("Refresh token is missing or invalid.");
        }

        var client = _httpClientFactory.CreateClient("APIClient");
        HttpResponseMessage response;

        try
        {
            response = await client.PostAsJsonAsync("api/token/refresh", new { refreshToken });
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to connect to the token refresh endpoint.", ex);
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to refresh tokens. Status: {response.StatusCode}. Response: {error}");
        }

        var newTokens = await response.Content.ReadFromJsonAsync<TokenDto>();
        if (newTokens == null || string.IsNullOrEmpty(newTokens.AccessToken) || string.IsNullOrEmpty(newTokens.RefreshToken))
        {
            throw new Exception("Invalid token response from API.");
        }

        return newTokens.AccessToken;
    }

    public async Task<bool> IsAccessTokenExpiredAsync(string userId)
    {
        if (_tokenStore.TryGetValue(userId, out var tokens) && tokens?.AccessToken != null)
        {
            var jwtHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            if (jwtHandler.CanReadToken(tokens.AccessToken))
            {
                var jwtToken = jwtHandler.ReadJwtToken(tokens.AccessToken);
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
        }

        return true; // Assume expired if no valid token is found
    }

    public async Task<bool> IsTokenExpiredAsync(string userId)
    {
        return await IsAccessTokenExpiredAsync(userId);
    }

    public async Task<TokenDto?> RefreshTokensAsync(string userId)
    {
        if (!_tokenStore.TryGetValue(userId, out var tokens) || string.IsNullOrEmpty(tokens?.RefreshToken))
        {
            throw new InvalidOperationException("No valid refresh token available.");
        }

        var newAccessToken = await RefreshAccessTokenAsync(tokens.RefreshToken);
        if (string.IsNullOrEmpty(newAccessToken))
        {
            throw new Exception("Failed to refresh access token.");
        }

        var newTokens = new TokenDto(newAccessToken, tokens.RefreshToken); // Assuming refresh token doesn't change
        await StoreTokensAsync(userId, newTokens);

        return newTokens;
    }
}
