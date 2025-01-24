using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LMS.Shared.DTOs;

public record FileUploadDto()
{
    [Required]
    public string FileName { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    public IFormFile File { get; set; }
    public string ContentType { get; set; } = "application/octet-stream";
    public Stream FileStream { get; set; } = default!;
}