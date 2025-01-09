using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entites
{
    public class Module
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string ModuleName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int CourseId { get; set; } // Foreign Key to Course
        public Course Course { get; set; }

        public ICollection<Activity> Activities { get; set; } // One-to-Many with Activities
        public ICollection<Document> Documents { get; set; } // One-to-Many with Documents
    }
}