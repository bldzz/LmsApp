using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record UserCourseDto()
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public int CourseId { get; set; }
}