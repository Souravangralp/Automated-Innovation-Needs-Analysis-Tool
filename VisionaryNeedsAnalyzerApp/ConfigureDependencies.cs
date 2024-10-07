using VisionaryNeedsAnalyzerApp.Client.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Client.Repositories.Services;
using VisionaryNeedsAnalyzerApp.Data;
using VisionaryNeedsAnalyzerApp.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Repositories.Services;

namespace VisionaryNeedsAnalyzerApp;

public static class ConfigureDependencies
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services) 
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<ApplicationDbInitializer>();
        services.AddScoped<IIndustryService, IndustryService>();
        services.AddScoped<IAssessmentService, AssessmentService>();
        services.AddHttpClient();
        services.AddScoped<IIndustryClientService, IndustryClientService>();
        services.AddScoped<IAssessmentClientService, AssessmentClientService>();

        return services;
    }
}
