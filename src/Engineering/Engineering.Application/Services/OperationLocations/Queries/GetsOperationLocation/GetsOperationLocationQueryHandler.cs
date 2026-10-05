using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsOperationLocation;

public class GetsOperationLocationQueryHandler : IQueryHandler<GetsOperationLocationQuery, DataResult<List<OperationLocation>>>
{
    private readonly IOperationLocationRepository _repository;
    private readonly ILogger<GetsOperationLocationQueryHandler> _logger;

    public GetsOperationLocationQueryHandler(ILogger<GetsOperationLocationQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationLocation>>?>> Handle(GetsOperationLocationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationLocation(request.FilterData, request.PublicName, request.PublicCode, request.Ids, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

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