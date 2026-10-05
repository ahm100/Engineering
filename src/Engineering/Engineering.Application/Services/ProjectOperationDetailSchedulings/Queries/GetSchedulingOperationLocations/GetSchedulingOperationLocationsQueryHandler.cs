using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperationDetails;

public class GetSchedulingOperationLocationsQueryHandler : IQueryHandler<GetSchedulingOperationLocationsQuery, DataResult<List<OperationLocation>>>
{
    private readonly ILogger<GetSchedulingOperationLocationsQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetSchedulingOperationLocationsQueryHandler(ILogger<GetSchedulingOperationLocationsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationLocation>>?>> Handle(GetSchedulingOperationLocationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationLocationForSchecdulingAsync(request.CostCenterId, request.ProjectId,
                request.FilterData, request.OperationInfoIds, request.OperationLocationIds, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationLocation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationLocation>>>(OperationLocationErrors.OperationLocationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationLocation>>>(SharedErrors.UnknownError);
        }
    }
}
