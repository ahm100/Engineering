namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdProjectOperationDetailModel
{
    public long Id { get; set; }
    public string PublicName { get; set; } = string.Empty;
}
