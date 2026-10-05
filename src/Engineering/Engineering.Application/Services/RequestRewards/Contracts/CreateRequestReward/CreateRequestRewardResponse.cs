namespace Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;

public record CreateRequestRewardResponse(
    List<long> Ids,
    bool IsCreated
    );