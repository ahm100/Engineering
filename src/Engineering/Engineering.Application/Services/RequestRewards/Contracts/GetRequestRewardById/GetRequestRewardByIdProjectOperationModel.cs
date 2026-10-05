namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdProjectOperationModel
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
}
