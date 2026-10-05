using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.RejectRequestReward;

public record RejectRequestRewardCommand(long Id, string? ManagerDescription) : ICommand<RequestReward?>;

