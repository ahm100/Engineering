
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;

namespace Engineering.Application.Services.RequestRewards;

partial class RequestRewardLogic
{

    public async Task<Result<GetFilteredRequestRewardsResponse?>> GetFilteredRequestRewardsHandler(
        GetFilteredRequestRewardsRequest request, long? registerUserId, long? managerId, CT ct)
    {
        try
        {
            var result = await _requestRewardRepository.GetFilteredRequestRewards(
            null,
            request.CostCenterId,
            request.ProjectId,
            request.ThirdPartyId,
            registerUserId,
            managerId,
            request.EmployerContractId,
            request.IsReward,
            request.Fromdate,
            request.Todate,
            request.Status,
            request.Type,
            request.ProjectOperationId,
            request.ProjectOperationDetailId,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            ct);
            if (result.Data is null || result.RowCount == 0)
                return Result.Failure<GetFilteredRequestRewardsResponse>(RequestRewardErrors.RequestRewardWithIdNotFound);
            return new GetFilteredRequestRewardsResponse
            { OtherData = null, Data = result.Data, RowCount = result.RowCount };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFilteredRequestRewardsResponse>(SharedErrors.UnknownError);
        }
    }
}