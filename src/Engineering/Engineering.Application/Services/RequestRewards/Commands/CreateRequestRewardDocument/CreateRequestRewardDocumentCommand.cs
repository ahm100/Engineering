using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardDocument;

public record CreateRequestRewardDocumentCommand(long? Id,
                                                 string Url,
                                                 RequestReward RequestReward,
                                                 bool IsDeleted) : ICommand<RequestRewardDocument>;
