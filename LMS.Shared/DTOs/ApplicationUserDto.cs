using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record ApplicationUserDto()
{
    [Required]
    public string Id { get; set; }

    [Required] 
    public string Name { get; set; }

    [Required]
    public int CourseId { get; set; }
}