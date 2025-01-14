using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs
{
    public record ActivityCreationDto
    {
    [Required]
    [MaxLength(50)]
    public string Type { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    
    [Required]
    public DateTime EndTime { get; set; }
    
    [Required]
    public int ModuleId { get; set; }
    
    [Required]
    public List<int> DocumentIds { get; set; } = new List<int>();

    }
}
