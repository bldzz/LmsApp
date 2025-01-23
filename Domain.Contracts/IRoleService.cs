namespace Domain.Contracts;

public interface IRoleService
{
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task AddUserToRoleAsync(string userId, string roleName);
    Task AddUserToRolesAsync(string userId, IEnumerable<string> roleNames);
    Task RemoveUserFromRoleAsync(string userId, string roleName);
    Task RemoveUserFromRolesAsync(string userId, IEnumerable<string> roleNames);
}