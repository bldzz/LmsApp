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

            if (await db.Users.AnyAsync()) return; // Exit if users already exist

            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>() 
                           ?? throw new InvalidOperationException("UserManager service is not available.");
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>() 
                           ?? throw new InvalidOperationException("RoleManager service is not available.");

            try
            {
                // Step 1: Seed roles
                await CreateRolesAsync();

                // Step 2: Generate users and assign roles
                var users = await GenerateUsersAsync();

                // Step 3: Seed courses, modules, and documents
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

    private static async Task CreateRolesAsync()
    {
        var initialRoles = new[] { "Admin", "Student", "Teacher", "Principal" };

        foreach (var roleName in initialRoles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to create role '{roleName}': {string.Join("\n", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    private static async Task<List<ApplicationUser>> GenerateUsersAsync()
    {
        var faker = new Faker<ApplicationUser>("sv").Rules((faker, user) =>
        {
            user.Email = faker.Person.Email;
            user.UserName = faker.Person.Email;
            user.FirstName = faker.Name.FirstName();
            user.LastName = faker.Name.LastName();
        });

        var users = faker.Generate(5);

        var password = "BytMig123!";
        if (string.IsNullOrEmpty(password))
            throw new InvalidOperationException("Password cannot be null or empty.");

        for (int i = 0; i < users.Count; i++)
        {
            var user = users[i];
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create user '{user.UserName}': {string.Join("\n", result.Errors.Select(e => e.Description))}");
            }

            // Assign roles based on user index
            if (i == 0)
            {
                // First user gets both Principal and Admin roles
                await _userManager.AddToRoleAsync(user, "Principal");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else if (i == 1)
            {
                // Second user get Teacher role
                await _userManager.AddToRoleAsync(user, "Teacher");
            }
            else
            {
                // Remaining users get Student role
                await _userManager.AddToRoleAsync(user, "Student");
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            Console.WriteLine($"User {user.UserName} has the following roles: {string.Join(", ", roles)}");
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