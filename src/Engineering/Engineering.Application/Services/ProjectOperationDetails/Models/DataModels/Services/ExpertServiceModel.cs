namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;

public record ExpertServiceModel
{
    public string? TempId { get; set; }
    public long ExpertId { get; init; }
    public string? ExpertName { get; set; }
    public string? ExpertCode { get; set; }
    public decimal? Number { get; init; }
    public decimal? UnusedPercentage { get; init; }
    public bool IsStandard { get; init; }
    public string? IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public long? StandardValue { get; set; }
    public long FinalValue { get; set; }
}