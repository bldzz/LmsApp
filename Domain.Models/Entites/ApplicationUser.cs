namespace Domain.Models.Entites
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        public string? RoleId { get; set; }
        public IdentityRole Role { get; set; }

        public int? CurrentCourseId { get; set; }
        public Course CurrentCourse { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}
