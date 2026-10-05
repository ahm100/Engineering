using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.DeleteRequestRewardByIds;

public record DeleteRequestRewardByIdsCommand(
    List<long> Ids
    ) : ICommand<List<RequestReward>?>;