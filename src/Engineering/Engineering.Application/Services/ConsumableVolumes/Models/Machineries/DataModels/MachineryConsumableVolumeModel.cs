namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;

public record ConsumableVolumeMachineryModel
{
    public long ProjectOperationDetailMachineryId { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long Id { get; init; }
    public string? MachineryName { get; init; }
    public string? MachineryCode { get; init; } = string.Empty;
    public decimal Number { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public bool IsStandard { get; init; }
    public string IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public string? StandardValue { get; set; }
    public decimal FinalValue { get; set; }
}