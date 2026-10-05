using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationRequestReward;

public record CreateDailyProjectOperationRequestRewardCommand(DailyProjectOperation DailyProjectOperation,
                                                       long RequestRewardId
                                                     ) : ICommand<DailyProjectOperationRequestReward>;
