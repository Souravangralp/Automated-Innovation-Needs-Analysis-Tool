using AutoMapper;
using VisionaryNeedsAnalyzerApp.Data.Models.Assessment;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.MappingProfiles;

public class AssessmentMappingProfile : Profile
{
    public AssessmentMappingProfile()
    {
        CreateMap<Assessment, AssessmentDto>()
            .ReverseMap()
            .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        
    }
}
