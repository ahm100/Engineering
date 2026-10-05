namespace Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;

public record GetFilteredRequestRewardsResponse
{
    public TotalPricesRequestRewardModel? OtherData { get; set; }
    public List<GetFilteredRequestRewardsModel> Data { get; set; } = new();
    public int RowCount { get; set; }
}
public record TotalPricesRequestRewardModel
{
    public decimal? TotalOfferedPriceReward { get; set; } = 0;
    public decimal? TotalConfirmedPriceReward { get; set; } = 0;
    public decimal? TotalOfferedPriceFine { get; set; } = 0;
    public decimal? TotalConfirmedPriceFine { get; set; } = 0;
}
