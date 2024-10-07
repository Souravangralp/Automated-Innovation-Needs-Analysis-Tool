using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VisionaryNeedsAnalyzerApp.Data;
using VisionaryNeedsAnalyzerApp.Data.Models.Assessment;
using VisionaryNeedsAnalyzerApp.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Repositories.Services;

public class AssessmentService : IAssessmentService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AssessmentService(ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result> Create(AssessmentDto assessmentDto)
    {
        List<Error> errors = [];

        var industry = await _context.Industries.Where(i => i.Name.Trim().ToLower() == assessmentDto.IndustryType.Trim().ToLower()).FirstOrDefaultAsync();

        if (industry == null)
        {
            errors.Add(new Error() { Message = $"Apologizes for incontinence! but we done have a industry type {assessmentDto.IndustryType}." });
        }

        var existingAssessment = await _context.Assessments.Where(i => i.Name.Trim().ToLower() == assessmentDto.Name.Trim().ToLower() && i.Assessment_IndustryID.Equals(industry.Id)).FirstOrDefaultAsync();

        if (existingAssessment != null) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we already have a assessment Name: {assessmentDto.Name} with Code: {assessmentDto.TotalScore}. Please add a unique one, thanks." }); };

        if (errors.Any()) { return new Result() { Success = false, Errors = errors, Model = null }; }

        var newAssessment = _mapper.Map<Assessment>(assessmentDto);

        newAssessment.Assessment_IndustryID = industry.Id;

        try
        {
            await _context.Assessments.AddAsync(newAssessment);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }

        var test = _mapper.Map<AssessmentDto>(newAssessment);

        test.IndustryType = assessmentDto.IndustryType;

        return new Result() { Success = true, Errors = errors, Model = test };
    }

    public async Task<Result> Delete(string uniqueId)
    {
        List<Error> errors = [];

        var existingAssessment = await _context.Assessments.Where(i => i.UniqueId.Equals(uniqueId)).FirstOrDefaultAsync();

        if (existingAssessment is null) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we have not found any assessment with Id: {uniqueId}. Please enter a valid record to update, thanks." }); }

        if (errors.Any()) { return new Result() { Success = false, Errors = errors, Model = null }; }

        _context.Assessments.Remove(existingAssessment);

        await _context.SaveChangesAsync();

        return new Result() { Success = true, Errors = errors, Model = null };
    }

    public async Task<List<AssessmentDto>> GetAll()
    {
        var assessments = await _context.Assessments.Where(i => !i.IsDeleted && i.IsActive == true).Include(x => x.Assessment_Industry).ToListAsync();

        List<AssessmentDto> assessmentDtos = [];

        foreach (var assessment in assessments)
        {
            var industryType = assessment.Assessment_Industry?.Name;

            var assessmentDto = _mapper.Map<AssessmentDto>(assessment);

            assessmentDto.IndustryType = industryType;

            assessmentDtos.Add(assessmentDto);
        }

        return assessmentDtos;
    }

    public async Task<AssessmentDto> GetWithId(string uniqueId)
    {
        var assessment = await _context.Assessments.Where(x => x.UniqueId == uniqueId).Include(x=> x.Assessment_Industry).FirstOrDefaultAsync();

        var newAssessment = _mapper.Map<AssessmentDto>(assessment);

        newAssessment.IndustryType = assessment?.Assessment_Industry?.Name ?? string.Empty;

        return newAssessment;
    }

    public async Task<Result> Update(AssessmentDto assessmentDto)
    {
        List<Error> errors = [];

        var existingAssessment = await _context.Industries.Where(i => i.Name.Equals(assessmentDto.Name) || i.Code.Equals(assessmentDto.TotalScore)).FirstOrDefaultAsync();

        if (existingAssessment != null && existingAssessment.UniqueId != assessmentDto.UniqueId) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we have already have a assessment Name: {assessmentDto.Name} || Code: {assessmentDto.IndustryType}. Please add a unique one, thanks." }); };

        var toBeUpdatedAssessment = await _context.Industries.Where(i => i.UniqueId.Equals(assessmentDto.UniqueId)).FirstOrDefaultAsync();

        if (toBeUpdatedAssessment is null) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we have not found any assessment with Id: {assessmentDto.UniqueId}. Please enter a valid record to update, thanks." }); }

        if (errors.Any()) { return new Result() { Success = false, Errors = errors, Model = null }; }

        var updateAssessment = _mapper.Map(existingAssessment, toBeUpdatedAssessment);

        _context.Industries.Update(updateAssessment);

        await _context.SaveChangesAsync();

        return new Result() { Success = true, Errors = errors, Model = assessmentDto };
    }
}
