namespace Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;

public record GetRequestRewardThirdPartyModel
{
    public long Id { get; set; }
    public long ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; } = string.Empty;
}