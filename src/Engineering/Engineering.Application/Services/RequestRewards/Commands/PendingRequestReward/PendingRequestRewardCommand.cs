using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.PendingRequestReward;

public record PendingRequestRewardCommand(long Id) : ICommand<RequestReward?>;

