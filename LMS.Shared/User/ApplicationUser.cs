using Microsoft.AspNetCore.Identity;

namespace LMS.Shared.User;

//ApplicationUser is shared between Blazor and API
public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}