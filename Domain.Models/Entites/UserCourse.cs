using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entites
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; } // Changed from Guid to int

        [Required]
        public string UserId { get; set; } // Foreign Key to User
        public ApplicationUser User { get; set; }

        [Required]
        public int CourseId { get; set; } // Foreign Key to Course
        public Course Course { get; set; }
    }
}