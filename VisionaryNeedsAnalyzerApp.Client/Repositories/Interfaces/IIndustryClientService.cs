using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Client.Repositories.Interfaces;

public interface IIndustryClientService
{
    Task<List<IndustryDto>> GetAll();

    Task<Result> Create(IndustryDto industry);

    Task<Result> Update(IndustryDto industry);

    Task<Result> Delete(string uniqueId);
}