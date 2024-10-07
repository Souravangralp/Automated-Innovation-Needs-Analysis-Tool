namespace VisionaryNeedsAnalyzerApp.Shared.Dto;

public class AssessmentDto
{
    public string? IndustryType { get; set; }
    public required string Name { get; set; }
    public double TotalScore { get; set; }
    
    [DefaultValue(false)]
    public bool IsLive { get; set; }

    [DefaultValue(false)]
    public bool IsActive { get; set; }

    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
    public string? UniqueId { get; set; }
}
