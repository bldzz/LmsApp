using Domain.Models.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LMS.Shared.DTOs;

namespace LMS.Blazor.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public override int SaveChanges()
    {
        throw new InvalidOperationException("Use context in API project!!!");
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("Use context in API project!!!");
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Use context in API project!!!");
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Use context in API project!!!");
    }

    //public DbSet<LMS.Shared.DTOs.CourseDto> CourseDto { get; set; } = default!;
}
