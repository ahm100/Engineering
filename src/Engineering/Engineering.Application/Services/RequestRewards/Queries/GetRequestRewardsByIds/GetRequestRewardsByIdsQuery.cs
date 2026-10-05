using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardsByIds;

public record GetRequestRewardsByIdsQuery(List<long> Ids) : IQuery<DataResult<List<RequestReward>>>;

