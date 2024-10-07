using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Client.Repositories.Interfaces;

public interface IAssessmentClientService
{
    Task<List<AssessmentDto>> GetAll();

    Task<AssessmentDto> GetWithId(string uniqueId);

    Task<Result> Create(AssessmentDto assessmentDto);

    Task<Result> Update(AssessmentDto assessmentDto);

    Task<Result> Delete(string uniqueId);
}
