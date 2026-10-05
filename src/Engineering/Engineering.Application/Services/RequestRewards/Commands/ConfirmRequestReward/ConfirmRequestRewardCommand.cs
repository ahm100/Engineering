using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.ConfirmRequestReward;

public record ConfirmRequestRewardCommand(
    long Id,
    long? CurrencyId,
    decimal ConfirmedPrice,
    string? ManagerDescription) : ICommand<RequestReward>;

