using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetFilteredProjectOperationDetails;

public class GetFilteredProjectOperationDetailsQueryHandler : IQueryHandler<GetFilteredProjectOperationDetailsQuery, GetFilteredProjectOperationDetailsResponse>
{
    private readonly ILogger<GetFilteredProjectOperationDetailsQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetFilteredProjectOperationDetailsQueryHandler(ILogger<GetFilteredProjectOperationDetailsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetFilteredProjectOperationDetailsResponse?>> Handle(
        GetFilteredProjectOperationDetailsQuery request,
        CT ct)
    {
        try
        {
            var response = await _repository.GetProjectOperationDetailByCostCenterId(
                request.CostCenterId,
                request.ProjectIds,
                request.CategoryIds,
                request.BranchIds,
                request.SeasonIds,
                request.OperationInfoIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            var result = new GetFilteredProjectOperationDetailsResponse(
                Data: response.Data,
                RowCount: response.RowCount
            );

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFilteredProjectOperationDetailsResponse?>(
                SharedErrors.UnknownError
            );
        }
    }

}
