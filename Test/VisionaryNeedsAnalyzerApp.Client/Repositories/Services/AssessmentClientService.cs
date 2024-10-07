using System.Net.Http.Json;
using VisionaryNeedsAnalyzerApp.Client.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Client.Repositories.Services;

public class AssessmentClientService : IAssessmentClientService
{
    private readonly HttpClient httpClient;

    public AssessmentClientService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<Result> Create(AssessmentDto assessmentDto)
    {
        var response = await httpClient.PostAsJsonAsync("https://localhost:7078/api/assessment", assessmentDto);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Result>(); // Deserializing the response to 'Result' object
            return result; // Return or use the result as needed
        }
        else
        {
            throw new HttpRequestException($"Failed to create industry. Status code: {response.StatusCode}");
        }
    }

    public async Task<Result> Delete(string uniqueId)
    {
        var response = await httpClient.DeleteAsync($"https://localhost:7078/api/assessment/{uniqueId}");

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

    public async Task<List<AssessmentDto>> GetAll()
    {
        var response = await httpClient.GetAsync("https://localhost:7078/api/assessment");

        if (response.IsSuccessStatusCode)
        {
            var assessments = await response.Content.ReadFromJsonAsync<List<AssessmentDto>>();
            return assessments ?? new List<AssessmentDto>();
        }
        else
        {
            throw new HttpRequestException($"Failed to retrieve industries. Status code: {response.StatusCode}");
        }
    }

    public async Task<AssessmentDto> GetWithId(string uniqueId)
    {
        var response = await httpClient.GetAsync($"https://localhost:7078/api/assessment/{uniqueId}");

        if (response.IsSuccessStatusCode)
        {
            var test = await response.Content.ReadFromJsonAsync<AssessmentDto>();

            return test;
        }
        else
        {
            throw new HttpRequestException($"Failed to retrieve industries. Status code: {response.StatusCode}");
        }
    }

    public async Task<Result> Update(AssessmentDto assessmentDto)
    {
        var response = await httpClient.PostAsJsonAsync("https://localhost:7078/api/assessment", assessmentDto);

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
