using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Abstractions.Data.RequestRewards;

public interface IRequestRewardRepository : IBaseRepository<RequestReward>
{
    Task<(List<GetFilteredRequestRewardsModel> Data, int RowCount)>
    GetFilteredRequestRewards(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        long? thirdPartyId,
        long? registerUserId,
        long? managerId,
        long? employerContractId,
        bool? isReward,
        DateTime? fromdate,
        DateTime? todate,
        RequestRewardStatus? status,
        RequestRewardType? type,
        long? projectOperationId,
        long? projectOperationDetailId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<RequestReward> Data, int RowCount)> GetsConfirmedContractorRequestReward(
        long projectId,
        long contractorId,
        DateTime fromDate,
        DateTime toDate,
        int pageIndex,
        int pageSize, CT ct);

    Task<RequestReward?> GetById(long id, CT ct);

    Task<List<RequestReward>> GetRequestRewardsByDate(
        DateTime startDate,
        DateTime endDate,
        List<long>? projectOperationIds,
        List<long>? fiduciaryProductDetailReturnIds,
        RequestRewardType type,
        CT ct);

    Task<(List<RequestReward> Data, int RowCount)> GetRequestRewardsByIds(List<long> ids, CT ct);

    Task<List<RequestReward>> GetDiscounts(
        long projectId,
        long contractorId, CT ct);
}
