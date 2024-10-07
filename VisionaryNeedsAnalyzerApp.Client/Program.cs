using VisionaryNeedsAnalyzerApp.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using VisionaryNeedsAnalyzerApp.Client.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Client.Repositories.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddSingleton<IIndustryClientService, IndustryClientService>();
builder.Services.AddScoped<IAssessmentClientService, AssessmentClientService>();

await builder.Build().RunAsync();
