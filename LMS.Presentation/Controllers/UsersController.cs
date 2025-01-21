using Domain.Contracts;
using Domain.Models.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace LMS.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IServiceManager _serviceManager;

    public UsersController(UserManager<ApplicationUser> userManager, IServiceManager serviceManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _userManager.Users.ToList();

        var userDetails = new List<object>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userDetails.Add(new
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            });
        }

        return Ok(userDetails);
    }

    // GET: api/users/{userId}
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = $"User with ID '{userId}' not found." });

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(new
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles
        });
    }

    // DELETE: api/users
    [HttpDelete]
    public async Task<IActionResult> DeleteAllUsers()
    {
        var users = _userManager.Users.ToList();

        foreach (var user in users)
        {
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Failed to delete user with ID: {user.Id}");
            }
        }

        return Ok("All users have been deleted successfully.");
    }

    // DELETE: api/users/{userId}
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = $"User with ID '{userId}' not found." });

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return BadRequest(new { message = $"Failed to delete user with ID '{userId}'." });

        return Ok(new { message = $"User with ID '{userId}' has been deleted successfully." });
    }

    // GET: api/users/{userId}/roles
    [HttpGet("{userId}/roles")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        try
        {
            var roles = await _serviceManager.RoleService.GetUserRolesAsync(userId);
            return Ok(roles);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    // POST: api/users/{userId}/roles
    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AddRolesToUser(string userId, [FromBody] List<string> roles)
    {
        if (roles == null || !roles.Any())
            return BadRequest(new { message = "Roles cannot be null or empty." });

        try
        {
            await _serviceManager.RoleService.AddUserToRolesAsync(userId, roles);
            return Ok(new { message = "Roles added successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    // DELETE: api/users/{userId}/roles
    [HttpDelete("{userId}/roles")]
    public async Task<IActionResult> RemoveRolesFromUser(string userId, [FromBody] List<string> roles)
    {
        if (roles == null || !roles.Any())
            return BadRequest(new { message = "Roles cannot be null or empty." });

        try
        {
            await _serviceManager.RoleService.RemoveUserFromRolesAsync(userId, roles);
            return Ok(new { message = "Roles removed successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    // DELETE: api/users/{userId}/roles/{roleName}
    [HttpDelete("{userId}/roles/{roleName}")]
    public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return BadRequest(new { message = "Role name cannot be null or empty." });

        try
        {
            await _serviceManager.RoleService.RemoveUserFromRoleAsync(userId, roleName);
            return Ok(new { message = $"Role '{roleName}' removed successfully from user '{userId}'." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }
}