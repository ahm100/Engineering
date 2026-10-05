using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsWithoutParentOperationLocation;

public class GetsWithoutParentOperationLocationQueryHandler : IQueryHandler<GetsWithoutParentOperationLocationQuery, DataResult<List<OperationLocation>>>
{
    private readonly IOperationLocationRepository _repository;
    private readonly ILogger<GetsWithoutParentOperationLocationQueryHandler> _logger;

    public GetsWithoutParentOperationLocationQueryHandler(ILogger<GetsWithoutParentOperationLocationQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationLocation>>?>> Handle(GetsWithoutParentOperationLocationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsWithoutParentOperationLocation(
                request.CostCenterId,
                request.ProjectId,
                request.FilterData,
                request.IsActive,
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
