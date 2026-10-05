namespace Engineering.Application.Services.RequestRewards.Contracts.DeleteRequestRewardByIds;

public record DeleteRequestRewardByIdsRequest(
    List<long> Ids
    ) : IHttpRequest;