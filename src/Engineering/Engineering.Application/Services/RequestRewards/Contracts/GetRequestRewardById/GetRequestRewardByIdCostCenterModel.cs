namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdCostCenterModel
{
    public long Id { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
}
