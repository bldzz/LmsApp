using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record CourseCreationDto()
{
    [Required]
    [MaxLength(255, ErrorMessage = "Course name cannot exceed 255 characters long.")]
    [MinLength(3, ErrorMessage = "Course name must be at least 3 characters long.")]
    public string CourseName { get; init; }
    
    [Required]
    public DateTime StartDate { get; init; }
    
    [Required]
    public DateTime EndDate { get; init; }
    public string Description { get; init; }
}