using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entites
{
    public class Role
    {
        [Key]
        public int Id { get; set; } // Changed from Guid to int

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; } // Changed "Role" to "RoleName" for clarity

        public ICollection<ApplicationUser> Users { get; set; } // One-to-Many relationship with Users
    }

}
