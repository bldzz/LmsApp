using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record DocumentDto()
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public int? CourseId { get; set; }
    
    [Required]
    public int? ModuleId { get; set; }
    
    [Required]
    public Guid? ActivityId { get; set; }
}