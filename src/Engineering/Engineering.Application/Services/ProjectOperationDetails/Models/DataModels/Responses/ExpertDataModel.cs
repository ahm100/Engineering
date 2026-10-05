namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

public record ExpertDataModel
{
    public long? Id { get; set; }
    public long ExpertId { get; set; }
    public string? ExpertName { get; set; } = string.Empty;
    public string? ExpertCode { get; set; } = string.Empty;
    public decimal Number { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public bool IsStandard { get; set; }
    public string? IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public string? StandardValue { get; set; }
    public string FinalValue { get; set; } = "01:00";
    public bool? HasContractorExpert { get; set; }
}