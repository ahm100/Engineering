namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdThirdPartyModel
{
    public long Id { get; set; }
    public long ThirdPartyId { get; set; }
    public string? FullName { get; set; } = string.Empty;
}