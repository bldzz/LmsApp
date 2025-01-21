using Domain.Contracts;
using Domain.Models.Entites;
using Microsoft.AspNetCore.Identity;

namespace LMS.Services;

public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User with ID '{userId}' was not found.");
            return await _userManager.GetRolesAsync(user);
        }

        // Maybe unused?
        public async Task AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User with ID '{userId}' was not found.");
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded) throw new InvalidOperationException($"Failed to add user to role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        public async Task AddUserToRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User with ID '{userId}' was not found.");
            var result = await _userManager.AddToRolesAsync(user, roleNames);
            if (!result.Succeeded) throw new InvalidOperationException($"Failed to add roles: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        public async Task RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User with ID '{userId}' was not found.");
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded) throw new InvalidOperationException($"Failed to remove role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        public async Task RemoveUserFromRolesAsync(string userId, IEnumerable<string> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User with ID '{userId}' was not found.");
            var result = await _userManager.RemoveFromRolesAsync(user, roleNames);
            if (!result.Succeeded) throw new InvalidOperationException($"Failed to remove roles: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }