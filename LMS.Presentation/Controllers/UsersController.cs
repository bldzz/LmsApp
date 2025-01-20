using Domain.Models.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Controllers;

[ApiController]
[Route("api/{controller}")]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
}