using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VisionaryNeedsAnalyzerApp.Data.Models.Assessment;
using VisionaryNeedsAnalyzerApp.Data.Models.Common;
using VisionaryNeedsAnalyzerApp.Data.Models.Industries;
using VisionaryNeedsAnalyzerApp.Data.Models.Scores;

namespace VisionaryNeedsAnalyzerApp.Data;

public class ApplicationDbInitializer
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbInitializer(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await InitializeWithMigrationsAsync();
    }

    private async Task InitializeWithMigrationsAsync()
    {
        if (_context.Database.IsSqlServer())
        {
            await _context.Database.MigrateAsync();
        }
        else
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }

    public async Task SeedAsync()
    {
        if (!_context.Industries.Any())
        {
            var industries = new List<Industry>()
            {
                new() { Code = "100", Name = "Hospitality", Description = "Hospitality", IsActive = true, IsDeleted = false },
                new() { Code = "101", Name = "IT", Description = "IT", IsActive = true, IsDeleted = false },
                new() { Code = "102", Name = "ECommerce", Description = "ECommerce", IsActive = true, IsDeleted = false },
                new() { Code = "103", Name = "Banking", Description = "Banking", IsActive = true, IsDeleted = false }
            };

            await _context.Industries.AddRangeAsync(industries);

            await _context.SaveChangesAsync();
        }

        if (!_context.ScoreCategories.Any())
        {
            var scoreCategories = new List<ScoreCategory>()
            {
                new() { Value = "Innovation-Lagging Organization", PointsFrom = 0, PointsTo = 60, Recommendation = "Focus on building leadership alignment and improving your innovation culture by implementing leadership training programs and fostering open communication around innovation goals", IsActive = true, IsDeleted= false},
                new() { Value = "Innovation-Aware Organization", PointsFrom = 61, PointsTo = 120, Recommendation = "Yes You can do it", IsActive = true, IsDeleted= false},
                new() { Value = "Innovation-Engaged Organization", PointsFrom = 121, PointsTo = 180, Recommendation = "You have a solid foundation for innovation. Now, focus on streamlining your processes and encouraging more cross-departmental collaboration to further embed innovation", IsActive = true, IsDeleted= false},
                new() { Value = "Innovation-Driven Organization", PointsFrom = 181, PointsTo = 240, Recommendation = "", IsActive = true, IsDeleted= false},
                new() { Value = "Innovation-Integrated Leader", PointsFrom = 241, PointsTo = 300, Recommendation = "", IsActive = true, IsDeleted= false}
            };

            await _context.ScoreCategories.AddRangeAsync(scoreCategories);

            await _context.SaveChangesAsync();
        }

        if (!_context.GeneralLookUps.Any()) 
        {
            var generalLookUps = new List<GeneralLookUp>()
            {
                new() { Type = "Question_Type", Value="Multiple choice", Description="to add multiple choice question", IsActive = true, IsDeleted= false},
                new() { Type = "Question_Type", Value="True False", Description="to add true false question", IsActive = true, IsDeleted= false},
                new() { Type = "Question_Type", Value="Text", Description="to add question based on text", IsActive = true, IsDeleted= false},
                new() { Type = "Question_Type", Value="Percentage", Description="to add question based on percentage", IsActive = true, IsDeleted= false},
                new() { Type = "Section_Type", Value="Who we are", Description="Defines what we are", IsActive = true, IsDeleted= false},
                new() { Type = "Section_Type", Value="What we do", Description="Defines what we do", IsActive = true, IsDeleted= false},
                new() { Type = "Section_Type", Value="How we do it", Description="Defines how we do it", IsActive = true, IsDeleted= false},
            };

            await _context.GeneralLookUps.AddRangeAsync(generalLookUps);

            await _context.SaveChangesAsync();
        }

        if (!_context.Assessments.Any())
        {
            var assessments = new List<Assessment>()
            {
                new() { Assessment_IndustryID = 1, Name="Quality assurance", TotalScore=300, IsActive = true, IsDeleted= false},
                new() { Assessment_IndustryID = 1, Name="Seek attention", TotalScore=300, IsActive = true, IsDeleted= false},
                new() { Assessment_IndustryID = 1, Name="How to survive", TotalScore=300, IsActive = true, IsDeleted= false}
            };

            await _context.Assessments.AddRangeAsync(assessments);

            await _context.SaveChangesAsync();
        }
    }

    public async Task InitializeRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roleNames = { "Admin", "Client" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Add a default admin user
        var adminUser = new ApplicationUser { UserName = "admin@yourdomain.com", Email = "admin@yourdomain.com", Mobile = "8954562255" };
        var createAdminUser = await userManager.CreateAsync(adminUser, "AdminPassword123!");
        if (createAdminUser.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}