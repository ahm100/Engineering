namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.UpdateMachineryConsumableVolume;

public record UpdateConsumableVolumeMachineryResponse
{
    public long ProjectOperationDetailMachineryId { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long Id { get; init; }
    public string? MachineryName { get; init; }
    public string? MachineryCode { get; init; } = string.Empty;
    public decimal Number { get; init; }
    public decimal? UnusedPercentage { get; init; }
    public bool IsStandard { get; init; }
    public string IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public string? StandardValue { get; init; }
    public string FinalValue { get; init; } = "01:00";
}