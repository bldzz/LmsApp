
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
        public int? CourseId { get; set; } // Nullable FK for Course
        public Course? Course { get; set; } // Nullable navigation property

        // Foreign Key to Module
        public int? ModuleId { get; set; } // Nullable FK for Module
        public Module? Module { get; set; } // Nullable navigation property

        // Foreign Key to Activity
        public Guid? ActivityId { get; set; } // Nullable FK for Activity
        public Activity? Activity { get; set; } // Nullable navigation property
    }
}
