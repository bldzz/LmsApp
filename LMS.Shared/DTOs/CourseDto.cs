using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record CourseDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255, ErrorMessage = "Course name cannot exceed 255 characters long.")]
    [MinLength(3, ErrorMessage = "Course name must be at least 3 characters long.")]
    public string CourseName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }
    
    public List<ModuleDto> Modules { get; set; } = new List<ModuleDto>();
    public List<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
    public List<UserDto> Students { get; set; } = new List<UserDto>();
}