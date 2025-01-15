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
    public List<Guid> ActivityIds { get; set; } = new List<Guid>();
    
    [Required]
    public List<int> DocumentIds { get; set; } = new List<int>();
}