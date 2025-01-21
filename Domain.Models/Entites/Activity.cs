using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entites
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } // Example: "Lecture", "Assignment"

        [Required]
        [MaxLength(255)]
        public string ActivityName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? Description { get; set; }

        [Required]
        public int ModuleId { get; set; } // Foreign Key to Module
        public Module Module { get; set; }

        public ICollection<Document> Documents { get; set; } = new List<Document>(); // One-to-Many with Documents
        
        public bool IsValidTimeRange()
        {
            return EndDate > StartDate;
        }
    }
}