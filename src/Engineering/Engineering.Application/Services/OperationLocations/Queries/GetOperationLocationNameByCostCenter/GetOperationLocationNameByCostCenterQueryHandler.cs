using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByCostCenter;

public class GetOperationLocationNameByCostCenterQueryHandler : IQueryHandler<GetOperationLocationNameByCostCenterQuery, OperationLocation?>
{
    private readonly ILogger<GetOperationLocationNameByCostCenterQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public GetOperationLocationNameByCostCenterQueryHandler(ILogger<GetOperationLocationNameByCostCenterQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(GetOperationLocationNameByCostCenterQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationLocationNameByCostCenter(
                request.PrivateName,
                request.CostCenterId,
                request.ProjectId, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}
