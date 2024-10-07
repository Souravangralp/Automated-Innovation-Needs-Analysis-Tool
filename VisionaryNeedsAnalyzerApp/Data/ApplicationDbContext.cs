using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisionaryNeedsAnalyzerApp.Data.Models.Assessment;
using VisionaryNeedsAnalyzerApp.Data.Models.Common;
using VisionaryNeedsAnalyzerApp.Data.Models.Industries;
using VisionaryNeedsAnalyzerApp.Data.Models.Scores;

namespace VisionaryNeedsAnalyzerApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Industry> Industries { get; set; }

    public DbSet<Assessment> Assessments { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Option> Options { get; set; }

    public DbSet<GeneralLookUp> GeneralLookUps { get; set; }

    public DbSet<ScoreCategory> ScoreCategories { get; set; }
}
