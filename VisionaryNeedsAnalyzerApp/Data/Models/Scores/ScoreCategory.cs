﻿using VisionaryNeedsAnalyzerApp.Data.Models.Common;

namespace VisionaryNeedsAnalyzerApp.Data.Models.Scores;

public class ScoreCategory : BaseEntity
{
    public required string Value { get; set; }

    public int PointsFrom { get; set; }

    public int PointsTo { get; set; }

    public string? Recommendation { get; set; }
}
