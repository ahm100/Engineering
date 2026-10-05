namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdProductCurrencyModel
{
    public long? Id { get; set; }
    public string? Name { get; set; } = string.Empty;
}