using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardThirdParty;

public record CreateRequestRewardThirdPartyCommand(long? Id,
                                                   long ThirdPartyId,
                                                   RequestReward RequestReward,
                                                   bool IsDeleted) : ICommand<RequestRewardThirdParty>;
