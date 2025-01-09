using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entites
{
    public class Course
    {
        [Key]
        public int Id { get; set; } // Changed from Guid to int

        [Required]
        [MaxLength(255)]
        public string CourseName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public ICollection<Module> Modules { get; set; } // One-to-Many with Modules
        public ICollection<UserCourse> UserCourses { get; set; } // Many-to-Many with Users

        public ICollection<Document> Documents { get; set; } // Documents related to Course
    }

}
