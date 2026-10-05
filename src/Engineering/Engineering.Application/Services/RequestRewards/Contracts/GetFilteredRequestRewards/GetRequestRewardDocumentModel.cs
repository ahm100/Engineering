namespace Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;

public record GetRequestRewardDocumentModel
{
    public long Id { get; set; }
    public string? Url { get; set; } = string.Empty;
}
