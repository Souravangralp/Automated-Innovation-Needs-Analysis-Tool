using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Repositories.Interfaces;

public interface IAssessmentService
{
    Task<List<AssessmentDto>> GetAll();

    Task<AssessmentDto> GetWithId(string uniqueId);

    Task<Result> Create(AssessmentDto assessmentDto);

    Task<Result> Update(AssessmentDto assessmentDto);

    Task<Result> Delete(string uniqueId);
}