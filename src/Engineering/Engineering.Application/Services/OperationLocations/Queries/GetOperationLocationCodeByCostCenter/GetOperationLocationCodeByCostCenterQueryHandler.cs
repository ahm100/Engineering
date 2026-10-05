using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByCostCenter;

public class GetOperationLocationCodeByCostCenterQueryHandler : IQueryHandler<GetOperationLocationCodeByCostCenterQuery, OperationLocation?>
{
    private readonly ILogger<GetOperationLocationCodeByCostCenterQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public GetOperationLocationCodeByCostCenterQueryHandler(ILogger<GetOperationLocationCodeByCostCenterQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(GetOperationLocationCodeByCostCenterQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationLocationCodeByCostCenter(
                request.PrivateCode,
                request.CostCenterId,
                request.ProjectId, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}
