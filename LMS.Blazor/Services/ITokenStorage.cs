using LMS.Shared.DTOs;

namespace LMS.Blazor.Services;

public interface ITokenStorage
{

    /// Stores the tokens for a specific user.
    Task StoreTokensAsync(string userId, TokenDto tokens);


    /// Retrieves the stored tokens for a specific user.
    Task<TokenDto?> GetTokensAsync(string userId);


    /// Removes the tokens for a specific user.
    Task<bool> RemoveTokensAsync(string userId);


    /// Retrieves the access token for a specific user.
    Task<string?> GetAccessTokenAsync(string userId);


    /// Checks if the access token for a specific user is expired.
    Task<bool> IsAccessTokenExpiredAsync(string userId);


    /// Refreshes the tokens for a specific user using the refresh token.
    Task<TokenDto?> RefreshTokensAsync(string userId);


    Task<bool> IsTokenExpiredAsync(string userId);


    Task<string?> GetRefreshTokenAsync(string userId); 
    Task<string?> RefreshAccessTokenAsync(string refreshToken); 
}
