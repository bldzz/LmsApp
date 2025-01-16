using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LMS.Shared.DTOs;

/// <summary>
/// Data Transfer Object for tokens used in authentication and refresh operations.
/// </summary>
public record TokenDto
{
    /// <summary>
    /// Access token used for authorization.
    /// </summary>
    [Required(ErrorMessage = "Access token is required.")]
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Refresh token used for obtaining a new access token.
    /// </summary>
    [Required(ErrorMessage = "Refresh token is required.")]
    public string RefreshToken { get; init; } = string.Empty;

    /// <summary>
    /// Default constructor for initializing an empty TokenDto instance.
    /// </summary>
    public TokenDto() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenDto"/> class with specified access and refresh tokens.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="refreshToken">The refresh token.</param>
    public TokenDto(string accessToken, string refreshToken)
    {
        AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken), "Access token cannot be null.");
        RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken), "Refresh token cannot be null.");
    }

    /// <summary>
    /// Validates if a token is a properly formatted JWT.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>True if the token is a valid JWT; otherwise, false.</returns>
    public bool IsValidJwt(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;
        return Regex.IsMatch(token, @"^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+$");
    }

    /// <summary>
    /// Validates both access and refresh tokens.
    /// </summary>
    /// <returns>True if both tokens are valid JWTs; otherwise, false.</returns>
    public bool IsValid()
    {
        return IsValidJwt(AccessToken) && IsValidJwt(RefreshToken);
    }
}
