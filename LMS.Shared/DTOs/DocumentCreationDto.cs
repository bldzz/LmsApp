using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public record DocumentCreationDto
    {
    [Required]
    public string UserId { get; set; }
    
    public int? CourseId { get; set; }
    
    public int? ModuleId { get; set; }
    
    public Guid? ActivityId { get; set; }

    }
}
