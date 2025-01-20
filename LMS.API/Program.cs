using Domain.Models.Entites;
using LMS.API.Extensions;
using LMS.Infrastructure.Data;
using LMS.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddDbContext<LmsContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LmsContext")
            ?? throw new InvalidOperationException("Connection string 'LmsContext' not found.")));

        builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        })
        .AddNewtonsoftJson()
        .AddApplicationPart(typeof(AssemblyReference).Assembly);

        builder.Services.ConfigureOpenApi();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        builder.Services.ConfigureServiceLayerServices();
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.ConfigureCors();
        
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<LmsContext>()
        .AddDefaultTokenProviders();

        builder.Services.Configure<PasswordHasherOptions>(options => options.IterationCount = 100000);

        var app = builder.Build();

        // Seed data asynchronously before starting the app
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                await SeedData.SeedDataAsync(app);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions during seeding
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                throw;
            }
        }

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}
