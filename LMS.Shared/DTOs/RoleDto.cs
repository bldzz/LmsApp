using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record RoleDto()
{
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; }
    
    public List<string> UserIds { get; set; } = new List<string>();
}