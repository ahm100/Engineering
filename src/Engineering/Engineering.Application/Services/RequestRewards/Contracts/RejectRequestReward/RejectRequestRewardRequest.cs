

namespace Engineering.Application.Services.RequestRewards.Contracts.RejectRequestReward;

public record RejectRequestRewardRequest(long Id, string? ManagerDescription) : IHttpRequest;

