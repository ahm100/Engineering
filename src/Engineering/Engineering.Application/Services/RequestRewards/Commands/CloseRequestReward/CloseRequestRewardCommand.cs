using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CloseRequestReward;

public record CloseRequestRewardCommand(long Id) : ICommand<RequestReward?>;

