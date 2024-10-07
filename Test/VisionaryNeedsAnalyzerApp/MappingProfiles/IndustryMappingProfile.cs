using AutoMapper;
using VisionaryNeedsAnalyzerApp.Data.Models.Industries;
using VisionaryNeedsAnalyzerApp.Shared.Dto;

namespace VisionaryNeedsAnalyzerApp.MappingProfiles;

public class IndustryMappingProfile : Profile
{
    public IndustryMappingProfile()
    {
        CreateMap<Industry, IndustryDto>()
            .ReverseMap()
            .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        
    }
}
