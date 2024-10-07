using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Repositories.Interfaces;

public interface IIndustryService
{
    Task<List<IndustryDto>> GetAll();

    Task<Result> Create(IndustryDto industry);

    Task<Result> Update(IndustryDto industry);

    Task<Result> Delete(string id);
}