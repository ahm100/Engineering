using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetsConfirmedContractorRequestReward;

public record GetsConfirmedContractorRequestRewardQuery(
    long ProjectId,
    long ContractorId,
    DateTime FromDate,
    DateTime ToDate,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<RequestReward>>>;

