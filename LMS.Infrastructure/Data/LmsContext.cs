using Domain.Models.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class LmsContext : IdentityDbContext<ApplicationUser>
{
    public LmsContext(DbContextOptions<LmsContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Map Identity tables to existing AspNet tables
        builder.Entity<IdentityRole>().ToTable("AspNetRoles");
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
        builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");

        // Seed roles (if necessary)
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Teacher", NormalizedName = "TEACHER" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Student", NormalizedName = "STUDENT" }
        );

        // Define relationships explicitly to prevent shadow properties

        // Course-Module Relationship (One-to-Many)
        builder.Entity<Module>()
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(m => m.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Module-Activity Relationship (One-to-Many)
        builder.Entity<Activity>()
            .HasOne(a => a.Module)
            .WithMany(m => m.Activities)
            .HasForeignKey(a => a.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Document-User Relationship
        builder.Entity<Document>()
            .HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Document-Course Relationship (NO ACTION to avoid cascade path issues)
        builder.Entity<Document>()
            .HasOne(d => d.Course)
            .WithMany(c => c.Documents)
            .HasForeignKey(d => d.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

        // Document-Module Relationship (NO ACTION to avoid cascade path issues)
        builder.Entity<Document>()
            .HasOne(d => d.Module)
            .WithMany(m => m.Documents)
            .HasForeignKey(d => d.ModuleId)
            .OnDelete(DeleteBehavior.NoAction);

        // Document-Activity Relationship
        builder.Entity<Document>()
            .HasOne(d => d.Activity)
            .WithMany(a => a.Documents)
            .HasForeignKey(d => d.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique constraint: Ensure a user can enroll in one course only once
        builder.Entity<UserCourse>()
            .HasIndex(uc => new { uc.UserId, uc.CourseId })
            .IsUnique();

        // Ensure modules within the same course do not overlap
        builder.Entity<Module>()
            .HasIndex(m => new { m.CourseId, m.StartDate, m.EndDate })
            .IsUnique();

        // Ensure activities within the same module do not overlap
        builder.Entity<Activity>()
            .HasIndex(a => new { a.ModuleId, a.StartTime, a.EndTime })
            .IsUnique();
    }
}
