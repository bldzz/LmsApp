using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record ActivityDto()
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Type { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string ActivityName { get; set; }

    [Required]
    public string Description { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public int ModuleId { get; set; }
    
    [Required]
    public List<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
}