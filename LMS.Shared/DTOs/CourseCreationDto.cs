﻿using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs;

public record CourseCreationDto()
{
    [Required]
    [MaxLength(255, ErrorMessage = "Course name cannot exceed 255 characters long.")]
    [MinLength(3, ErrorMessage = "Course name must be at least 3 characters long.")]
    public string CourseName { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
}