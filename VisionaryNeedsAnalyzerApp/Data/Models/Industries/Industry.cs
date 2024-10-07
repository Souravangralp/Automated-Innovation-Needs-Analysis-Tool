using VisionaryNeedsAnalyzerApp.Data.Models.Common;

namespace VisionaryNeedsAnalyzerApp.Data.Models.Industries;

public class Industry : BaseEntity
{
    public required string Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
}
