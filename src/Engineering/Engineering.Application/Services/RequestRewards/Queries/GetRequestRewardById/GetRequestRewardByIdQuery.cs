using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardById;

public record GetRequestRewardByIdQuery(long Id) : IQuery<RequestReward>;

