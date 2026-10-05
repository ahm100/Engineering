namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

public record ConsumableVolumeExpertModel
{
    public long ProjectOperationDetailExpertId { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public decimal Number { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public bool IsStandard { get; init; }
    public string? IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public string? StandardValue { get; init; }
    public string FinalValue { get; set; } = "01:00";
}

