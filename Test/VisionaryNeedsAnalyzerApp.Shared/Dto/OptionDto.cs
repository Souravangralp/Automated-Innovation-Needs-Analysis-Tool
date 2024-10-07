namespace VisionaryNeedsAnalyzerApp.Shared.Dto;

public class OptionDto
{
    [DefaultValue("")]
    public string? UniqueId { get; set; }

    public int DisplayOrder { get; set; }

    [DefaultValue("")]
    public string? Name { get; set; }

    [DefaultValue(false)]
    public bool ISCorrect { get; set; }
}
