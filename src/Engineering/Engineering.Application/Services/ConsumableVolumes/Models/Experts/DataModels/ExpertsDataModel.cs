namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.DataModels;

public record ExpertsDataModel
{
    public long Id { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long ExpertId { get; init; }
    public decimal Number { get; init; }
    public decimal? UnusedPercentage { get; init; }
    public bool IsStandard { get; init; }
    public long? StandardValue { get; init; }
    public long FinalValue { get; set; }
}

