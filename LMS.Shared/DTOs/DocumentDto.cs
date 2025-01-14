using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record DocumentDto()
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime? UploadTime { get; set; }
    public int? CourseId { get; set; }
    public int? ModuleId { get; set; }
    public Guid? ActivityId { get; set; }
}