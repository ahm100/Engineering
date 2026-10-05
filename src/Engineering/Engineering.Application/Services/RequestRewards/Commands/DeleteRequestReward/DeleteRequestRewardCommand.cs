using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.DeleteRequestReward;

public record DeleteRequestRewardCommand(long Id) : ICommand<RequestReward>;

