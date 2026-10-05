using Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrPOForReports;
using Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;

namespace Engineering.Application.Services.ProjectOperations;

public partial class ProjectOperationLogic : IProjectOperationLogic
{
    public async Task<Result<List<GetFltrBasePricedPOsModel>?>> GetFltrBasePricedHandle(
        GetFltrBasePricedPOsRequest request,
        CT ct)
    {
        try
        {
            var result = await _repository.GetFltrBasePricedPOs(
                request.CostCenterId,
                request.ProjectIds,
                request.ActionIds,
                request.CategoryIds,
                request.BranchIds,
                request.SeasonIds,
                request.OperationInfoIds,
                request.FilterData,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetFltrBasePricedPOsModel>?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetPOActionByPOIdResponse?>> GetPOActionByPOIdQuery(
        GetPOActionByPOIdRequest request,
        CT ct)
    {
        try
        {
            var result = await _projectOperationActionRepository.GetPOActionByPOId(
                request.ProjectOperationId,
                ct);
            if (result is null)
                return Result.Failure<GetPOActionByPOIdResponse>(ProjectOperationErrors.ProjectOperationActionNotFound);

            return new GetPOActionByPOIdResponse(result, result.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetPOActionByPOIdResponse?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetFltrPOForReportsResponse?>> GetFltrPOForReportsQuery(
        GetFltrPOForReportsRequest request,
        CT ct)
    {
        try
        {
            var result = await _repository.GetFltrPOForReports(
                request.CostCenterIds,
                request.ProjectIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);
            if (result is null)
                return Result.Failure<GetFltrPOForReportsResponse>(ProjectOperationErrors.NotFound);

            return new GetFltrPOForReportsResponse(result, result.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrPOForReportsResponse?>(SharedErrors.UnknownError);
        }
    }
}