using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record UserDto()
{
    [Required]
    public int Id { get; set; }

    [Required] 
    public string Name { get; set; }

    [Required]
    public int CourseId { get; set; }
}