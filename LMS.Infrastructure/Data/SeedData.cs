using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models.Entites;

namespace LMS.Infrastructure.Data;

public static class SeedData
{
    private static UserManager<ApplicationUser> userManager = null!;
    private static RoleManager<IdentityRole> roleManager = null!;
    private const string adminRole = "Admin";

    public static async Task SeedDataAsync(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var db = serviceProvider.GetRequiredService<LmsContext>();

            if (await db.Users.AnyAsync()) return;

            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>() ?? throw new ArgumentNullException(nameof(userManager));
            roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>() ?? throw new ArgumentNullException(nameof(roleManager));

            try //TODO: något går fel här när man seedar ny databas
            {
                // Seed roles
                await CreateRolesAsync(new[] { adminRole, "Student", "Teacher" });

                // Generate users and assign roles
                var users = await GenerateUsersAsync(5);

                // Seed courses, modules, and documents
                await SeedCoursesAndModulesAsync(db, users);

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error seeding database", ex);
            }
        }
    }

    private static async Task CreateRolesAsync(string[] roleNames)
    {
        foreach (var roleName in roleNames)
        {
            if (await roleManager.RoleExistsAsync(roleName)) continue;
            var role = new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpper() };
            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
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
            throw new Exception("Password not found");

        foreach (var user in users)
        {
            var result = await userManager.CreateAsync(user, passWord);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            // Assign "Student" role to all generated users
            await userManager.AddToRoleAsync(user, "Student");
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
    }
}
