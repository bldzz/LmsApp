using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models.Entites;

namespace LMS.Infrastructure.Data;

public static class SeedData
{
    private static UserManager<ApplicationUser> _userManager = null!;
    private static RoleManager<IdentityRole> _roleManager = null!;

    public static async Task SeedDataAsync(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var db = serviceProvider.GetRequiredService<LmsContext>();

            if (await db.Users.AnyAsync()) return;

            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>() 
                           ?? throw new InvalidOperationException("UserManager service is not available.");
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>() 
                           ?? throw new InvalidOperationException("RoleManager service is not available.");

            try
            {
                // Seed roles
                await CreateRolesAsync(new[] { "Admin", "Student", "Teacher" });

                // Generate users and assign roles
                var users = await GenerateUsersAsync(5);

                // Seed courses, modules, and documents
                await SeedCoursesAndModulesAsync(db, users);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while updating the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred during database seeding.", ex);
            }
        }
    }

    private static async Task CreateRolesAsync(string[] roleNames)
    {
        foreach (var roleName in roleNames)
        {
            if (await _roleManager.RoleExistsAsync(roleName)) continue;

            var role = new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpper() };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new InvalidOperationException($"Failed to create role '{roleName}': {string.Join("\n", result.Errors.Select(e => e.Description))}");
        }
    }

    private static async Task<List<ApplicationUser>> GenerateUsersAsync(int nrOfUsers)
    {
        var faker = new Faker<ApplicationUser>("sv").Rules((f, e) =>
        {
            e.Email = f.Person.Email;
            e.UserName = f.Person.Email;
            e.FirstName = f.Name.FirstName();
            e.LastName = f.Name.LastName();
        });

        var users = faker.Generate(nrOfUsers);

        var passWord = "BytMig123!";
        if (string.IsNullOrEmpty(passWord))
            throw new InvalidOperationException("Password cannot be null or empty.");

        foreach (var user in users)
        {
            var result = await _userManager.CreateAsync(user, passWord);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create user '{user.UserName}': {string.Join("\n", result.Errors.Select(e => e.Description))}");
            }

            // Assign "Student" role to all generated users
            await _userManager.AddToRoleAsync(user, "Student");
        }

        return users;
    }
    
    private static async Task SeedCoursesAndModulesAsync(LmsContext db, List<ApplicationUser> users)
    {
        var faker = new Faker();

        // Seed courses
        var courses = new List<Course>();
        for (int i = 1; i <= 3; i++)
        {
            var course = new Course
            {
                CourseName = $"Course {i}",
                Description = faker.Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(-30 * i)
            };

            courses.Add(course);
        }

        await db.Courses.AddRangeAsync(courses);
        await db.SaveChangesAsync();

        // Seed modules
        var modules = new List<Module>();
        foreach (var course in courses)
        {
            for (int i = 1; i <= 2; i++) // Each course has 2 modules
            {
                var module = new Module
                {
                    CourseId = course.Id,
                    ModuleName = $"Module {i} for {course.CourseName}",
                    Description = faker.Lorem.Sentence(),
                    StartDate = course.StartDate.AddDays(10 * i),
                    EndDate = course.StartDate.AddDays(10 * (i + 1))
                };

                modules.Add(module);
            }
        }

        await db.Modules.AddRangeAsync(modules);
        await db.SaveChangesAsync();

        // Seed documents
        var documents = new List<Document>();
        foreach (var module in modules)
        {
            for (int i = 1; i <= 3; i++) // Each module has 3 documents
            {
                var document = new Document
                {
                    ModuleId = module.Id,
                    Name = $"Document {i} for {module.ModuleName}",
                    Description = faker.Lorem.Sentence(),
                    UploadTime = DateTime.UtcNow,
                    UserId = users[faker.Random.Int(0, users.Count - 1)].Id // Assign to a random user
                };

                documents.Add(document);
            }
        }

        await db.Documents.AddRangeAsync(documents);
        await db.SaveChangesAsync(); // Save all changes at the end
    }
}