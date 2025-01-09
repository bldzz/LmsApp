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
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } // Example: "Lecture", "Assignment"

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public string Description { get; set; }

        [Required]
        public int ModuleId { get; set; } // Foreign Key to Module
        public Module Module { get; set; }
        // Initialize collection to avoid null reference

        public ICollection<Document> Documents { get; set; } = new List<Document>(); // One-to-Many with Documents

        
        /// Validates if the activity's time range is valid (EndTime > StartTime).
        
        public bool IsValidTimeRange()
        {
            return EndTime > StartTime;
        }
    }

}
