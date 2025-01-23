namespace LMS.Shared.DTOs;

public record DocumentDto()
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime? UploadTime { get; set; }
    public int? CourseId { get; set; }
    public int? ModuleId { get; set; }
    public Guid? ActivityId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = "application/octet-stream";
    public string Name { get; set; } = string.Empty;
}