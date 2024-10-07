using VisionaryNeedsAnalyzerApp.Data.Models.Common;
using VisionaryNeedsAnalyzerApp.Data.Models.Industries;

namespace VisionaryNeedsAnalyzerApp.Data.Models.Assessment;

public class Assessment : BaseEntity
{
    public int? Assessment_IndustryID { get; set; }

    public string? Name { get; set; }

    public double? TotalScore { get; set; }

    public bool IsLive { get; set; }

    public Industry? Assessment_Industry { get; set; }
}
