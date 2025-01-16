using AutoMapper;
using Domain.Models.Configuration;
using Domain.Models.Entites;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Services;

public class AuthService : IAuthService
{
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly JwtConfiguration jwtConfig;
    private readonly ILogger<AuthService> logger; // Logger field
    private ApplicationUser? user;

    public AuthService(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtConfiguration> jwtConfig,
        ILogger<AuthService> logger)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.jwtConfig = jwtConfig.Value;
        this.logger = logger; // Assign logger
    }

    public async Task<TokenDto> CreateTokenAsync(bool expireTime)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        SigningCredentials signing = GetSigningCredentials();
        IEnumerable<Claim> claims = GetClaims();
        JwtSecurityToken tokenOptions = GenerateTokenOptions(signing, claims);

        user.RefreshToken = GenerateRefreshToken();
        if (expireTime)
        {
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);
        }

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new InvalidOperationException("Failed to update user with new token information.");
        }

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new TokenDto(accessToken, user.RefreshToken!);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signing, IEnumerable<Claim> claims)
    {
        return new JwtSecurityToken(
            issuer: jwtConfig.Issuer,
            audience: jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtConfig.Expires)),
            signingCredentials: signing
        );
    }

    private IEnumerable<Claim> GetClaims()
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto registrationDto)
    {
        if (registrationDto == null)
        {
            throw new ArgumentNullException(nameof(registrationDto));
        }

        // Map the DTO to the ApplicationUser entity
        var newUser = mapper.Map<ApplicationUser>(registrationDto);

        // Create the user
        return await userManager.CreateAsync(newUser, registrationDto.Password!);
    }

    public async Task<bool> ValidateUserAsync(UserForAuthDto userForAuthDto)
    {
        if (userForAuthDto == null)
        {
            throw new ArgumentNullException(nameof(userForAuthDto));
        }

        user = await userManager.FindByNameAsync(userForAuthDto.UserName);
        return user != null && await userManager.CheckPasswordAsync(user, userForAuthDto.PassWord);
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto token)
    {
        if (string.IsNullOrWhiteSpace(token.AccessToken) || string.IsNullOrWhiteSpace(token.RefreshToken))
        {
            throw new ArgumentException("Invalid token provided.");
        }

        ClaimsPrincipal principal;
        try
        {
            principal = GetPrincipalFromExpiredToken(token.AccessToken);
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException("Invalid access token.", ex);
        }

        var username = principal.Identity?.Name;
        if (username == null)
        {
            throw new SecurityTokenException("Invalid access token: missing username.");
        }

        var user = await userManager.FindByNameAsync(username);
        if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpireTime <= DateTime.UtcNow)
        {
            throw new SecurityTokenException("Invalid or expired refresh token.");
        }

        this.user = user;
        return await CreateTokenAsync(expireTime: false);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        logger.LogInformation("Validating token. Issuer: {Issuer}, Audience: {Audience}", jwtConfig.Issuer, jwtConfig.Audience);
        logger.LogInformation("Token: {Token}", accessToken);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false, // Ignore expiration for refresh
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token signature algorithm.");
            }

            return principal;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Token validation failed.");
            throw new SecurityTokenException("Invalid access token.", ex);
        }
    }
}
