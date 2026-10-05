using Engineering.Application.Abstractions.Data.CostCenters;

namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterWarehouseByProjectOperation;

public class GetsCostCenterWarehouseByProjectOperationQueryHandler : IQueryHandler<GetsCostCenterWarehouseByProjectOperationQuery, List<long>?>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetsCostCenterWarehouseByProjectOperationQueryHandler> _logger;

    public GetsCostCenterWarehouseByProjectOperationQueryHandler(ILogger<GetsCostCenterWarehouseByProjectOperationQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<long>?>> Handle(GetsCostCenterWarehouseByProjectOperationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostCenterWarehouseByProjectOperation(request.ProjectOperationId, ct);
            if (result is null)
                return new List<long>(0);
            else
                return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<long>?>(SharedErrors.UnknownError);
        }
    }
}