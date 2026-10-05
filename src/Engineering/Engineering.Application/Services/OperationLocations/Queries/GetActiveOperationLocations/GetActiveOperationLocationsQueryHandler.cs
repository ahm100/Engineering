using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetActiveOperationLocations;

public class GetActiveOperationLocationsQueryHandler : IQueryHandler<GetActiveOperationLocationsQuery, DataResult<List<OperationLocation>>>
{
    private readonly IOperationLocationRepository _repository;
    private readonly ILogger<GetActiveOperationLocationsQuery> _logger;

    public GetActiveOperationLocationsQueryHandler(ILogger<GetActiveOperationLocationsQuery> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationLocation>>?>> Handle(GetActiveOperationLocationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveOperationLocations(
                request.FilterData,
                request.CostCenterId,
                request.ProjectId,
                request.PrivateName,
                request.PrivateCode,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

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