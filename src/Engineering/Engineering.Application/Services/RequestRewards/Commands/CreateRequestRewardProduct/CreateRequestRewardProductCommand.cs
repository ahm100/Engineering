using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardProduct;

public record CreateRequestRewardProductCommand(long? Id,
                                                long ProductId,
                                                long CurrencyId,
                                                int Count,
                                                decimal Price,
                                                RequestReward RequestReward,
                                                bool IsDeleted) : ICommand<RequestRewardProduct>;
