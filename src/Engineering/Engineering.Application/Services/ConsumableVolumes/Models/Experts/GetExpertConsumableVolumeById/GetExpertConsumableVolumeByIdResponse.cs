namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertConsumableVolumeById;

public record GetConsumableVolumeExpertByIdResponse
{
    public long ProjectOperationDetailExpertId { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public decimal Number { get; init; }
    public decimal? UnusedPercentage { get; init; }
    public bool IsStandard { get; init; }
    public string? IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public string? StandardValue { get; init; }
    public string FinalValue { get; init; } = "01:00";
}