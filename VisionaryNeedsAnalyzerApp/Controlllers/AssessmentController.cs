using Microsoft.AspNetCore.Mvc;
using VisionaryNeedsAnalyzerApp.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Controlllers;

[ApiController]
public class AssessmentController : ControllerBase
{
    private readonly IAssessmentService _assessmentService;

    public AssessmentController(IAssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }

    [HttpGet("api/assessment")]
    public async Task<List<AssessmentDto>> GetAll()
    {
        return await _assessmentService.GetAll();
    }

    [HttpGet("api/assessment/{id}")]
    public async Task<AssessmentDto> GetWithId(string id)
    {
        return await _assessmentService.GetWithId(id);
    }

    [HttpPost("api/assessment")]
    public async Task<Result> Create(AssessmentDto assessmentDto)
    {
        return await _assessmentService.Create(assessmentDto);
    }

    [HttpPatch("api/assessment")]
    public async Task<Result> Update(AssessmentDto assessmentDto)
    {
        return await _assessmentService.Update(assessmentDto);
    }

    [HttpDelete("api/assessment/{uniqueId}")]
    public async Task<Result> Delete(string uniqueId)
    {
        return await _assessmentService.Delete(uniqueId);
    }
}
