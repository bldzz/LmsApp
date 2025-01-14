using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;
public record UserCourseCreationDto()
{

    [Required]
    public string UserId { get; set; }

    [Required]
    public int CourseId { get; set; }
}
