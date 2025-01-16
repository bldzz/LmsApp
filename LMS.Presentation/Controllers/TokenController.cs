using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Logging;
using Services.Contracts;

namespace LMS.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<TokenController> _logger;

    public TokenController(IAuthService authService, ILogger<TokenController> logger)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Refreshes an access token using the provided refresh token.
    /// </summary>
    /// <param name="token">The token containing the refresh token.</param>
    /// <returns>A new token if the refresh operation is successful.</returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto token)
    {
        if (token == null || string.IsNullOrEmpty(token.RefreshToken))
        {
            _logger.LogWarning("Invalid token refresh request received.");
            return BadRequest(new
            {
                errors = new
                {
                    token = "The token field is required."
                }
            });
        }

        try
        {
            _logger.LogInformation("Attempting to refresh token...");

            var newToken = await _authService.RefreshTokenAsync(token);

            if (newToken == null || string.IsNullOrEmpty(newToken.AccessToken))
            {
                _logger.LogWarning("Failed to refresh token: Invalid or null response from the auth service.");
                return Unauthorized(new
                {
                    errors = new
                    {
                        token = "Failed to refresh token. Please try again."
                    }
                });
            }

            _logger.LogInformation("Token refreshed successfully.");
            return Ok(newToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while refreshing the token.");

            // Temporary: Return exception details for debugging
            return StatusCode(500, new
            {
                errors = new
                {
                    server = ex.Message,
                    stackTrace = ex.StackTrace // Add this line to view the stack trace in the response
                }
            });
        }
    }
}
