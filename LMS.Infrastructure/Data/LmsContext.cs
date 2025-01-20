using Domain.Models.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class LmsContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public LmsContext(DbContextOptions<LmsContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Map Identity tables to existing AspNet tables
        modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");

        // Seed roles (if necessary)
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Teacher", NormalizedName = "TEACHER" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Student", NormalizedName = "STUDENT" }
        );

        // Define relationships explicitly to prevent shadow properties

        // Course-Module Relationship (One-to-Many)
        modelBuilder.Entity<Module>()
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(m => m.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Module-Activity Relationship (One-to-Many)
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.Module)
            .WithMany(m => m.Activities)
            .HasForeignKey(a => a.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Document-User Relationship
        modelBuilder.Entity<Document>()
            .HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Document-Course Relationship (NO ACTION to avoid cascade path issues)
        modelBuilder.Entity<Document>()
            .HasOne(d => d.Course)
            .WithMany(c => c.Documents)
            .HasForeignKey(d => d.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

        // Document-Module Relationship (NO ACTION to avoid cascade path issues)
        modelBuilder.Entity<Document>()
            .HasOne(d => d.Module)
            .WithMany(m => m.Documents)
            .HasForeignKey(d => d.ModuleId)
            .OnDelete(DeleteBehavior.NoAction);

        // Document-Activity Relationship
        modelBuilder.Entity<Document>()
            .HasOne(d => d.Activity)
            .WithMany(a => a.Documents)
            .HasForeignKey(d => d.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique constraint: Ensure a user can enroll in one course only once
        modelBuilder.Entity<UserCourse>()
            .HasIndex(uc => new { uc.UserId, uc.CourseId })
            .IsUnique();

        // Ensure modules within the same course do not overlap
        modelBuilder.Entity<Module>()
            .HasIndex(m => new { m.CourseId, m.StartDate, m.EndDate })
            .IsUnique();

        // Ensure activities within the same module do not overlap
        modelBuilder.Entity<Activity>()
            .HasIndex(a => new { a.ModuleId, a.StartDate, a.EndDate })
            .IsUnique();
    }
}
