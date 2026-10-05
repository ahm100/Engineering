namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdProductModel
{
    public long? Id { get; set; }
    public GetRequestRewardByIdProduct? Product { get; set; }
    public GetRequestRewardByIdProductCurrencyModel? Currency { get; set; }
}
