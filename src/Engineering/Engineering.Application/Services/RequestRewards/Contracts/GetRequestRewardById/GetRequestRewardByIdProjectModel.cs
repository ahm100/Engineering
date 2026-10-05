namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdProjectModel
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}
