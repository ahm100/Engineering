namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdDocumentModel
{
    public long Id { get; set; }
    public string? Url { get; set; } = string.Empty;
}
