using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VisionaryNeedsAnalyzerApp.Data;
using VisionaryNeedsAnalyzerApp.Data.Models.Industries;
using VisionaryNeedsAnalyzerApp.Repositories.Interfaces;
using VisionaryNeedsAnalyzerApp.Shared.Common;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.Repositories.Services;

public class IndustryService : IIndustryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public IndustryService(ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> Create(IndustryDto industry)
    {
        List<Error> errors = [];

        var existingIndustry = await _context.Industries.Where(i => i.Name.Equals(industry.Name) || i.Code.Equals(industry.Code)).FirstOrDefaultAsync();

        if (existingIndustry != null) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we already have a industry Name: {industry.Name} with Code: {industry.Code}. Please add a unique one, thanks." }); };

        if (errors.Any()) { return new Result() { Success = false, Errors = errors, Model = null }; }

        var newIndustry = _mapper.Map<Industry>(industry);
        try
        {
            await _context.Industries.AddAsync(newIndustry);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
        

        return new Result() { Success = true, Errors = errors, Model = industry };
    }

    public async Task<List<IndustryDto>> GetAll()
    {
        var industries = await _context.Industries.Where(i => !i.IsDeleted && i.IsActive == true).ToListAsync();

        return _mapper.Map<List<IndustryDto>>(industries);
    }

    public async Task<Result> Update(IndustryDto industryDto)
    {
        List<Error> errors = [];

        var existingIndustry = await _context.Industries.Where(i => i.Name.Equals(industryDto.Name) || i.Code.Equals(industryDto.Code)).FirstOrDefaultAsync();

        if (existingIndustry != null && existingIndustry.UniqueId != industryDto.UniqueId) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we have already have a industry Name: {industryDto.Name} || Code: {industryDto.Code}. Please add a unique one, thanks." }); };

        var toBeUpdatedIndustry = await _context.Industries.Where(i => i.UniqueId.Equals(industryDto.UniqueId)).FirstOrDefaultAsync();

        if (toBeUpdatedIndustry is null) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we have not found any industry with Id: {industryDto.UniqueId}. Please enter a valid record to update, thanks." }); }

        if (errors.Any()) { return new Result() { Success = false, Errors = errors, Model = null }; }

        var updateIndustry = _mapper.Map(existingIndustry, toBeUpdatedIndustry);

        //toBeUpdatedIndustry.Name = industryDto.Name;
        //toBeUpdatedIndustry.Code = industryDto.Code;
        //toBeUpdatedIndustry.Description = industryDto.Description;
        //toBeUpdatedIndustry.IsActive = industryDto.IsActive;
        //toBeUpdatedIndustry.IsDeleted = industryDto.IsDeleted;

        //_context.Industries.Update(toBeUpdatedIndustry);
        _context.Industries.Update(updateIndustry);

        await _context.SaveChangesAsync();

        return new Result() { Success = true, Errors = errors, Model = industryDto };
    }

    public async Task<Result> Delete(string uniqueId)
    {
        List<Error> errors = [];

        var existingIndustry = await _context.Industries.Where(i => i.UniqueId.Equals(uniqueId)).FirstOrDefaultAsync();

        if (existingIndustry is null) { errors.Add(new Error() { Message = $"Apologizes for incontinence! but we have not found any industry with Id: {uniqueId}. Please enter a valid record to update, thanks." }); }

        if (errors.Any()) { return new Result() { Success = false, Errors = errors, Model = null }; }

        _context.Industries.Remove(existingIndustry);

        await _context.SaveChangesAsync();

        return new Result() { Success = true, Errors = errors, Model = null };
    }
}


//public class Result
//{
//    public bool Success { get; set; }

//    public object? Model { get; set; } = null;

//    public List<Error> Errors { get; set; } = [];
//}

//public class Error
//{
//    public string Message { get; set; } = string.Empty;
//}