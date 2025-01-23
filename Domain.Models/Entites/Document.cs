using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entites
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime UploadTime { get; set; } = DateTime.UtcNow;

        // Foreign Key to User
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign Key to Course
        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        // Foreign Key to Module
        public int? ModuleId { get; set; }
        public Module? Module { get; set; }

        // Foreign Key to Activity
        public int? ActivityId { get; set; }
        public Activity? Activity { get; set; }

        [Required]
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = "application/octet-stream";
    }
}