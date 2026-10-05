using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocations;

public class GetOperationLocationsQueryHandler : IQueryHandler<GetOperationLocationsQuery, DataResult<List<OperationLocation>>>
{
    private readonly IOperationLocationRepository _repository;
    private readonly ILogger<GetOperationLocationsQueryHandler> _logger;

    public GetOperationLocationsQueryHandler(ILogger<GetOperationLocationsQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationLocation>>?>> Handle(GetOperationLocationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationLocations(
                request.Ids,
                request.CostCenterId,
                request.ProjectId,
                request.ParentId,
                request.FilterData,
                request.IsActive,
                request.OrderBy,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationLocation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationLocation>>>(OperationLocationErrors.FilteredOperationLocationNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationLocation>>>(SharedErrors.UnknownError);
        }
    }
}