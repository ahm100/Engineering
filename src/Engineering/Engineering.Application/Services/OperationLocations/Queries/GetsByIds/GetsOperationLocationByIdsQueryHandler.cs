using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsByIds;

public class GetsOperationLocationByIdsQueryHandler : IQueryHandler<GetsOperationLocationByIdsQuery, DataResult<List<OperationLocation>>>
{
    private readonly IOperationLocationRepository _repository;
    private readonly ILogger<GetsOperationLocationByIdsQueryHandler> _logger;

    public GetsOperationLocationByIdsQueryHandler(ILogger<GetsOperationLocationByIdsQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationLocation>>?>> Handle(GetsOperationLocationByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByIds(request.Ids, ct);

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