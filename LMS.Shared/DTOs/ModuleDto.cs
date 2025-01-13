using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record ModuleDto()
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string ModuleName { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    public List<ActivityDto> ActivityIds { get; set; } = new List<ActivityDto>();
    
    public List<DocumentDto> DocumentIds { get; set; } = new List<DocumentDto>();
}