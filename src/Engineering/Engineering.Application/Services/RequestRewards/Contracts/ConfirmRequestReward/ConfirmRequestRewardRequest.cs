
namespace Engineering.Application.Services.RequestRewards.Contracts.ConfirmRequestReward;

public record ConfirmRequestRewardRequest(
    long Id,
    long? CurrencyId,
    decimal ConfirmedPrice,
    string? ManagerDescription
    ) : IHttpRequest;
