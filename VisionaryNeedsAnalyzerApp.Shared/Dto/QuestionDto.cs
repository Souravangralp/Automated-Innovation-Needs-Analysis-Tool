namespace VisionaryNeedsAnalyzerApp.Shared.Dto;

public class QuestionDto
{
    public int? QuestionType_GeneralLookUpID { get; set; }

    [DefaultValue("")]
    public string? UniqueId { get; set; }

    public int DisplayOrder { get; set; }

    [DefaultValue("")]
    public string? Value { get; set; }

    [DefaultValue("")]
    public string? Description { get; set; }

    public int? OldDisplayOrder { get; set; }

    public virtual List<OptionDto> Options { get; set; } = [];
}
