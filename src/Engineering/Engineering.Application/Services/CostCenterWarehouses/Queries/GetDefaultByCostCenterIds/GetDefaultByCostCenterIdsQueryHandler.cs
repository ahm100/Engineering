using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetDefaultByCostCenterIds;

public class GetDefaultByCostCenterIdsQueryHandler : IQueryHandler<GetDefaultByCostCenterIdsQuery, List<CostCenterWarehouse>>
{
    private readonly ICostCenterWarehouseRepository _repository;
    private readonly ILogger<GetDefaultByCostCenterIdsQueryHandler> _logger;

    public GetDefaultByCostCenterIdsQueryHandler(ILogger<GetDefaultByCostCenterIdsQueryHandler> logger,
        ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<CostCenterWarehouse>?>> Handle(GetDefaultByCostCenterIdsQuery request,
        CT ct)
    {
        try
        {
            var result = await _repository.GetDefaultByCostCenterIds(request.CostCenterIds, ct);

            return result.Any()
                ? result
                : Result.Failure<List<CostCenterWarehouse>>(CostCenterWarehouseErrors.DataIsNull);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CostCenterWarehouse>>(SharedErrors.UnknownError);
        }
    }
}