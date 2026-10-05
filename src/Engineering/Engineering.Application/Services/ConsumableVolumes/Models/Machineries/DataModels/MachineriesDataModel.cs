namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DataModels;

public record MachineriesDataModel
{
    public long ProjectOperationDetailMachineryId { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long Id { get; init; }
    public string? MachineryName { get; init; }
    public string? MachineryCode { get; init; } = string.Empty;
    public decimal Number { get; init; }
    public decimal? UnusedPercentage { get; init; }
    public bool IsStandard { get; init; }
    public long? StandardValue { get; init; }
    public decimal FinalValue { get; set; }
}