using Engineering.Application.Abstractions.Data.OperationLocations;

namespace Engineering.Application.Services.OperationLocations.Queries.GetsLocationByProjectOperationDetailIds;

public class GetsLocationByProjectOperationDetailIdsQueryHandler : IQueryHandler<GetsLocationByProjectOperationDetailIdsQuery, DataResult<List<GetsLocationByProjectOperationDetailIdsModel>>>
{
    private readonly IOperationLocationRepository _repository;
    private readonly ILogger<GetsLocationByProjectOperationDetailIdsQueryHandler> _logger;

    public GetsLocationByProjectOperationDetailIdsQueryHandler(ILogger<GetsLocationByProjectOperationDetailIdsQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsLocationByProjectOperationDetailIdsModel>>?>> Handle(GetsLocationByProjectOperationDetailIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsLocationByProjectOperationDetailIds(request.ProjectOperationDetailIds, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsLocationByProjectOperationDetailIdsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsLocationByProjectOperationDetailIdsModel>>>(OperationLocationErrors.FilteredOperationLocationNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsLocationByProjectOperationDetailIdsModel>>>(SharedErrors.UnknownError);
        }
    }
}