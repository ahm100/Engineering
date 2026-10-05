namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdCurrencyModel
{
    public long? Id { get; set; }
    public string? Currency { get; set; } = string.Empty;
}
