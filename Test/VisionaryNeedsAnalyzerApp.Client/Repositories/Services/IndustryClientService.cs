using System.Net.Http.Json;
using VisionaryNeedsAnalyzerApp.Client.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Client.Repositories.Services;

public class IndustryClientService : IIndustryClientService
{
    private readonly HttpClient httpClient;

    public IndustryClientService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Create(IndustryDto industry)
    {
        var response = await httpClient.PostAsJsonAsync("https://localhost:7078/api/industries", industry);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Result>(); // Deserializing the response to 'Result' object
            //return result; // Return or use the result as needed
        }
        else
        {
            throw new HttpRequestException($"Failed to create industry. Status code: {response.StatusCode}");
        }

    }

    public async Task<Result> Delete(string uniqueId)
    {
        var response = await httpClient.DeleteAsync($"https://localhost:7078/api/industries/{uniqueId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Result>(); // Deserializing the response to 'Result' object
            //return result; // Return or use the result as needed
        }
        else
        {
            throw new HttpRequestException($"Failed to create industry. Status code: {response.StatusCode}");
        }
    }

    public async Task<List<IndustryDto>> GetAll()
    {
        var response = await httpClient.GetAsync("https://localhost:7078/api/industries");

        if (response.IsSuccessStatusCode)
        {
            var industries = await response.Content.ReadFromJsonAsync<List<IndustryDto>>();
            return industries ?? new List<IndustryDto>();
        }
        else
        {
            throw new HttpRequestException($"Failed to retrieve industries. Status code: {response.StatusCode}");
        }
    }

    public async Task<Result> Update(IndustryDto industry)
    {
        var response = await httpClient.PostAsJsonAsync("https://localhost:7078/api/industries", industry);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Result>(); // Deserializing the response to 'Result' object
            //return result; // Return or use the result as needed
        }
        else
        {
            throw new HttpRequestException($"Failed to create industry. Status code: {response.StatusCode}");
        }
    }
}
